using UnityEngine;

public class CollectedState : IChestStates
{
    private ChestController chestController;
    public CollectedState(ChestController chestController)
    {
        this.chestController = chestController;
    }
    public void OnEnterState()
    {
        chestController.currentChestState = ChestStates.COLLECTED;      
    }

    public void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
