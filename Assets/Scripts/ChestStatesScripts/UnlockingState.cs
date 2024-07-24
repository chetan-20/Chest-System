using UnityEngine;

public class UnlockingState : IChestStates
{
    private ChestTimer chestTimer;
    public UnlockingState(ChestController chestController) : base(chestController) 
    { 
        chestTimer = new ChestTimer();
    } 
    public override void OnEnterState()
    {
        chestController.SetCurrentChestState(ChestStates.UNLOCKING);
        chestTimer.SetStartTime();
        chestController.EnableBuyButtonOnChest(true);
        chestController.GetBuyonChestButton().onClick.AddListener(SetBuyButtonOnChest);
        Debug.Log("Entered Unlocking State");
    }   
    public override void Update()
    {
        chestTimer.StartTimer(chestController.GetStatusText(),chestController.GetUnlockAfterTimerText(),chestController.chestData.timerInMinutes);
        if (chestTimer.currentTimeInSeconds <= 0)
        {
            chestController.SetCurrentChestState(ChestStates.UNLOCKED);
        }
    } 
    public override void OnExitState()
    {
        chestController.EnableBuyButtonOnChest(false);
    }
    public void SetBuyButtonOnChest()
    {
        int openingCost = chestController.chestValueCalculator.GetOpeningWithGemCost(chestTimer.currentTimeInSeconds);
        GameService.Instance.playerData.InstantBuy(openingCost,chestController);
    }
}   
