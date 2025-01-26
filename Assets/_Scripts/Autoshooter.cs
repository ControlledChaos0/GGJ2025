using UnityEngine;

public class Autoshooter : MonoBehaviour
{

    private WeaponEmitter weaponEmitter;

    [SerializeField] private Weapon weapon;
    [SerializeField] private Vector2 direction = Vector2.down;

    [SerializeField] private float fireInterval = 4f;
    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponEmitter = GetComponentInChildren<WeaponEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval) {
            timer -= fireInterval;
            weaponEmitter.Fire(weapon, transform.TransformVector(direction));
        }
    }
}
