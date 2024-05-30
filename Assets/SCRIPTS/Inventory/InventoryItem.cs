using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
    public bool stackable;
    public int id;
    public bool equippable; // Indique si l'item peut être équipé
    public bool isEquipped; // Indique si l'item est actuellement équipé
}
