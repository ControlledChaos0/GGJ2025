using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MantisShrimp : MonoBehaviour, IDamageable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _punchHitBox;
    [SerializeField] private float _punchSize;

    private ShrimpStateMachine _stateMachine;
    private Vector3 _defaultPunchScale;

    public NavMeshAgent Agent
    {
        get => _agent;
    }
    public ShrimpStateMachine StateMachine
    {
        get => _stateMachine;
    }
    public float StalkDistance
    {
        get => 5.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _stateMachine = new ShrimpStateMachine(this);

        _defaultPunchScale = _punchHitBox.transform.localScale;
        _punchHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
        UpdateSprite();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void UpdateSprite()
    {
        _spriteRenderer.flipX = _agent.velocity.x > 0.0f;

        Vector2 dir = PlayerController.Instance.transform.position - transform.position;
        float angle = Vector2.SignedAngle(transform.right, dir);
        _punchHitBox.transform.parent.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    public void Punch()
    {
        StartCoroutine(PunchCoroutine());
    }
    public IEnumerator PunchCoroutine()
    {
        _punchHitBox.SetActive(true);

        while (_punchHitBox.transform.localScale.x < 3f)
        {
            _punchHitBox.transform.localScale += (Vector3.one * 0.25f);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        _punchHitBox.transform.localScale = _defaultPunchScale;
        _punchHitBox.SetActive(false);
    }

    public void Damage()
    {
        throw new System.NotImplementedException();
    }
}
