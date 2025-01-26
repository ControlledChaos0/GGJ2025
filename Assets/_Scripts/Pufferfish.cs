using UnityEngine;
using System.Collections;

public class Pufferfish : MonoBehaviour, IDamageable
{
    
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private Sprite belowSprite;
    [SerializeField] private Sprite otherSprite;

    private GameObject player; // Change to singleton later
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 dirToPlayer;
    private SpriteRenderer sr;

    // Parameters touched by OnTriggerEnter
    private bool _CheckForPlayer = false;
    private bool _Expanding;
    private bool _FacingBelow;
    private bool _Expanded;
    

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start() {
        player = PlayerController.Instance.gameObject;
        sr.sprite = otherSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Controlling rotation
        gameObject.transform.eulerAngles = new Vector3(0, ((player.transform.position - transform.position).x < 0)? 0 : 180f, 0);
        if (Mathf.Atan(dirToPlayer.y / Mathf.Abs(dirToPlayer.x)) * Mathf.Rad2Deg <  -45f) {
            if (_Expanded) {
                anim.Play("downBig");
            } else {
                anim.Play("down");
            }
            
            // sr.sprite = belowSprite;
        } else {
            if (_Expanded) {
                anim.Play("sideBig");
            } else {
                anim.Play("side");
            }
            // sr.sprite = otherSprite;
        }

        // Control Movement
        dirToPlayer = player.transform.position - transform.position;
        _CheckForPlayer = (Vector3.Distance(player.transform.position, transform.position) <= range);
        if (_CheckForPlayer) {

            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPlayer, Mathf.Infinity, aimMask);
            // Debug.DrawRay(transform.position, dirToPlayer * 10);
            if (ray.collider != null && LayerMask.LayerToName(ray.collider.gameObject.layer).Equals("Player")) {
                MoveTowardsPlayer();
                if (!_Expanding && (Vector3.Distance(player.transform.position, transform.position) <= 4)) {
                    Expand();
                }
            }

        } else if (_Expanding) {
            Shrink();
        }

        // Rotate GameObject
         // If positive, then 
        
        // Debug.Log(Mathf.Atan(dirToPlayer.y / dirToPlayer.x)); 
    }

    void FireRay() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        Physics.Raycast(ray, out hitData);
    }

    private void MoveTowardsPlayer() {
        rb.AddForce(dirToPlayer * speed);
    }

    public void Damage() {
        Debug.Log("Damaged?");
        Destroy(gameObject);
        LevelManager.Instance.OnEnemyDeath();
    }

    private void Expand() {
        anim.Play("Expand");
        _Expanding = true;
        _Expanded = true;
    }

    private void Shrink() {
        anim.Play("Shrink");
        _Expanding = false;
        _Expanded = false;
    }

    private IEnumerator ExpandRoutine() {
        yield return null;
    }

    private IEnumerator ShrinkRoutine() {
        yield return null;
    }
}
