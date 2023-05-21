using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    public List<RestoreHealthItem> StoredHealItems;
    public int HealAmount = 20;
    private PlayerHealth _playerHealth;
    
    
    private void Awake()
    {
        StoredHealItems = new List<RestoreHealthItem>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void UseHealItem()
    {
        int lastItem = StoredHealItems.FindLastIndex(item => item);
        if(lastItem == -1)
            return;
        HealItem healItem = new HealItem();
        if (StoredHealItems.Count == 0)
            return;    
        _playerHealth.ChangeCurrentHealthAmount(healItem.RestoreHealth(HealAmount));
        StoredHealItems.RemoveAt(lastItem);
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
