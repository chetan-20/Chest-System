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
        if(chestController.undoPressed)
        {
            GameService.Instance.UIService.EnableAllChests();
            return;
        }
        Debug.Log("Entered Collected State");
        chestController.currentChestState = ChestStates.COLLECTED;
        GameService.Instance.UIService.UpdatePlayerCoinsAndGems(chestController);
        GameService.Instance.UIService.OnChestTabClose();
        OnExitState();
    }

    public void OnExitState()
    {
        GameService.Instance.GenerateChest.MArkSlotEmpty(chestController.GetParentTransform());
        GameService.Instance.UIService.OnChestTabClose();
        chestController.DestroyChest();
    }

    public void Update()
    {
       
    }
}
