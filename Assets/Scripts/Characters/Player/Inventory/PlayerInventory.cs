using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    public List<RestoreHealthItem> StoredHealItems;
    
    private void Awake()
    {
        StoredHealItems = new List<RestoreHealthItem>();
    }

    public void UseHealItem()
    {
        if (StoredHealItems.Count == 0)
            return;    
        RestoreHealthItem item = StoredHealItems[StoredHealItems.Count + 1];
        item.playerHealth.ChangeCurrentHealthAmount(item.RestoreHealth());
        StoredHealItems.RemoveAt(StoredHealItems.Count);
        UpdateUI();
    }

    public void AddHealItem(RestoreHealthItem item)
    {
        if(StoredHealItems.Count == 4)
            return;
        
        StoredHealItems.Add(item);
        UpdateUI();
        Debug.Log("Current heal items: " + StoredHealItems.Count);
    }

    private void UpdateUI()
    {
        Counter.text = StoredHealItems.Count.ToString();
    }
}
