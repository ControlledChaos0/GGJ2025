using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputController : Singleton<InputController>
{
    [SerializeField] private InputActionAsset inputActions;

    private InputActionMap _playerControls;

    private InputAction _pauseAction;
    private InputAction _pointAction;
    private InputAction _shootAction;
    private InputAction _blowAction;

    private bool _blowing;

    ////Events
    public event Action<Vector2> Blow;
    public event Action<Vector2> Point;
    public event Action Shoot;

    private void Awake()
    {
        InitializeSingleton();

        SetControls();
    }
    //// Start is called before the first frame update
    void Start()
    {
        _blowing = false; 
    }

    //// Update is called once per frame
    void Update()
    {
        OnPoint();
    }

    void FixedUpdate()
    {
        if (_blowing)
        {
            OnBlowPerformed();
        }
    }
    private void OnEnable()
    {
        _playerControls.Enable();
        ActivateControls();
    }
    private void OnDisable()
    {
        DeactivateControls();
        _playerControls.Disable();
    }
    private void SetControls()
    {
        _playerControls = inputActions.FindActionMap("Player");

        _blowAction = _playerControls.FindAction("Blow");
        _shootAction = _playerControls.FindAction("Shoot");
        _pointAction = _playerControls.FindAction("Point");

        _pauseAction = _playerControls.FindAction("Pause");
    }

    private void ActivateControls()
    {
        _blowAction.started += OnBlowStarted;
        _blowAction.canceled += OnBlowCanceled;

        _shootAction.performed += OnShootPerformed;
        _pauseAction.performed += OnPause;
    }

    private void DeactivateControls()
    {
        _blowAction.started -= OnBlowStarted;
        _blowAction.canceled -= OnBlowCanceled;

        _shootAction.performed -= OnShootPerformed;
        _pauseAction.performed -= OnPause;
    }

    //TODO
    //Redo RightClick action if needed in line with LeftClick
    private void OnBlowStarted(InputAction.CallbackContext context)
    {
        _blowing = true;
        Debug.Log("Blow Started");
    }
    private void OnBlowPerformed()
    {
        Vector2 screenPos = _pointAction.ReadValue<Vector2>();
        Blow?.Invoke(screenPos);
    }
    private void OnBlowCanceled(InputAction.CallbackContext context)
    {
        _blowing = false;
        Debug.Log("Blow Canceled");
    }
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Shoot?.Invoke();
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        LevelManager.Instance.TogglePause();
    }
    private void OnPoint()
    {
        Vector2 screenPos = _pointAction.ReadValue<Vector2>();
        Point?.Invoke(screenPos);
    }

}