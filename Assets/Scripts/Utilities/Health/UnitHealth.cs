using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField]
    private int _maxHealth = 100;
    private int health;
    
    public int Health
    {
        get => health;
        set
        {
            health = Math.Clamp(value, 0, _maxHealth);
            if(_healthBar != null)
                _healthBar.value = health;
            Debug.Log(gameObject.name + " Health changed. Current amount: " + health);
        }
    }

    protected virtual void Awake()
    {
        InitHealth();
        Debug.Log("HEALTH: " + Health);
    }

    private void InitHealth()
    {
        Health = _maxHealth;
        if (_healthBar == null) return;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = Health;
    }

    public virtual void SetHealth(int amount)
    {
        _maxHealth = amount;
        Health = _maxHealth;
        Debug.Log("Health amount set: " + _maxHealth);
    }

    public virtual void ChangeCurrentHealthAmount(int amount)
    {
        Health += amount;
        Debug.Log("Health amount changed: " + Health);

        if(Health <= 0)
            OnZeroHealth();
    }

    public virtual void OnZeroHealth()
    {
        
    }
}
