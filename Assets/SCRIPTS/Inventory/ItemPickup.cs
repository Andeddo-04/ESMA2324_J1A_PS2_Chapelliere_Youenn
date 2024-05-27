// Example script to add item
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
