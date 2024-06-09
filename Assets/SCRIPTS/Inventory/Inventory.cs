using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<InventoryItem> items = new List<InventoryItem>();
    public Dictionary<int, int> itemCounts = new Dictionary<int, int>();

    public void AddItem(InventoryItem item)
    {
        if (item.stackable)
        {
            if (itemCounts.ContainsKey(item.id))
            {
                itemCounts[item.id]++;
            }
            else
            {
                items.Add(item);
                itemCounts[item.id] = 1;
            }
        }
        else
        {
            items.Add(item);
        }

        Debug.Log("Added item: " + item.itemName);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item.stackable && itemCounts.ContainsKey(item.id))
        {
            itemCounts[item.id]--;
            if (itemCounts[item.id] <= 0)
            {
                itemCounts.Remove(item.id);
                items.Remove(item);
            }
        }
        else
        {
            items.Remove(item);
        }

        Debug.Log("Removed item: " + item.itemName);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool HasItem(InventoryItem item)
    {
        return items.Contains(item);
    }

    public int GetItemCount(InventoryItem item)
    {
        if (itemCounts.ContainsKey(item.id))
        {
            return itemCounts[item.id];
        }
        return 0;
    }

    public InventoryItem GetSelectedItem()
    {
        return InventoryUI.instance.selectedItem;
    }
}
