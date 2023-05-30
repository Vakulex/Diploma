using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : UnitHealth
{
    [SerializeField] private Image _healthBarImage;
    private Color _lerpColor;
    private Color _maxHealthColor;
    private Color _minHealthColor;
    [SerializeField] private string _weaponTag;
    [SerializeField] private UnityEvent _onDeath;

    private void Start()
    {
        _maxHealthColor = new Color(0, 1, 0);
        _minHealthColor = new Color(1, 0, 0);

        UpdateHealthBarColor();
    }
    
    public override void OnZeroHealth()
    {
        Debug.Log("Player is dead");
        _onDeath.Invoke();
    }

    public override void ChangeCurrentHealthAmount(int amount)
    {
        base.ChangeCurrentHealthAmount(amount);
        if(_healthBarImage != null)
            UpdateHealthBarColor();
    }

    private void UpdateHealthBarColor()
    {
        if (_healthBarImage != null)
            return;
        _lerpColor = Color.Lerp(_maxHealthColor, _minHealthColor, Health);
        _healthBarImage.material.color = _lerpColor;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.CompareTag(_weaponTag))
        {
            var player = gameObject.GetComponent<PlayerHealth>();
            Debug.Log("collider entered: " + other.collider.gameObject.name);
            var weapon = other.collider.gameObject.GetComponent<EnemyClaws>();
            player.ChangeCurrentHealthAmount(-weapon._weaponDamage);
            Debug.Log("player received dmg!");
        }
    }
}
