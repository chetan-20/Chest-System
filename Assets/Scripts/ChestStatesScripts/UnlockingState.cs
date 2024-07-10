using UnityEngine;

public class UnlockingState : IChestStates
{
    private ChestController chestController;
    public UnlockingState(ChestController chestController) => this.chestController = chestController; 
    public void OnEnterState()
    {
        chestController.currentChestState = ChestStates.UNLOCKING;
        Debug.Log("Entered Unlocking State");
        chestController.startTime = Time.time;
        chestController.EnableClickingCurrentChest();
        chestController.EnableBuyButtonOnChest(true);
    }

    public void OnExitState()
    {
        chestController.EnableBuyButtonOnChest(false);
    }

    public void Update()
    {
        chestController.StartChestTimer();
    }
}   
