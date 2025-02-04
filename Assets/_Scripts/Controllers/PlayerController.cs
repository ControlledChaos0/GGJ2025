using System;
using UnityEngine;
using System.Collections;

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
    [SerializeField] private GameObject _gunSprite;
    private WeaponEmitter _gunEmitter;
    [SerializeField] private float _gunRecoil = 5;
    [SerializeField] private Weapon _shotgunWeapon;
    [SerializeField] private int maxAmmo = 2;
    [SerializeField] private float timeToHideGun = 0.4f;
    private int ammo;
    [SerializeField] private float reloadTime = 2f;
    private bool gunDisabled;

    private const float Limit = 6.0f;
    private float a;
    private float b;

    public Rigidbody2D Rigidbody
    {
        get => _rigidbody;
    }

    private float hidingGunCooldownPoint;
    void Awake()
    {
        InitializeSingleton();
        ammo = maxAmmo;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b = 6 / ((_maxDistance - _minDistance) / 2.0f);
        a = -b * ((_maxDistance + _minDistance) / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _gunSprite.SetActive(Time.time < hidingGunCooldownPoint);
    }

    void OnEnable()
    {
        CoroutineUtils.ExecuteAfterEndOfFrame(Enable, this);
    }

    private void Enable()
    {
        CameraController.Instance.Blow += OnBlow;
        CameraController.Instance.Point += OnPoint;
        InputController.Instance.Shoot += OnShoot;

        if (!gunDisabled)
        {
            UIManager.Instance.AmmoDisplay.Show();
            UIManager.Instance.AmmoDisplay.ChangeAmmo(ammo);
        }

        _gunEmitter = _gunObject.GetComponentInChildren<WeaponEmitter>();
    }

    void OnDisable()
    {
        CameraController.Instance.Blow -= OnBlow;
        CameraController.Instance.Point -= OnPoint;
        InputController.Instance.Shoot -= OnShoot;
    }
    public void EnableGun()
    {
        gunDisabled = false;
        UIManager.Instance.AmmoDisplay.Show();
        UIManager.Instance.AmmoDisplay.ChangeAmmo(ammo);
    }
    public void DisableGun()
    {
        gunDisabled = true;
        UIManager.Instance.AmmoDisplay.Hide();
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
        if (gunDisabled || ammo <= 0) return;

        ammo -= 1;
        UIManager.Instance.AmmoDisplay.ChangeAmmo(ammo);
        Vector2 shootDir = _gunObject.transform.right;
        _gunEmitter.Fire(_shotgunWeapon, shootDir);
        _rigidbody.AddForce(_gunRecoil * -shootDir, ForceMode2D.Impulse);
        BubbleBlowerCursor.Instance.OnShoot();
        if (ammo == 0)
        {
            StartCoroutine(Reload());
        }
        hidingGunCooldownPoint = Time.time + timeToHideGun;
    }

    private IEnumerator Reload() {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        UIManager.Instance.AmmoDisplay.ChangeAmmo(ammo);
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
