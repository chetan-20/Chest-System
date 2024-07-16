using UnityEngine;

public class UnlockNotCollected : IChestStates
{    
    public UnlockNotCollected(ChestController chestController) : base(chestController) { }
    
    public override void OnEnterState()
    {
        chestController.SetChestStatusText("UNLOCKED");
        chestController.currentChestState = ChestStates.UNLOCKED;
        GameService.Instance.ChestEnablerScript.EnableAllChests();
        Debug.Log("Entered Unlock Not Collected");
        GameService.Instance.UIService.OnChestTabClose();
        GameService.Instance.UIService.istimerActive = false;       
    }

    public  override    void OnExitState()
    {
       
        GameService.Instance.UIService.OnChestTabClose();
    }

    public override void Update()
    {       
    }
}
