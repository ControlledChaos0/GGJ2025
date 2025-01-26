using System.Collections.Generic;
using UnityEngine;

public class PorcupineStateMachine : StateMachine
{
    // States
    private IdlePorcupineState _idleState;
    private BounceAttackPorcupineState _bounceState;

    private List<IState> _attackList;

    public IdlePorcupineState IdleState
    {
        get => _idleState;
    }
    public BounceAttackPorcupineState BounceState
    {
        get => _bounceState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PorcupineStateMachine(Porcupine porcupine)
    {
        _idleState = new IdlePorcupineState(porcupine);
        _bounceState = new BounceAttackPorcupineState(porcupine);

        GenerateAttackList();
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

    public void GenerateAttackList()
    {
        _attackList = new();
        _attackList.Add(BounceState);
    }

    public void WallHit(Vector2 normal)
    {
        if (currentState == _bounceState)
        {
            _bounceState.WallHit(normal);
        }
    }
}
