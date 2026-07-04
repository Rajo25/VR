using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 20;

    private bool hasHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        HP_Controller playerHealth = collision.gameObject.GetComponent<HP_Controller>();

        if (playerHealth == null)
            return;

        hasHit = true;

        playerHealth.TakeDamage(damage);

        Destroy(gameObject);
        
        Debug.Log("Kolizja z: " + collision.gameObject.name);
        
    }
    
}