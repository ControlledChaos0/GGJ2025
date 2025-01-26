using UnityEngine;

public class FanGenerator : MonoBehaviour
{
    
    [SerializeField] private LayerMask wallLayers;
    
    [SerializeField] private Transform effectorTransform;
    private AreaEffector2D effector;
    private ParticleSystem particleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effector = GetComponentInChildren<AreaEffector2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformVector(Vector3.right), Mathf.Infinity, wallLayers);
        if (hit) {
            effectorTransform.localScale = new Vector3(hit.distance, effectorTransform.localScale.y, 1);
            effectorTransform.position = ((Vector3)hit.point + transform.position) / 2;
            if (transform.lossyScale.x < 0) {
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(-s.x, s.y, s.z);
                transform.Rotate(new Vector3(0,0,180));
                // effector.forceMagnitude *= -1;
                // ParticleSystem.ShapeModule shape = particleSystem.shape;
                // shape.rotation = shape.rotation + new Vector3(0,0,180);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
