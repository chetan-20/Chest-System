using UnityEngine;

public class ChestController 
{
    private ChestDataSO chestData;
    private ChestView chestView;
    public ChestStateMachine chestStateMachine;
    private float currentTimeInSeconds;
    public float startTime;
    public ChestStates currentChestState;
    public ChestController(ChestDataSO chestDataSO,ChestView chestView)
    {
        this.chestData = chestDataSO;
        this.chestView = chestView;
        chestView.SetViewController(this);
        currentChestState = ChestStates.NOTCREATED;
    }
    public void SetChest()
    {
        SetChestImage();
        SetChestRarity();
        SetChestStatus();
    }
    public void CreateStateMachine()
    {
        if (chestStateMachine == null)
        {
            chestStateMachine = new ChestStateMachine(this);
            chestStateMachine.Initialize(chestStateMachine.lockedState);            
        }       
    }
    public int GetRandomCoins()
    {
        return Random.Range(chestData.coinsMinRange,chestData.coinsMaxRange);
    }
    public int GetRandomGems()
    {
        return Random.Range(chestData.gemsMinRange,chestData.gemsMaxRange);
    }
    private void SetChestImage() => chestView.chestImage.sprite = chestData.chestSprite;
    private void SetChestRarity() => chestView.chestTypeText.text = chestData.chestType.ToString();
    private void SetChestStatus() => chestView.chestStatusText.text = "LOCKED";
    private string GetGemsRange()
    {
        return ""+chestData.gemsMinRange+"-"+chestData.gemsMaxRange;
    }
    private string GetCoinsRange()
    {
        return "" + chestData.coinsMinRange + "-" + chestData.coinsMaxRange;
    }
    public int GetTimeLimit() => chestData.timerInMinutes;
    public void ShowChestData()
    {
        UIService.Instance.gemsToGainText.text = GetGemsRange();
        UIService.Instance.coinsToGainText.text = GetCoinsRange();
        UIService.Instance.timeLimitText.text = "Time To Open " + GetTimeLimit() + " Mins";
    }
    public void OnOpenForFree()
    {        
        chestStateMachine.ChangeState(chestStateMachine.unlockingState);        
    }
    public void StartChestTimer()
    {        
        float elapseTime = Time.time - startTime;
        currentTimeInSeconds = chestData.timerInMinutes * 60;
        if( currentTimeInSeconds > 0 )
        {
            currentTimeInSeconds -= elapseTime;
            UpdateTimerText();
        }
        else
        {
            SetChestStatusText("UNLOCKED");
            chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);           
        }
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(currentTimeInSeconds % 60);
        chestView.chestStatusText.text = minutes + " : " + seconds;
    }
    public void SetChestStatusText(string text)
    {
        chestView.chestStatusText.text = text;
    }
    public void EnableClickingCurrentChest()
    {
        chestView.inputHandler.SetClickStatus(true);
    }
    public void CheckCurrentState(ChestStates state)
    {
        switch (state)
        {
            case ChestStates.NOTCREATED:               
                CreateStateMachine();
                break;
            case ChestStates.LOCKED:               
                chestStateMachine.lockedState.OnEnterState();
                break;
            case ChestStates.UNLOCKING:
                break;
            case ChestStates.UNLOCKED:
                break;
            case ChestStates.COLLECTED:
                break;
            default:
                break;
        }
    }
}
