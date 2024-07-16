public abstract class IChestStates  
{
    protected ChestController chestController;
    public IChestStates(ChestController chestController)
    {
        this.chestController = chestController;
    }
    public abstract void OnEnterState();
    public abstract void Update();
    public abstract void OnExitState();
}
