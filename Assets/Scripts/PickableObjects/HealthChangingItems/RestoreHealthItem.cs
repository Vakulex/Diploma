using UnityEngine;

public class RestoreHealthItem : MonoBehaviour
{
    public int HealAmount = 20;
    private void OnCollisionEnter(Collision other)
    {
        var inv = other.gameObject.GetComponent<PlayerInventory>();
        if (inv != null)
        {
            inv.AddHealItem(this, HealAmount);
            gameObject.SetActive(false);
        }
    }
}
