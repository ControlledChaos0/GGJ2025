using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox2D : MonoBehaviour
{ 
    [SerializeField] private HitBox2DType hitType;
    [SerializeField] private LayerMask ignoreLayers;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitType == HitBox2DType.OnlyCollision) return;
        if (collision.TryGetComponent(out IDamageable damageable) && !isIgnoredLayer(collision.gameObject.layer))
        {
            damageable.Damage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitType == HitBox2DType.OnlyTrigger) return;
        if (collision.gameObject.TryGetComponent(out IDamageable damageable) && !isIgnoredLayer(collision.gameObject.layer))
        {
            damageable.Damage();
        }
    }
    /// <summary>
    /// Determines whether the layer index provided is a layer currently ignored
    /// </summary>
    /// <param name="layer">As an integer from 0 to 31 </param>
    /// <returns></returns>
    private bool isIgnoredLayer(int layer)
    {
        return ((1 << layer) & ignoreLayers) != 0;
    }
}

public enum HitBox2DType
{
    OnlyCollision,
    OnlyTrigger,
    Both
}
