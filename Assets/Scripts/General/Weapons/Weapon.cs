using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    private BoxCollider Collider;

    private void Start()
    {
        Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.EnemyHealth.TakeDamage(damage);
        }
    }

    public void EnableCollider()
    {
        Collider.enabled = true;
    }
    public void DisableCollider()
    {
        Collider.enabled = false;
    }
}
