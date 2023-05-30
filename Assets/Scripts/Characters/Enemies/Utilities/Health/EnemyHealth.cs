using UnityEngine;

public class EnemyHealth : UnitHealth
{
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = gameObject.GetComponentInParent<Animator>();
    }
    
    public override void ChangeCurrentHealthAmount(int amount)
    {
        int newHealth = Health;
        base.ChangeCurrentHealthAmount(amount);
        if(newHealth < Health)
            _animator.SetTrigger("Damage");
    }

    public override void OnZeroHealth()
    {
        _animator.SetTrigger("Die");
    }
}
