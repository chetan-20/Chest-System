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
        UIService.Instance.EnableAllChests();
        Debug.Log("Entered Unlock Not Collected");
        UIService.Instance.OnChestTabClose();
    }

    public void OnExitState()
    {
        UIService.Instance.UpdatePlayerCoinsAndGems(chestController);
        UIService.Instance.OnChestTabClose();
    }

    public void Update()
    {       
    }
}
