using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class PlayerHealthComponent : GeneralHealthComponent
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _deathAnimationTrigger;

    [SerializeField] private Slider _healthUI;
    
    public override void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        _onDamageTaken.Invoke();

        if (_healthUI != null)
            _healthUI.value -= dmg * 0.01f; //by percent

        if (_currentHealth <= 0)
        {
            _animator.SetTrigger(_deathAnimationTrigger);
        }
    }

    public override void ReceiveHeal(int healAmount)
    {
        _currentHealth += healAmount;
        
        if (_healthUI != null)
            _healthUI.value += healAmount * 0.01f; //by percent
        
        _onHealReceived.Invoke();
        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }
}
