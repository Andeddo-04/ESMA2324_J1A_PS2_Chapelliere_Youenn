// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Text countText; // Assurez-vous que ce champ est assigné dans l'inspecteur
    public InventoryItem item; // Rend cette variable publique
    private int count;

    // Référence à InventoryUI pour signaler la sélection d'un item
    private InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = InventoryUI.instance;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            inventoryUI.DisplaySelectedItem(item);
        }
        else
        {
            Debug.Log("Slot is empty, cannot select item.");
        }
    }

    public void OnRemoveButton()
    {
        if (item != null)
        {
            Inventory.instance.RemoveItem(item);
        }
    }
}
