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
        chestController.SetChestStatusText("UNLOCKED");
        chestController.currentChestState = ChestStates.UNLOCKED;
        GameService.Instance.UIService.EnableAllChests();
        Debug.Log("Entered Unlock Not Collected");
        GameService.Instance.UIService.OnChestTabClose();
        GameService.Instance.UIService.istimerActive = false;       
    }

    public void OnExitState()
    {
       
        GameService.Instance.UIService.OnChestTabClose();
    }

    public void Update()
    {       
    }
}
