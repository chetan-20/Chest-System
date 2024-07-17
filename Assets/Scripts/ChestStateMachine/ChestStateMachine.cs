
public class ChestStateMachine 
{
    private IChestStates CurrentState;
    public IChestStates lockedState { get; private set; }
    public IChestStates unlockingState { get; private set; }
    public IChestStates unlockNotCollectedState { get; private set; }
    public IChestStates collectedState { get; private set; }
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
    public void CheckCurrentState(ChestStates state)
    {
        switch (state)
        {            
            case ChestStates.LOCKED:
                lockedState.OnEnterState();
                break;
            case ChestStates.UNLOCKING:
                break;
            case ChestStates.UNLOCKED:
                ChangeState(collectedState);
                break;
            case ChestStates.COLLECTED:
                break;
            default:              
                break;
        }
    }
}
