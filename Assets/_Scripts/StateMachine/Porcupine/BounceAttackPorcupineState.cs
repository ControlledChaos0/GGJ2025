using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class BounceAttackPorcupineState : IState
{
    private Porcupine _porcupine;

    private const float ATTACK_TIME = 3.5f;
    private const float WIND_TIME = 3.0f;
    private const float BOUNCE_SPEED = 20.0f;

    private Vector2 _dir;

    private float _currSpeed;
    private float _timeSpeed;

    private bool _speedUp;
    private bool _slowDown;

    public BounceAttackPorcupineState(Porcupine porcupine)
    {
        _porcupine = porcupine;
    }

    public void Enter()
    {
        _porcupine.Agent.enabled = false;
        _porcupine.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _porcupine.ChangeSprite("Ball");

        _currSpeed = 0.0f;
        _timeSpeed = 0.0f;

        _speedUp = true;
        _slowDown = false;

        _dir = (Vector2.right + Vector2.up).normalized;
    }

    public void Exit()
    {
        _porcupine.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _porcupine.Agent.enabled = true;
        _porcupine.SpriteObject.transform.rotation = Quaternion.identity;

        _porcupine.ChangeSprite("Idle");
    }

    public void FixedUpdate()
    {
        if (_speedUp)
        {
            SpeedUp();
        }

        if (_slowDown)
        {
            SpeedDown();
        }

        if (!_speedUp && !_slowDown)
        {
            CountDown();
        }

        Attack();
        RotateSprite();
    }

    public void Update()
    {
    }

    private void SpeedUp()
    {
        _currSpeed = Mathf.Lerp(0.0f, BOUNCE_SPEED, _timeSpeed / WIND_TIME);
        if (_timeSpeed >= WIND_TIME)
        {
            _speedUp = false;
            _timeSpeed = 0.0f;
            return;
        }
        _timeSpeed += Time.deltaTime;
    }

    private void SpeedDown()
    {
        _currSpeed = Mathf.Lerp(BOUNCE_SPEED, 0.0f, _timeSpeed / WIND_TIME);
        if (_timeSpeed >= WIND_TIME)
        {
            _porcupine.StateMachine.Transition(_porcupine.StateMachine.IdleState);
            return;
        }
        _timeSpeed += Time.deltaTime;
    }

    private void CountDown()
    {
        if (_timeSpeed >= ATTACK_TIME)
        {
            _slowDown = true;
            _timeSpeed = 0.0f;
            return;
        }
        _timeSpeed += Time.deltaTime;
    }

    private void Attack()
    {
        _porcupine.Rigidbody.linearVelocity = _dir * _currSpeed;
    }
    private void RotateSprite()
    {
        float neg = _porcupine.Rigidbody.linearVelocityX > 0.0f ? -1.0f : 1.0f;
        _porcupine.SpriteObject.transform.Rotate(_porcupine.transform.forward, neg * 2.0f * _currSpeed);
    }

    public void WallHit(Vector2 normal)
    {
        if (Math.Abs(normal.x) > Math.Abs(normal.y))
        {
            _dir = new Vector2(-_dir.x, _dir.y);
        }
        else
        {
            _dir = new Vector2(_dir.x, -_dir.y);
        }
    }

}
