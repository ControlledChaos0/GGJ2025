using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;
    protected IState startState;

    // Update is called once per frame
    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void Transition(IState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }

    protected void Initialize(IState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
}
