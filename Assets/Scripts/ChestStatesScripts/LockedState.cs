using UnityEngine;

public class LockedState : IChestStates
{
    private ChestController chestController;  
    public LockedState(ChestController chestController) 
    { 
        this.chestController = chestController;       
    }

    public void OnEnterState()
    {
        chestController.currentChestState = ChestStates.LOCKED;
        chestController.ShowChestData();
        chestController.SetChestStatusText();      
        Debug.Log("Entered Locked State");
        chestController.undoPressed = false;
    }

    public void OnExitState()
    {
       
    }

    public void Update()
    {
       
    }
}


