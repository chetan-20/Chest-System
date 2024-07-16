using UnityEngine;

public class UnlockingState : IChestStates
{
    
    public UnlockingState(ChestController chestController) : base(chestController) { } 
    public override void OnEnterState()
    {
        chestController.currentChestState = ChestStates.UNLOCKING;
        Debug.Log("Entered Unlocking State");
        chestController.startTime = Time.time;
        chestController.EnableBuyButtonOnChest(true);
    }

    public override void OnExitState()
    {
        chestController.EnableBuyButtonOnChest(false);
    }

    public override void Update()
    {
        chestController.StartChestTimer();
    }
}   
