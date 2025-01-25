using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputController : Singleton<InputController>
{
    [SerializeField]
    private InputActionAsset inputActions;

    [SerializeField] private float _pressTime;

    private InputActionMap _playerControls;

    private InputAction _pointAction;
    private InputAction _shootAction;
    private InputAction _blowAction;

    ////Events
    public event Action<PointerEventData> Click;
    public event Action Hold;
    public event Action Cancel;
    public event Action RightClick;
    public event Action RightHold;
    public event Action RightCancel;

    private void Awake()
    {
        InitializeSingleton();

        SetControls();
        InitializeControls();
    }
    //// Start is called before the first frame update
    void Start()
    {

    }

    //// Update is called once per frame
    void Update()
    {

    }

    private void SetControls()
    {
        _playerControls = inputActions.FindActionMap("Player");

        _blowAction = _playerControls.FindAction("Blow");
        _shootAction = _playerControls.FindAction("Shoot");
        _pointAction = _playerControls.FindAction("Point");
    }

    private void InitializeControls()
    {
        _blowAction.started += OnBlowStarted;
        _blowAction.canceled += OnBlowCanceled;

        _shootAction.performed += OnShootPerformed;
    }

    //TODO
    //Redo RightClick action if needed in line with LeftClick
    private void OnBlowStarted(InputAction.CallbackContext context)
    {

        Debug.Log("Blow Started");
    }
    private void OnBlowPerformed()
    {
        Debug.Log("Blow Performed");
    }
    private void OnBlowCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Blow Canceled");
    }


    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        
    }
}
