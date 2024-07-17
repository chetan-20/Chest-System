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
    
    public void Update() => chestStateMachine?.Update();        
    public void StartTimer()=>chestTimer.StartTimer();    
    public void EnableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(true);
    public void DisableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(false);
    public void EnableBuyButtonOnChest(bool status) => chestView.unlockAfterTimerButton.gameObject.SetActive(status);           
    public void EnableUndoButton(bool status) => chestView.undoChestButton.gameObject.SetActive(status);
    public Transform GetParentTransform() => chestView.parentTransform;
    public ChestStates GetCurrentState() => chestData.currentChestState;
    public void SetCurrentChestState(ChestStates state) => chestData.currentChestState = state;
    public bool GetUndoStatus() => chestData.undoPressed;
    public void SetUndoStatus(bool status) => chestData.undoPressed = status;
    public void SetStartTime() => chestData.startTime = Time.time;
    public void SetChestStatusText(string text) => chestUIUpdater.SetChestStatusText(text);
    public void ShowChestData() => chestUIUpdater.ShowChestData();

    public void SetChest()
    {
        chestUIUpdater.SetChestImage();
        chestUIUpdater.SetChestRarity();
    }
    public void OnOpenForFree()
    {
        GameService.Instance.ChestEnablerScript.DisableLockedChests(GameService.Instance.ChestSlotService.GetChestViewList());
        chestStateMachine.ChangeState(chestStateMachine.unlockingState);
    }
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
    public void DestroyChest()
    {
        GameService.Instance.ChestSlotService.UpdateSlot(chestView);
        Object.Destroy(chestView.gameObject);
    }
}
