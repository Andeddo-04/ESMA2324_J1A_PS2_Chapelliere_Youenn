// InventoryUI.cs
using Rewired;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public Transform itemsParent;
    public GameObject inventoryUI;

    public bool inventoryIsActive = false;

    Inventory inventory;
    InventorySlot[] slots;

    private Player player;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI; // Subscribe to the event

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    public void ActiveInventory()
    {
        inventoryIsActive = true;
        inventoryUI.SetActive(true);
    }

    public void DesactivateInventory()
    {
        inventoryIsActive = false;
        inventoryUI.SetActive(false);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
