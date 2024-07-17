using UnityEngine;
public class CollectedState : IChestStates
{    
    public CollectedState(ChestController chestController) : base(chestController)
    {
        this.chestController = chestController;
    }
    public override void OnEnterState()
    {
        if(chestController.GetUndoStatus())
        {
            GameService.Instance.ChestEnablerScript.EnableAllChests(GameService.Instance.ChestSlotService.GetChestViewList());
            return;
        }
        Debug.Log("Entered Collected State");
        chestController.SetCurrentChestState(ChestStates.COLLECTED);
        chestController.UpdatePlayerCoinsAndGems();
        GameService.Instance.UIService.OnChestTabClose();
        OnExitState();
    }
    public override void OnExitState()
    {       
        GameService.Instance.UIService.OnChestTabClose();
        chestController.DestroyChest();
    }

    public override void Update()
    {
       
    }
}
