using UnityEngine;

public class IdlePorcupineState : IState
{
    private Porcupine _porcupine;

    private bool canTrans;

    public IdlePorcupineState(Porcupine porcupine)
    {
        _porcupine = porcupine;
    }

    public void Enter()
    {
        canTrans = false;
        _porcupine.Agent.SetDestination(_porcupine.GetRandArenaPos());
        CoroutineUtils.ExecuteAfterEndOfFrame(CanTransition, _porcupine);
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (!_porcupine.Agent.hasPath && canTrans)
        {
            _porcupine.StateMachine.Transition(_porcupine.StateMachine.BounceState);
        }
    }

    public void Exit()
    {

    }

    private void CanTransition()
    {
        canTrans = true;
    }

    private void MoveToPlayer()
    {

    }
}
