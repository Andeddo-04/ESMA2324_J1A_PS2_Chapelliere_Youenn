using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image icon;
    public Text countText;
    public InventoryItem item;
    private int count;

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
        countText.text = newItem.stackable ? "x" + count.ToString() : "";
        countText.enabled = newItem.stackable && count > 0;
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
            // Ajoutez du code pour désélectionner l'item ici si nécessaire
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // Optionnel : Ajoutez une logique si nécessaire
    }

    public void OnPointerUp(PointerEventData eventData)
    {


    }

    public void OnRemoveButton()
    {
        if (item != null)
        {
            Inventory.instance.RemoveItem(item);
            inventoryUI.ClearSelectedItemDisplay();
        }
    }
}
