using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public ItemType itemType;
    public bool stackable;
    public int id;
    public bool equippable; // Indique si l'item peut être équipé
    public bool isEquipped = false; // Indique si l'item est actuellement équipé

    public InventoryItem()
    {
        isEquipped = false;
    }

    public InventoryItem GetSelectedItem()
    {
        return InventoryUI.instance.selectedItem;
    }
}

public enum ItemType
{
    Weapon,
    Consumable,
    Armor,
    // Ajoutez d'autres types d'objets ici
}