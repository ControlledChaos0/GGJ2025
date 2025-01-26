using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class Porcupine : MonoBehaviour
{
    [System.Serializable]
    public struct SpriteItem
    {
        public string name;
        public Sprite sprite;
    }

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _arenaPositions;

    [SerializeField] private WeaponEmitter _weapon;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<SpriteItem> _sprites;

    private PorcupineStateMachine _stateMachine;
    private int health;
    private bool _spawnBoxes;

    public const float COOLDOWN = 7.0f;

    public NavMeshAgent Agent
    {
        get => _agent;
    }
    public Rigidbody2D Rigidbody
    {
        get => _rigidbody;
    }
    public PorcupineStateMachine StateMachine
    {
        get => _stateMachine;
    }

    public GameObject SpriteObject
    {
        get => _spriteRenderer.gameObject;
    }

    void Awake()
    {
        health = 3;
        //Rigidbody.Sleep();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _stateMachine = new PorcupineStateMachine(this);
        _spawnBoxes = false;
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
        UpdateSprite();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void UpdateSprite()
    {
        _spriteRenderer.flipX = (_agent.velocity.x > 0.0f || _rigidbody.linearVelocityX > 0.0f);
    }

    public void ChangeSprite(string str)
    {
        var items = _sprites.Where(x => x.name == str);
        foreach (SpriteItem item in items)
        {
            _spriteRenderer.sprite = item.sprite;
        }
    }

    public Vector2 GetRandArenaPos()
    {
        int rand = Random.Range(0, _arenaPositions.transform.childCount);
        return _arenaPositions.transform.GetChild(rand).position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ContactPoint2D hit = col.GetContact(0);
            Vector2 dir = hit.point - (Vector2)transform.position;
            _stateMachine.WallHit(hit.normal);
        }
    }
}
