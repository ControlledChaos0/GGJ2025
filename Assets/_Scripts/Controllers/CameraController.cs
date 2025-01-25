using System;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField]
    private Camera _camera;

    public event Action<Vector2> Blow;
    public event Action<Vector2> Point;
    void Awake()
    {
        InitializeSingleton();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputController.Instance.Blow += OnBlow;
        InputController.Instance.Point += OnPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBlow(Vector2 screenPos)
    {
        Vector2 pos = _camera.ScreenToWorldPoint(screenPos);
        Blow?.Invoke(pos);
    }

    private void OnPoint(Vector2 screenPos)
    {
        Vector2 pos = _camera.ScreenToWorldPoint(screenPos);
        Point?.Invoke(pos);
    }
}
