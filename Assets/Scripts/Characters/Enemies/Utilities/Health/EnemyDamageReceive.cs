using UnityEngine;
using MovementSystem;
public class EnemyDamageReceive : MonoBehaviour
{
    private EnemyHealth _enemyHealth;
    public string weaponTag;
    private void Awake()
    {
        _enemyHealth = gameObject.GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter(Collision other)
    { 
        /*
        Debug.Log("HIT! :" + other.gameObject);
        Debug.Log("HIT! Collider: " + other.collider.gameObject);
        */
        if (other.collider.gameObject.CompareTag(weaponTag))
        {
            var player = other.gameObject.GetComponent<Player>();
            Debug.Log("collider entered: " + other.collider.gameObject.name);
            var weapon = other.collider.gameObject.GetComponent<PlayerWeapon>();
            if (weapon != null && player.MovementStateMachine.ReusableData.IsAttacking)
            {
                _enemyHealth.ChangeCurrentHealthAmount(-weapon._weaponDamage);
                Debug.Log("Enemy received dmg!");
            }
        }
    }
}
