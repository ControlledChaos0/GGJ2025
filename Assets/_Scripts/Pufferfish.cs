using UnityEngine;
using System.Collections;

public class Pufferfish : MonoBehaviour, IDamageable
{
    
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private float speed;

    private GameObject player; // Change to singleton later

    private Rigidbody2D rb;
    private Vector2 dirToPlayer;

    // Parameters touched by OnTriggerEnter
    private bool _CheckForPlayer = false;
    private bool _Expanding;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        player = PlayerController.Instance.gameObject;
        Debug.Log(player);
        Collider2D[] colList = player.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colList) {
            Debug.Log(col);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vector2 hitDir = Vector2.Normalize(player.transform.position - transform.position);
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hitData;
        // Debug.Log("Angle " + transform.forward); 
        dirToPlayer = player.transform.position - transform.position;

        

        if (_CheckForPlayer) {
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dirToPlayer, Mathf.Infinity, aimMask);
            if (LayerMask.LayerToName(ray.collider.gameObject.layer).Equals("Player")) {
                Debug.DrawRay(transform.position, dirToPlayer * 10);
                MoveTowardsPlayer();
            }
            // // if (!_Expanding && Vector3.Distance(player.transform.position, transform.position) <= 2) {
            //     _Expnding = true;
            //     StartCoroutine(ExpandRoutine());
            // } else {
            //     StartCoroutine(Expand)
            // }
            // Debug.Log("The ray hit " + ray.collider.name);

        } else {
            // rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Entered");
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Player")) {
            _CheckForPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exited");
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Player")) {
            _CheckForPlayer = false;
        }
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

    }

    private void Expand() {
        StartCoroutine(ExpandRoutine());
    }

    private void Shrink() {
        StartCoroutine(ShrinkRoutine());
        transform.localScale = new Vector2(1,1);
    }

    private IEnumerator ExpandRoutine() {
        // while ()
        yield return null;
    }

    private IEnumerator ShrinkRoutine() {
        yield return null;
    }
}
