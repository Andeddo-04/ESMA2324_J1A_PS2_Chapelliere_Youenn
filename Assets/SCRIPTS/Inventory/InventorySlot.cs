// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text countText; // Assurez-vous que ce champ est assigné dans l'inspecteur
    private InventoryItem item;
    private int count;

    public void AddItem(InventoryItem newItem, int itemCount)
    {
        item = newItem;
        count = itemCount;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        countText.text = count > 0 ? "x" + count.ToString() : "";
        countText.enabled = count > 0;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        count = 0;
        countText.text = "";
        countText.enabled = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }
}
