using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PunchShrimpState : IState
{
    private MantisShrimp _shrimp;
    private float _speed;
    private float _acc;

    private const float Speed = 8f;
    private const float Acceleration = 30f;
    private const float PunchDistance = 1.5f;
    private const float BackUpDistance = 1.0f;
    private const float WaitTime = 0.3f;

    private Vector2 _punchPos;
    private Vector2 _punchDir;
    private float _arriveTime;
    private bool _arrived;
    private bool _backUp;

    public PunchShrimpState(MantisShrimp shrimp)
    {
        _shrimp = shrimp;
    }
    public void Enter()
    {
        _shrimp.Agent.ResetPath();
        _speed = _shrimp.Agent.speed;
        _acc = _shrimp.Agent.acceleration;

        _arriveTime = 0.0f;
        _arrived = false;
        _backUp = true;

        _shrimp.Agent.speed = Speed;
        _shrimp.Agent.acceleration = Acceleration;
        RushToPlayer();
    }

    public void Exit()
    {
        _shrimp.Agent.autoBraking = true;
    }

    public void FixedUpdate()
    {
        
    }

    public void Update()
    {
        if (!_shrimp.Agent.hasPath)
        {
            if (!_arrived)
            {
                _arrived = true;
                CoroutineUtils.ExecuteAfterDelay(Attack, _shrimp, WaitTime);
            } else if (!_backUp)
            {
                _shrimp.Agent.velocity += (Vector3)_punchDir * 10.0f;
                Transition();
            }
        }

        CustomDebug.DrawCircle(_punchPos, 0.5f, 10, Color.red);
    }

    private void RushToPlayer()
    {
        Vector2 shrimpPos = _shrimp.transform.position;
        Vector2 playerPos = PlayerController.Instance.transform.position;
        _punchDir = (shrimpPos - playerPos).normalized;
        Vector2 playerOffset = PlayerController.Instance.Rigidbody.linearVelocity * 0.5f;

        Vector2 punchPos = PunchDistance * _punchDir + playerPos + playerOffset;
        bool blocked = _shrimp.Agent.Raycast(punchPos, out var hit);
        if (blocked)
        {
            _shrimp.StateMachine.Transition(_shrimp.StateMachine.IdleState);
            return;
        }

        _shrimp.Agent.SetDestination(punchPos);
        _punchPos = punchPos;
    }

    private void Attack()
    {
        Debug.Log("Attacked!");
        BackUp();
    }

    private void BackUp()
    {
        Vector2 shrimpPos = _shrimp.transform.position;
        Vector2 newPos = (_punchDir * BackUpDistance) + shrimpPos;

        //_shrimp.Agent.SetDestination(newPos);
        //_shrimp.Agent.autoBraking = false;
        _shrimp.Punch();
        _backUp = false;
        //CoroutineUtils.ExecuteAfterDelay(Transition, _shrimp, 0.5f);
    }

    private void Transition()
    {
        _shrimp.StateMachine.Transition(_shrimp.StateMachine.IdleState);
    }
}
