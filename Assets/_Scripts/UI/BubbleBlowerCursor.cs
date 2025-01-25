using UnityEngine;

public class BubbleBlowerCursor : Singleton<BubbleBlowerCursor>
{
    public Transform blowerPivot;
    private Animator m_animator;
    // Unity Messages
    private void Awake()
    {
        InitializeSingleton();
        m_animator = GetComponent<Animator>();
        if (blowerPivot == null)
        {
            Debug.LogError("Please Attack Blower Gameobject to blower pivot ref!");
        }
    }
    private void Update()
    {
        transform.position = Input.mousePosition;

        LookAt(PlayerController.Instance.transform);
    }
    private void LookAt(Transform target)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(transform.position);

        Vector2 direction = (target.transform.position - worldPosition).normalized;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion lookRotation = Quaternion.Euler(0, 0, angle);

        blowerPivot.rotation = lookRotation;
    }
    public void OnEnable()
    {
        Cursor.visible = false;
    }
    public void OnDisable()
    {
        Cursor.visible = true;
    }
    public void OnDestroy()
    {
        Cursor.visible = true;
    }
    // On Mouse Actions
    public void OnShoot()
    {
        m_animator.Play("shoot");
    }
    public void OnBlow()
    {
        m_animator.SetTrigger("blowing");
    }
}
