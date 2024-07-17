using UnityEngine;

public class ChestController 
{
    private ChestDataSO chestData;
    private ChestView chestView;
    private ChestStateMachine chestStateMachine;
    private ChestUIUpdater chestUIUpdater;
    private ChestTimer chestTimer;
    private ChestValueCalculator chestValueCalculator;

    public ChestController(ChestDataSO chestDataSO, ChestView chestView)
    {
        this.chestData = chestDataSO;
        this.chestView = chestView;
        chestView.SetViewController(this);
        chestStateMachine = new ChestStateMachine(this);
        chestValueCalculator = new ChestValueCalculator(chestData);
        chestTimer = new ChestTimer(chestData, chestStateMachine,this.chestView);
        chestUIUpdater = new ChestUIUpdater(this.chestView, chestData, chestValueCalculator);        
        SetChest();
        chestStateMachine.Initialize(chestStateMachine.lockedState);
    }

   /* public ChestController(ChestDataSO chestDataSO,ChestView chestView)
     {
         this.chestData = chestDataSO;
         this.chestView = chestView;
         chestView.SetViewController(this);       
         chestStateMachine = new ChestStateMachine(this);
         chestStateMachine.Initialize(chestStateMachine.lockedState);
     }*/
     public void SetChest()
     {
         chestUIUpdater.SetChestImage();
         chestUIUpdater.SetChestRarity();               
     }
     public void Update() => chestStateMachine?.Update();    
     //private void SetChestImage() => chestView.chestImage.sprite = chestData.chestSprite;
     //private void SetChestRarity() => chestView.chestTypeText.text = chestData.chestType.ToString();  
     //public int GetTimeLimit() => chestData.timerInMinutes;
     public void StartTimer()=>chestTimer.StartTimer();    
     public void EnableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(true);
     public void DisableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(false);
     public void EnableBuyButtonOnChest(bool status) => chestView.unlockAfterTimerButton.gameObject.SetActive(status);
     //private void SetBuyButtonTextOnChest(float remainingTime) => chestView.unlockAfterTimerText.text = "OPEN NOW " + GetOpeningWithGemCost(remainingTime);
     public void DestroyChest() => Object.Destroy(chestView.gameObject);
     public void OnOpenForFree()
     {
         GameService.Instance.ChestEnablerScript.DisableLockedChests();
         chestStateMachine.ChangeState(chestStateMachine.unlockingState);
     }
     //public void SetChestStatusText(string text) => chestView.chestStatusText.text = text;
    public void EnableUndoButton(bool status) => chestView.undoChestButton.gameObject.SetActive(status);
    public Transform GetParentTransform() => chestView.parentTransform;
    public ChestStates GetCurrentState() => chestData.currentChestState;
    public void SetCurrentChestState(ChestStates state) => chestData.currentChestState = state;
    public bool GetUndoStatus() => chestData.undoPressed;
    public void SetUndoStatus(bool status) => chestData.undoPressed = status;
    public void SetStartTime() => chestData.startTime = Time.time;
    public void SetChestStatusText(string text) => chestUIUpdater.SetChestStatusText(text);
    public void ShowChestData() => chestUIUpdater.ShowChestData();
    /*public int GetOpeningWithGemCost(float remainigTime)
    {
        int costToOpen = Mathf.CeilToInt((remainigTime / 60) / 10);
        return costToOpen;
    }
    public int GetRandomCoins()  
    {
        return Random.Range(chestData.coinsMinRange,chestData.coinsMaxRange);
    }
    public int GetRandomGems()
    {
        return Random.Range(chestData.gemsMinRange,chestData.gemsMaxRange);
    }   
    private string GetGemsRange()
    {
        return ""+chestData.gemsMinRange+"-"+chestData.gemsMaxRange;
    }
    private string GetCoinsRange()
    {
        return "" + chestData.coinsMinRange + "-" + chestData.coinsMaxRange;
    }
    private int GetInstantOpeningCost()
    {
        return GetOpeningWithGemCost(GetTimeLimit() * 60);
    }*/
    /*public void ShowChestData()
    {      
        GameService.Instance.UIService.SetGemsToGainText(GetGemsRange());
        GameService.Instance.UIService.SetCoinsToGainText(GetCoinsRange());
        GameService.Instance.UIService.SetTimeLimitText("Time To Open " + GetTimeLimit() + " Mins");
        GameService.Instance.UIService.SetInstantBuyWithGemsText(GetOpeningWithGemCost(chestData.timerInMinutes*60));
    }*/
    /*public void StartChestTimer()
    {        
        float elapseTime = Time.time - chestData.startTime;
        chestData.currentTimeInSeconds = chestData.timerInMinutes * 60;
        if(chestData.currentTimeInSeconds > 0 )
        {
            chestData.currentTimeInSeconds -= elapseTime;
            UpdateTimerText();
            SetBuyButtonTextOnChest(chestData.currentTimeInSeconds);
        }
        if(chestData.currentTimeInSeconds <= 0)
        {          
            chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);           
        }
    }
    private void UpdateTimerText()
    {       
        int hours = Mathf.FloorToInt(chestData.currentTimeInSeconds / 3600);
        int remainingSeconds = Mathf.FloorToInt(chestData.currentTimeInSeconds % 3600);
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
    }*/

    public void SetBuyButtonOnChest()
     {
         int openingCost = chestValueCalculator.GetOpeningWithGemCost(chestData.currentTimeInSeconds);
         int playerGems = GameService.Instance.playerData.GetPlayerGems();
         if (playerGems >= openingCost)
         {
             OnSuccesfullBuyWithGems(openingCost);                      
         }
         else 
         {
             GameService.Instance.PopUpService.DisplayPopUp("NOT ENOUGH GEMS");
         }
     }
     public void OnSuccesfullBuyWithGems(int openingCost)
     {
         EnableUndoButton(true);
         GameService.Instance.playerData.RemoveGems(openingCost);
         GameService.Instance.UIService.SetPlayerUI();       
         chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);        
     }
     public void InstantBuy()
     {
         int openingCost = chestValueCalculator.GetInstantOpeningCost();      
         if (GameService.Instance.playerData.GetPlayerGems() >= openingCost)
         {
             OnSuccesfullBuyWithGems(openingCost);
         }
         else
         {
             GameService.Instance.PopUpService.DisplayPopUp("NOT ENOUGH GEMS");
         }

     }
     public void UndoChestState()
     {
         int openingCost = chestValueCalculator.GetInstantOpeningCost();
         SetUndoStatus(true);
         GameService.Instance.playerData.AddGems(openingCost);
         GameService.Instance.UIService.SetPlayerUI();
         chestStateMachine.ChangeState(chestStateMachine.lockedState);
         EnableUndoButton(false);
         GameService.Instance.UIService.OnChestTabClose();           
     }
     public void ChestClicked()
     {
         GameService.Instance.UIService.OnChestClick();        
         GameService.Instance.UIService.SetCurrentChestView(chestView);
         chestStateMachine.CheckCurrentState(chestData.currentChestState);
     }         
     public void UpdatePlayerCoinsAndGems()
     {
         int randomCoins = chestValueCalculator.GetRandomCoins();
         int randomGems = chestValueCalculator.GetRandomGems();
         GameService.Instance.PopUpService.DisplayPopUp("+" + randomGems + " Gems" + " +" + randomCoins + " Coins");
         GameService.Instance.playerData.AddCoins(randomCoins);
         GameService.Instance.playerData.AddGems(randomGems);
         GameService.Instance.UIService.SetPlayerUI();
     }
    /*private ChestDataSO chestData;
    private ChestView chestView;
    private ChestStateMachine chestStateMachine;
    private ChestUIUpdater chestUIUpdater;
    private ChestTimer chestTimer;
    private ChestValueCalculator chestCostCalculator;

    public ChestController(ChestDataSO chestDataSO, ChestView chestView)
    {
        this.chestData = chestDataSO;
        this.chestView = chestView;
        chestView.SetViewController(this);
        chestStateMachine = new ChestStateMachine(this);                      
        chestCostCalculator = new ChestValueCalculator(chestData); 
        chestUIUpdater = new ChestUIUpdater(this.chestView,chestData,chestCostCalculator);
        chestTimer = new ChestTimer(chestData,chestStateMachine,chestUIUpdater);
        SetChest();
        chestStateMachine.Initialize(chestStateMachine.lockedState);
    }
    public void Update()
    {
        chestStateMachine?.Update();
        chestTimer?.Update();
    }
    public void ShowChestData() => chestUIUpdater.ShowChestData();   
    public void StartChestTimer() => chestTimer.StartTimer();
    public void EnableUndoButton(bool status) => chestView.undoChestButton.gameObject.SetActive(status);
    public ChestStates GetCurrentState() => chestData.currentChestState;
    public void SetCurrentChestState(ChestStates state) => chestData.currentChestState = state;
    public bool GetUndoStatus() => chestData.undoPressed;
    public void SetUndoStatus(bool status) => chestData.undoPressed = status;
    public void SetStartTime() => chestData.startTime = Time.time;
    public Transform GetParentTransform() => chestView.parentTransform;
    public void EnableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(true);
    public void DisableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(false);
    public void EnableBuyButtonOnChest(bool status) => chestView.unlockAfterTimerButton.gameObject.SetActive(status);      
    public void DestroyChest() => Object.Destroy(chestView.gameObject);
    public void SetChestStatusText(string text) => chestUIUpdater.SetChestStatusText(text);
    public void SetChest()
    {
        chestUIUpdater.SetChestImage();
        chestUIUpdater.SetChestRarity();
    }
    public void SetBuyButtonOnChest()
    {
        int openingCost = chestCostCalculator.GetOpeningWithGemCost(chestData.currentTimeInSeconds);
        int playerGems = GameService.Instance.playerData.GetPlayerGems();
        if (playerGems >= openingCost)
        {
            OnSuccesfullBuyWithGems(openingCost);
        }
        else
        {
            GameService.Instance.PopUpService.DisplayPopUp("NOT ENOUGH GEMS");
        }
    }
    public void OnOpenForFree()
    {
        GameService.Instance.ChestEnablerScript.DisableLockedChests();
        chestStateMachine.ChangeState(chestStateMachine.unlockingState);
    }
    public void OnSuccesfullBuyWithGems(int openingCost)
    {
        GameService.Instance.playerData.RemoveGems(openingCost);
        GameService.Instance.UIService.SetPlayerUI();
        chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);
    }
    public void InstantBuy()
    {
        int openingCost = chestCostCalculator.GetInstantOpeningCost();
        if (GameService.Instance.playerData.GetPlayerGems() >= openingCost)
        {
            OnSuccesfullBuyWithGems(openingCost);
        }
        else
        {
            GameService.Instance.PopUpService.DisplayPopUp("NOT ENOUGH GEMS");
        }
    }
    public void UndoChestState()
    {
        int openingCost = chestCostCalculator.GetInstantOpeningCost();
        SetUndoStatus(true);
        GameService.Instance.playerData.AddGems(openingCost);
        GameService.Instance.UIService.SetPlayerUI();
        chestStateMachine.ChangeState(chestStateMachine.lockedState);
        EnableUndoButton(false);
        GameService.Instance.UIService.OnChestTabClose();
    }
    public void ChestClicked()
    {
        GameService.Instance.UIService.OnChestClick();
        GameService.Instance.UIService.SetCurrentChestView(chestView);
        chestStateMachine.CheckCurrentState(chestData.currentChestState);
    }   
    public void UpdatePlayerCoinsAndGems()
    {
        int randomCoins = chestCostCalculator.GetRandomCoins();
        int randomGems = chestCostCalculator.GetRandomGems();
        GameService.Instance.PopUpService.DisplayPopUp("+" + randomGems + " Gems" + " +" + randomCoins + " Coins");
        GameService.Instance.playerData.AddCoins(randomCoins);
        GameService.Instance.playerData.AddGems(randomGems);
        GameService.Instance.UIService.SetPlayerUI();
    }*/  
}
