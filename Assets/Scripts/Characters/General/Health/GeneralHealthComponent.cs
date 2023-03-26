using UnityEngine;
using UnityEngine.Events;

public class GeneralHealthComponent : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;

    [SerializeField] protected UnityEvent _onDamageTaken;
    [SerializeField] protected UnityEvent _onHealReceived;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        _onDamageTaken.Invoke();
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ReceiveHeal(int healAmount)
    {
        _currentHealth += healAmount;
        _onHealReceived.Invoke();
        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }
}
