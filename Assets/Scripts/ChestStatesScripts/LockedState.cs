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
        Debug.Log("Entered Locked State");
    }

    public void OnExitState()
    {
       
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}


