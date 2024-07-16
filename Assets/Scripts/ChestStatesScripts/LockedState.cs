using UnityEngine;

public class LockedState : IChestStates
{
    public LockedState(ChestController chestController) : base(chestController) { }      

    public override void OnEnterState()
    {
        chestController.currentChestState = ChestStates.LOCKED;
        chestController.ShowChestData();
        chestController.SetChestStatusText();      
        Debug.Log("Entered Locked State");
        chestController.undoPressed = false;
    }

    public override void OnExitState()
    {
       
    }

    public override void Update()
    {
       
    }
}


