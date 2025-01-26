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

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        CoroutineUtils.ExecuteAfterEndOfFrame(Enable, this);
    }

    private void Enable()
    {
        InputController.Instance.Blow += OnBlow;
        InputController.Instance.Point += OnPoint;
    }

    void OnDisable()
    {
        InputController.Instance.Blow -= OnBlow;
        InputController.Instance.Point -= OnPoint;
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
