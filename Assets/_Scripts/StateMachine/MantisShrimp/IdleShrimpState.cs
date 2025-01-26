using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleShrimpState : IState
{
    private MantisShrimp _shrimp;
    private Vector2 _lastPlayerPos;

    private float _stalkTime;
    private float _currentOdds;

    private float _normalizeTime;
    private float _currSpeed;
    private float _currAcc;

    private const float TimeBetweenChecksFar = 1.0f;
    private const float TimeBetweenChecksClose = 0.5f;
    private const float CoinFlipMult = 1.15f;

    private const float Speed = 5f;
    private const float Acceleration = 15.0f;
    private const float TimeToNormalize = 1.5f;

    public IdleShrimpState(MantisShrimp shrimp)
    {
        _shrimp = shrimp;
    }

    public void Enter()
    {
        _normalizeTime = 0.0f;
        _currSpeed = _shrimp.Agent.speed;
        _currAcc = _shrimp.Agent.acceleration;

        _currentOdds = 1.0f;
        Stalk();
    }
    public void Exit()
    {
        
    }
    public void FixedUpdate()
    {
        if (_normalizeTime < TimeToNormalize)
        {
            _shrimp.Agent.speed = Mathf.Lerp(_currSpeed, Speed, _normalizeTime / TimeToNormalize);
            _shrimp.Agent.acceleration = Mathf.Lerp(_currAcc, Acceleration, _normalizeTime / TimeToNormalize);
            _normalizeTime += Time.deltaTime;
            _normalizeTime = _normalizeTime > TimeToNormalize ? TimeToNormalize : _normalizeTime;
        }
        else
        {
            _shrimp.Agent.speed = Speed;
            _shrimp.Agent.acceleration = Acceleration;
        }
    }
    
    public void Update()
    {
        float timeBetweenChecks = Vector2.Distance(PlayerController.Instance.transform.position, _shrimp.transform.position) > _shrimp.StalkDistance + 1.0f
            ? TimeBetweenChecksFar : TimeBetweenChecksClose;
        if (Time.time - _stalkTime > timeBetweenChecks || !_shrimp.Agent.hasPath)
        {
            if (!CoinFlip())
            {
                Stalk();
            }
        }
    }

    private void Stalk()
    {
        _lastPlayerPos = PlayerController.Instance.transform.position;
        _stalkTime = Time.time;
        Vector2 shrimpPos = _shrimp.transform.position;
        float dist = Vector2.Distance(_lastPlayerPos, shrimpPos);
        Vector2 dir = (shrimpPos - _lastPlayerPos).normalized;

        if (dist <= _shrimp.StalkDistance + 1.0f)
        {
            bool left = Random.Range(0, 2) >= 1;
            float angle = left ? 60 : -60;
            dir = dir.Rotate(angle);
        }

        Vector2 stalkPos = (_shrimp.StalkDistance * dir) + _lastPlayerPos;
        Collider2D collider = Physics2D.OverlapCircle(stalkPos, _shrimp.Agent.radius, LayerMask.NameToLayer("Wall"));
        if (collider != null)
        {
            Vector2 closestColPos = collider.ClosestPoint(stalkPos);
            stalkPos = ((stalkPos - closestColPos).normalized * _shrimp.Agent.radius) + closestColPos;
        }

        _shrimp.Agent.SetDestination(stalkPos);

        Debug.Log("Stalk!");
    }

    private bool CoinFlip()
    {
        bool transition = Random.Range(0, _currentOdds) >= 1;
        if (transition)
        {
            _shrimp.StateMachine.Transition(_shrimp.StateMachine.PunchState);
        }
        else
        {
            _currentOdds *= CoinFlipMult;
        }
        return transition;
    }
}
