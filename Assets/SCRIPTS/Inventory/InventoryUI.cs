// InventoryUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    public Image selectedItemIcon;
    public Text selectedItemDescription;

    Inventory inventory;
    InventorySlot[] slots;

    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryUI found!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                InventoryItem item = inventory.items[i];
                int itemCount = inventory.itemCounts.ContainsKey(item.id) ? inventory.itemCounts[item.id] : 1;
                slots[i].AddItem(item, itemCount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void DisplaySelectedItem(InventoryItem item)
    {
        selectedItemIcon.sprite = item.itemIcon;
        selectedItemIcon.enabled = true;
        selectedItemDescription.text = item.description;
    }
}
