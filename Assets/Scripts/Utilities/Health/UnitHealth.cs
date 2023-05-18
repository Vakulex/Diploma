using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField]
    private int _maxHealth = 100;
    private int _health;
    
    public int Health
    {
        get => _health;
        set
        {
            _health = Math.Clamp(value, 0, _maxHealth);
            if(_healthBar != null)
                _healthBar.value = _health;
            Debug.Log(gameObject.name + " Health changed. Current amount: " + _health);
        }
    }

    private void Start()
    {
        InitHealth();
    }

    private void InitHealth()
    {
        _health = _maxHealth;
        if (_healthBar == null) return;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _health;
    }

    public virtual void SetHealth(int amount)
    {
        _maxHealth = amount;
        _health = _maxHealth;
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
