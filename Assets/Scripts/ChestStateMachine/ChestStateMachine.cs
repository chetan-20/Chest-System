
public class ChestStateMachine 
{
    public IChestStates CurrentState { get; private set; }
    public IChestStates lockedState;
    public IChestStates unlockingState;
    public IChestStates unlockNotCollectedState;
    public IChestStates collectedState;
    public ChestStateMachine(ChestController chestController)
    {
        CreateStates(chestController);
    }
    public void Initialize(IChestStates state)
    {
        CurrentState = state;
        CurrentState?.OnEnterState();
    }
    private void CreateStates(ChestController chestController)
    {
        lockedState = new LockedState(chestController);
        unlockingState = new UnlockingState(chestController);
        unlockNotCollectedState = new UnlockNotCollected(chestController);
        collectedState = new CollectedState(chestController);
    }
    public void Update() => CurrentState?.Update();
    public void ChangeState(IChestStates nextState)
    {
        CurrentState?.OnExitState();
        CurrentState = nextState;
        CurrentState?.OnEnterState();
    }
}
