using UnityEngine;

public class RestoreHealthItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var inv = other.gameObject.GetComponent<PlayerInventory>();
        if (inv != null)
        {
            inv.AddHealItem(this);
            gameObject.SetActive(false);
        }
    }
}
