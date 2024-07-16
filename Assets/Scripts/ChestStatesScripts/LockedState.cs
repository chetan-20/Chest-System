using UnityEngine;

public class LockedState : IChestStates
{
    public LockedState(ChestController chestController) : base(chestController) { }      

    public override void OnEnterState()
    {
        chestController.SetCurrentChestState(ChestStates.LOCKED);
        chestController.ShowChestData();
        chestController.SetChestStatusText("LOCKED");             
        chestController.SetUndoStatus(false);
        Debug.Log("Entered Locked State");
    }
    public override void OnExitState() { }  
    public override void Update() { }    
}


