using UnityEngine;

public class ShrimpStateMachine : StateMachine
{
    // States
    private IdleShrimpState _idleState;
    private PunchShrimpState _punchState;
    private ShockShrimpState _shockState;

    public IdleShrimpState IdleState
    {
        get => _idleState;
    }
    public PunchShrimpState PunchState
    {
        get => _punchState;
    }
    public ShockShrimpState ShockState
    {
        get => _shockState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ShrimpStateMachine(MantisShrimp shrimp)
    {
        _idleState = new IdleShrimpState(shrimp);
        _punchState = new PunchShrimpState(shrimp);
        _shockState = new ShockShrimpState(shrimp);

        Initialize(_idleState);
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
    }

    public void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
