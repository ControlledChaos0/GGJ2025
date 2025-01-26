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
    private bool isDead;

    // Parameters touched by OnTriggerEnter
    private bool _CheckForPlayer = false;
    private bool _FacingBelow;
    

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start() {
        player = PlayerController.Instance.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Controlling rotation
        gameObject.transform.eulerAngles = new Vector3(0, ((player.transform.position - transform.position).x < 0)? 0 : 180f, 0);
        if (Mathf.Atan(dirToPlayer.y / Mathf.Abs(dirToPlayer.x)) * Mathf.Rad2Deg <  -45f) {
            anim.SetBool("FaceSide", false);
        } else {
            anim.SetBool("FaceSide", true);
        }

        // Control Movement
        dirToPlayer = player.transform.position - transform.position;
        _CheckForPlayer = (Vector3.Distance(player.transform.position, transform.position) <= range);
        if (_CheckForPlayer) {

            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPlayer, Mathf.Infinity, aimMask);
            if (ray.collider != null && LayerMask.LayerToName(ray.collider.gameObject.layer).Equals("Player")) {
                MoveTowardsPlayer();
                if (!anim.GetBool("Inflated") && (Vector3.Distance(player.transform.position, transform.position) <= 4)) {
                    Expand();
                }
            }

        } else if (anim.GetBool("Inflated")) {
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
        if (isDead) return;
        isDead = true;
        LevelManager.Instance.OnEnemyDeath();
        Destroy(gameObject);
    }

    private void Expand() {
        anim.Play("Expand");
        anim.SetBool("Inflated", true);
    }

    private void Shrink() {
        anim.Play("Shrink");
        anim.SetBool("Inflated", false);
    }

    private IEnumerator ExpandRoutine() {
        yield return null;
    }

    private IEnumerator ShrinkRoutine() {
        yield return null;
    }
}
