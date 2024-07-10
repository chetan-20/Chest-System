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
        Debug.Log("Entered Collected State");
        chestController.currentChestState = ChestStates.COLLECTED;
        UIService.Instance.OnChestTabClose();
        OnExitState();
    }

    public void OnExitState()
    {
        UIService.Instance.genChest.MArkSlotEmpty(chestController.GetParentTransform());
        UIService.Instance.OnChestTabClose();
        chestController.DestroyChest();
    }

    public void Update()
    {
       
    }
}
