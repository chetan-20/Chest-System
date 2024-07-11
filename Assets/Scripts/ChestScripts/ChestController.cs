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
        GameService.Instance.UIService.OnChestClick();
        GameService.Instance.UIService.gemsToGainText.text = GetGemsRange();
        GameService.Instance.UIService.coinsToGainText.text = GetCoinsRange();
        GameService.Instance.UIService.timeLimitText.text = "Time To Open " + GetTimeLimit() + " Mins";
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
            SetBuyButtonTextOnChest(currentTimeInSeconds);
        }
        if(currentTimeInSeconds<=0)
        {          
            chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);           
        }
    }
    private void UpdateTimerText()
    {
        /*int hours = Mathf.FloorToInt(currentTimeInSeconds / 3600);
        int minutes = Mathf.FloorToInt(currentTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(currentTimeInSeconds % 60);
        if (hours < 1)
        {
            chestView.chestStatusText.text = minutes + " : " + seconds;
        }
        else
        {
            chestView.chestStatusText.text = hours+" : "+minutes + " : " + seconds;
        }*/
        int hours = Mathf.FloorToInt(currentTimeInSeconds / 3600);
        int remainingSeconds = Mathf.FloorToInt(currentTimeInSeconds % 3600);
        int minutes = Mathf.FloorToInt(remainingSeconds / 60);
        int seconds = Mathf.FloorToInt(remainingSeconds % 60);

        if (hours > 0)
        {
            chestView.chestStatusText.text = hours + " : " + minutes.ToString("00") + " : " + seconds.ToString("00");
        }
        else
        {
            chestView.chestStatusText.text = minutes + " : " + seconds.ToString("00");
        }
    }
    public void SetChestStatusText(string text)
    {
        chestView.chestStatusText.text = text;
    }
    public int GetOpeningWithGemCost(float remainigTime)
    {
        int costToOpen = Mathf.CeilToInt((remainigTime/60) / 10);
        return costToOpen;
    }
    public void EnableClickingCurrentChest()
    {
        chestView.inputHandler.SetClickStatus(true);
    }
    public void DisableClickingCurrentChest()
    {
        chestView.inputHandler.SetClickStatus(false);
    }
    public void EnableBuyButtonOnChest(bool status)
    {
        chestView.unlockAfterTimerButton.gameObject.SetActive(status);       
    }  
    private void SetBuyButtonTextOnChest(float remainingTime)
    {
        chestView.unlockAfterTimerText.text = "OPEN NOW " + GetOpeningWithGemCost(remainingTime);
    }
    public void DestroyChest()
    {
        Object.Destroy(chestView.gameObject);
    }
    public Transform GetParentTransform()
    {
        return chestView.parentTransform;
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
                chestStateMachine.ChangeState(chestStateMachine.collectedState);
                break;
            case ChestStates.COLLECTED:               
                break;
            default:
                Debug.Log("State Missing");
                break;
        }
    }
    public void SetBuyButtonOnChest()
    {
        int openingCost = GetOpeningWithGemCost(currentTimeInSeconds);
        int playerGems = GameService.Instance.UIService.playerData.playerGems;
        if (playerGems >= openingCost)
        {
            OnSuccesfullBuyWithGems();
            GameService.Instance.UIService.playerData.playerGems -= openingCost;
            GameService.Instance.UIService.SetPlayerUI();
        }
        if(playerGems< openingCost)
        {
            GameService.Instance.PopUpService.DisplayPopUp("NOT ENOUGH GEMS");
        }
    }
    public void OnSuccesfullBuyWithGems()
    {        
        chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);
    }
    public int GetTotalChestTime()
    {
        return chestData.timerInMinutes;
    }
}
