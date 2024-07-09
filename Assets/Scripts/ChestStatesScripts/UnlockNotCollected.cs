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
