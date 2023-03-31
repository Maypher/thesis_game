

// New implementation from https://www.youtube.com/watch?v=GB1Ri_SRtZY&list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&index=15
public class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
