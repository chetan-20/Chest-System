using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockingState : IChestStates
{
    private ChestController chestController;
    public UnlockingState(ChestController chestController) => this.chestController = chestController; 
    public void OnEnterState()
    {
        Debug.Log("Entered Unlocking State");
        chestController.startTime = Time.time;
    }

    public void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        chestController.StartChestTimer();
    }
}   
