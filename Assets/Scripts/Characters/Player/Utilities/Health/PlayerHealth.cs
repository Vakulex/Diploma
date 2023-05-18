using UnityEngine;

public class PlayerHealth : UnitHealth
{
    public override void OnZeroHealth()
    {
        Debug.Log("Player is dead");
    }
}
