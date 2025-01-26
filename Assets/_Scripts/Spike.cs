using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable dparent = other.GetComponentInParent<IDamageable>();
        IDamageable dchild = other.GetComponentInChildren<IDamageable>();
        if (dparent != null)
        {
            dparent.Damage();
        }
        else if (other.TryGetComponent(out IDamageable d)) 
        {
    	    d.Damage();
    	} 
        else if (dchild != null)
        {
            dchild.Damage();
        }
    }
}
