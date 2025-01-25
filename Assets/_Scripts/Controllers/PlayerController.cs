using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Blower")] 
    [SerializeField] private float _minPower = 1.0f;
    [SerializeField] private float _maxPower = 5.0f;
    [SerializeField] private float _minDistance = 10.0f;
    [SerializeField] private float _maxDistance = 50.0f;

    [Header("Gun")] 
    [SerializeField] private GameObject _gunObject;

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

        //Debug.Log("Distance: " + distance + "; Sigmoid: " + sigmoid + "; PushVal: " + pushVal);
    }

    private void OnPoint(Vector2 worldPos)
    {
        float angle = Vector2.SignedAngle(transform.right, worldPos - (Vector2)transform.position);
        _gunObject.transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    private void OnShoot()
    {

    }
}
