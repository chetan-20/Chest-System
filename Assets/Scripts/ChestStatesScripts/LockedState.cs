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
        chestController.ShowChestData();
        Debug.Log("Entered Locked State");
    }

    public void OnExitState()
    {
        Debug.Log("Exiting Locked State");
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}


