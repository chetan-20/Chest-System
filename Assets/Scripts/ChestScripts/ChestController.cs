using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestController 
{
    public ChestDataSO chestData { get; private set; }
    private ChestView chestView;
    private ChestStateMachine chestStateMachine;
    private ChestUIUpdater chestUIUpdater;  
    public ChestValueCalculator chestValueCalculator {get; private set;}
    public ChestController(ChestDataSO chestDataSO, ChestView chestView)
    {
        this.chestData = chestDataSO;
        this.chestView = chestView;
        chestView.SetViewController(this);
        chestStateMachine = new ChestStateMachine(this);
        chestValueCalculator = new ChestValueCalculator();      
        chestUIUpdater = new ChestUIUpdater(this.chestView,this);        
        SetChest();
        chestStateMachine.Initialize(chestStateMachine.lockedState);
    }
    
    public void Update() => chestStateMachine?.Update();             
    public void EnableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(true);
    public void DisableClickingCurrentChest() => chestView.chestDetailButton.gameObject.SetActive(false);
    public void EnableBuyButtonOnChest(bool status) => chestView.unlockAfterTimerButton.gameObject.SetActive(status);           
    public void EnableUndoButton(bool status) => chestView.undoChestButton.gameObject.SetActive(status);   
    public void SetChestStatusText(string text) => chestUIUpdater.SetChestStatusText(text);  
    public bool GetUndoStatus() => chestData.undoPressed;
    public void SetUndoStatus(bool status) => chestData.undoPressed = status;   
    public TextMeshProUGUI GetUnlockAfterTimerText() => chestView.unlockAfterTimerText;
    public TextMeshProUGUI GetStatusText() => chestView.chestStatusText;
    public ChestStates GetCurrentState() => chestData.currentChestState;
    public void SetCurrentChestState(ChestStates state) => chestData.currentChestState = state;
    public void ShowChestData() => chestUIUpdater.ShowChestData();
    public Button GetBuyonChestButton() => chestView.unlockAfterTimerButton;
    public int GetOpeningCost() => chestValueCalculator.GetInstantOpeningCost(chestData.timerInMinutes);
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
     public void OnSuccesfullBuyWithGems(int openingCost)
     {
         EnableUndoButton(true);
         GameService.Instance.playerData.RemoveGems(openingCost);
         GameService.Instance.UIService.SetPlayerUI();       
         chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);        
     }
     public void InstantBuy()
     {       
        GameService.Instance.playerData.InstantBuy(GetOpeningCost(), this);
     }
     public void UndoChestState()
     {        
         SetUndoStatus(true);
         GameService.Instance.playerData.AddGems(GetOpeningCost());
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
         int randomCoins = chestValueCalculator.GetRandomCoins(chestData.coinsMinRange,chestData.coinsMaxRange);
         int randomGems = chestValueCalculator.GetRandomGems(chestData.gemsMinRange,chestData.gemsMaxRange);
         GameService.Instance.UIService.UpdatePlayerData(randomCoins, randomGems);
     }
    public void DestroyChest()
    {
        GameService.Instance.ChestSlotService.UpdateSlot(chestView);
        Object.Destroy(chestView.gameObject);
    }
}
