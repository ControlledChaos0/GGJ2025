using System.Collections;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwordFish : MonoBehaviour, IDamageable, IPushable
{
    [Header("SwordFish Specs")]
    [SerializeField] private float m_cooldown = 3f;
    [SerializeField] private float m_chargeForce = 6f;
    [SerializeField] private float m_chargeDrag = 1f;
    [SerializeField] private float m_brakingDrag = 5f;
    [SerializeField] private LayerMask aimMask;
    private bool inSight;
    private bool chargeStarted = false;
    private bool inCharge = false;
    private float cooldownTime;
    private Coroutine chargeCoroutine;
    private Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        ChargeDetection();
    }
    private void ChargeDetection()
    {
        if (inCharge) return;
        Vector2 direction = PlayerController.Instance.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, aimMask.value);
        Debug.DrawRay(transform.position, direction);
        if (hit && hit.collider.TryGetComponent(out PlayerController pc))
        {
            Debug.Log(pc.transform);
            LookAt(pc.transform);  
            if (!inSight)
            {
                inSight = true;
                cooldownTime = Time.time + m_cooldown;
            }
            if (!chargeStarted && Time.time > cooldownTime)
            {
                Charge(hit.collider.transform);
            }
        }
        else
        {
            inSight = false;
        }
    }
    public void LookAt(Transform target)
    {
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), Time.deltaTime * 10f);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), 45 * Time.fixedDeltaTime);
    }
    public void Charge(Transform target)
    {
        Debug.Log("Charge Start");
        chargeStarted = true;
        chargeCoroutine = StartCoroutine(ChargeThread(target));
    }
    public void StopCharge()
    {
        if (chargeCoroutine != null) StopCoroutine(chargeCoroutine);
        cooldownTime = Time.time + m_cooldown;
        inCharge = false;
        chargeStarted = false;
    }
    private IEnumerator ChargeThread(Transform target)
    {
        m_rb.linearDamping = m_chargeDrag;
        Vector3 dir = (target.position - transform.position);
        // Pullback
        float time = 0.6f;
        while (time > 0)
        {
            transform.position = Vector2.Lerp(transform.position, transform.position - dir, Time.fixedDeltaTime * m_chargeForce * 0.02f);
            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        inCharge = true;
        //Impulse Charging(v1)
        //dir = target.position - transform.position;
        //m_rb.AddForce(dir / (Time.fixedDeltaTime * m_chargeForce), ForceMode2D.Impulse);
        //Impulse Charging(v2)
        Vector3 targetPos = target.position;
        Vector3 diff = targetPos - transform.position;
        dir = diff.normalized;
        Vector3 vel = dir * m_chargeForce;
        m_rb.AddForce(vel, ForceMode2D.Impulse);
        float t = Vector3.Distance(transform.position, targetPos) / m_chargeForce;
        while (t > 0 && Vector3.Distance(transform.position, targetPos) > 0.01f && m_rb.linearVelocity.magnitude > 0.01f)
        {
            t -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // -------------------old Charging----------------
        //Vector3 targetPos = target.position;
        //while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        //{
        //    transform.position = Vector2.Lerp(transform.position, targetPos, Time.fixedDeltaTime * m_speed);
        //    yield return new WaitForFixedUpdate();
        //}
        //---------------------------------------------------
        Debug.Log("Charge End");
        m_rb.linearDamping = m_brakingDrag;
        cooldownTime = Time.time + m_cooldown;
        inCharge = false;
        chargeStarted = false;
    }
    public void Damage()
    {
        // Basic Function
        Destroy(gameObject);
    }
    public void OnPush()
    {
        StopCharge();
    }
}
