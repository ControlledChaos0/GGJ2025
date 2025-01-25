using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IDamageable
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Blower")] 
    [SerializeField] private float _minPower = 1.0f;
    [SerializeField] private float _maxPower = 5.0f;
    [SerializeField] private float _minDistance = 10.0f;
    [SerializeField] private float _maxDistance = 50.0f;

    [Header("Gun")] 
    [SerializeField] private GameObject _gunObject;
    private WeaponEmitter _gunEmitter;
    [SerializeField] private float _gunRecoil = 5;
    [SerializeField] private Weapon _shotgunWeapon;


    private const float Limit = 6.0f;
    private float a;
    private float b;
    void Awake()
    {
        InitializeSingleton();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b = 6 / ((_maxDistance - _minDistance) / 2.0f);
        a = -b * ((_maxDistance + _minDistance) / 2.0f);

        CameraController.Instance.Blow += OnBlow;
        CameraController.Instance.Point += OnPoint;
        InputController.Instance.Shoot += OnShoot;

        _gunEmitter = _gunObject.GetComponentInChildren<WeaponEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBlow(Vector2 worldPos)
    {
        float distance = Vector2.Distance(worldPos, transform.position);
        float sigmoid = CustomMath.Sigmoid((distance * b) + a);
        float pushVal = ((_maxPower - _minPower) * CustomMath.Sigmoid((distance * b) + a)) + _minPower;
        Vector2 dir = ((Vector2)transform.position - worldPos).normalized;
        _rigidbody.AddForce(dir * pushVal);
        BubbleBlowerCursor.Instance.OnBlow();

        //Debug.Log("Distance: " + distance + "; Sigmoid: " + sigmoid + "; PushVal: " + pushVal);
    }

    private void OnPoint(Vector2 worldPos)
    {
        float angle = Vector2.SignedAngle(transform.right, worldPos - (Vector2)transform.position);
        _gunObject.transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    private void OnShoot()
    {
        Vector2 shootDir = _gunObject.transform.right;
        _gunEmitter.Fire(_shotgunWeapon, shootDir);
        _rigidbody.AddForce(_gunRecoil * -shootDir, ForceMode2D.Impulse);
    }

    public void Damage()
    {
        // (Ryan) Very Basic, can be canged to bubble pop effect instead
        // No need to actually destroy gameobject since scene resets!
        gameObject.SetActive(false);

        // (Ryan) Method of letting the LevelManager know the player is dead
        LevelManager.Instance.OnPlayerDeath();
    }
}
