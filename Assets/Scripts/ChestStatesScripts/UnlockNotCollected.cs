using UnityEngine;

public class UnlockNotCollected : IChestStates
{
    private ChestController chestController;
    public UnlockNotCollected(ChestController chestController)
    {
        this.chestController = chestController;
    }
    public void OnEnterState()
    {
        chestController.currentChestState = ChestStates.UNLOCKED;
        GameService.Instance.UIService.EnableAllChests();
        Debug.Log("Entered Unlock Not Collected");
        GameService.Instance.UIService.OnChestTabClose();
    }

    public void OnExitState()
    {
        GameService.Instance.UIService.UpdatePlayerCoinsAndGems(chestController);
        GameService.Instance.UIService.OnChestTabClose();
    }

    public void Update()
    {       
    }
}
