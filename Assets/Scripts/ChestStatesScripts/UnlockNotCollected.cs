using UnityEngine;
public class UnlockNotCollected : IChestStates
{    
    public UnlockNotCollected(ChestController chestController) : base(chestController) { }
    
    public override void OnEnterState()
    {
        chestController.SetChestStatusText("UNLOCKED");
        chestController.SetCurrentChestState(ChestStates.UNLOCKED);
        GameService.Instance.ChestEnablerScript.EnableAllChests();        
        GameService.Instance.UIService.OnChestTabClose();
        GameService.Instance.SetTimerStatus(false);
        Debug.Log("Entered Unlock Not Collected");       
    }
    public override void OnExitState()
    {       
        GameService.Instance.UIService.OnChestTabClose();
    }
    public override void Update() { }   
}
