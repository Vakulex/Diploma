using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    public List<RestoreHealthItem> StoredHealItems;
    private int _healAmount = 20;
    private PlayerHealth _playerHealth;
    [SerializeField] private RawImage[] _iconHolder;
    
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
        _playerHealth.ChangeCurrentHealthAmount(healItem.RestoreHealth(_healAmount));
        StoredHealItems.RemoveAt(lastItem);
        UpdateUI();
        UpdateItemAmountUI(false);
    }

    public void AddHealItem(RestoreHealthItem item, int healIncome)
    {
        if(StoredHealItems.Count == 4)
            return;
        
        StoredHealItems.Add(item);
        _healAmount = healIncome;
        UpdateUI();
        UpdateItemAmountUI();
        Debug.Log("Current heal items: " + StoredHealItems.Count);
    }

    private void UpdateUI()
    {
        Counter.text = StoredHealItems.Count.ToString();
    }

    private void UpdateItemAmountUI()
    {
        if (StoredHealItems.Count < 0)
            return;
        for (int i = 0; i < StoredHealItems.Count; i++)
        {
            _iconHolder?[i].gameObject.SetActive(true);
        }
    }

    private void UpdateItemAmountUI(bool setIconBool)
    {
        if(StoredHealItems.Count < 0)
            return;
        
        for (int i = _iconHolder.Length-1; i >= 0; i--)
        {
            if (_iconHolder[i].gameObject.activeSelf)
            {
                _iconHolder[i].gameObject.SetActive(setIconBool);
                break;
            }
        }
    }
}
