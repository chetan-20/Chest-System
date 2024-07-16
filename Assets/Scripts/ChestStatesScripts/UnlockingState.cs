using UnityEngine;

public class UnlockingState : IChestStates
{    
    public UnlockingState(ChestController chestController) : base(chestController) { } 
    public override void OnEnterState()
    {
        chestController.SetCurrentChestState(ChestStates.UNLOCKING);       
        chestController.SetStartTime();
        chestController.EnableBuyButtonOnChest(true);
        Debug.Log("Entered Unlocking State");
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
