using UnityEngine;

public class RestoreHealthItem : MonoBehaviour
{
    public int HealthToRestore = 20;
    public PlayerHealth playerHealth;
    private void OnCollisionEnter(Collision other)
    {
        playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        var inv = other.gameObject.GetComponent<PlayerInventory>();
        if(inv != null)
            inv.AddHealItem(this);
    }

    public int RestoreHealth()
    {
        if (playerHealth != null)
        {
            Debug.Log("Health restored: " + HealthToRestore);
            return HealthToRestore;
        }

        return 0;
    }
}
