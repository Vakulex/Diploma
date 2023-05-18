using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int _weaponDamage = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ChangeCurrentHealthAmount(_weaponDamage);
            Debug.Log(other.gameObject.name + " has been collided with " + gameObject.name);
        }
    }
}
