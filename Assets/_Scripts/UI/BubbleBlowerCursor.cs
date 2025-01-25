using UnityEngine;

public class BubbleBlowerCursor : Singleton<BubbleBlowerCursor>
{
    public Transform blowerPivot;
    private Animator m_animator;
    private float blowTime = 0.1f;
    private float blowDuration;
    // Unity Messages
    private void Awake()
    {
        InitializeSingleton();
        m_animator = GetComponent<Animator>();  
    }
    private void Start()
    {
        if (blowerPivot == null)
        {
            Debug.LogError("Please Attach Blower Gameobject to blower pivot ref!");
            return;
        }
    }
    private void Update()
    {
        transform.position = Input.mousePosition;

        LookAt(PlayerController.Instance.transform);

        m_animator.SetBool("blowing", Time.time < blowDuration);
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
        blowDuration = Time.time + blowTime;
    }
}
