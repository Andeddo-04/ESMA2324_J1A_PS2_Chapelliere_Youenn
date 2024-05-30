using Rewired;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; // Parent des slots d'items dans l'interface utilisateur
    public GameObject inventoryUI; // Référence à l'interface utilisateur de l'inventaire

    public Image selectedItemIcon; // Icône de l'item sélectionné
    public Text selectedItemDescription; // Description de l'item sélectionné
    public Button equipButton; // Bouton pour équiper l'item
    public Button unequipButton; // Bouton pour déséquiper l'item

    public InventorySlot equipSlot1; // Premier slot d'équipement
    public InventorySlot equipSlot2; // Deuxième slot d'équipement

    Inventory inventory; // Référence à l'inventaire
    InventorySlot[] slots; // Tableau de slots d'inventaire

    public InventoryItem selectedItem; // Référence à l'item sélectionné

    public static InventoryUI instance; // Instance singleton de InventoryUI

    Player player; // Référence au joueur
    int playerID = 0; // ID du joueur

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerID); // Obtenir la référence du joueur avec Rewired
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryUI found!");
            return;
        }
        instance = this; // Initialiser l'instance singleton
    }

    void Start()
    {
        inventory = Inventory.instance; // Initialiser la référence à l'inventaire
        inventory.onItemChangedCallback += UpdateUI; // Abonner UpdateUI à l'événement de changement d'item

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // Obtenir les slots enfants
        UpdateUI(); // Mettre à jour l'interface utilisateur initialement

        // Ajouter des écouteurs d'événements aux boutons
        equipButton.onClick.AddListener(EquipSelectedItem);
        unequipButton.onClick.AddListener(UnequipSelectedItem);
        Debug.Log("Écouteurs d'événements ajoutés avec succès");
    }

    // Méthode pour mettre à jour l'interface utilisateur de l'inventaire
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                InventoryItem item = inventory.items[i];
                int itemCount = inventory.itemCounts.ContainsKey(item.id) ? inventory.itemCounts[item.id] : 1;
                slots[i].AddItem(item, itemCount); // Ajouter l'item au slot
            }
            else
            {
                slots[i].ClearSlot(); // Effacer le slot si aucun item n'est présent
            }
        }
        ClearSelectedItemDisplay(); // Effacer l'affichage de l'item sélectionné initialement

        // Mettre à jour l'affichage des slots d'équipement
        UpdateEquipSlots();
    }

    // Méthode pour afficher les informations de l'item sélectionné
    public void DisplaySelectedItem(InventoryItem item)
    {
        selectedItem = item; // Mettre à jour l'item sélectionné

        if (selectedItem != null)
        {
            selectedItemIcon.sprite = item.itemIcon;
            selectedItemIcon.enabled = true;
            selectedItemDescription.text = item.description;

            // Activer/désactiver les boutons en fonction de l'état de l'item
            equipButton.gameObject.SetActive(item.equippable && !item.isEquipped);
            unequipButton.gameObject.SetActive(item.equippable && item.isEquipped);

            Debug.Log("Item Selected : " + selectedItem);
        }
        else
        {
            // Si aucun item n'est sélectionné, désactivez les boutons et cachez les informations de l'item
            ClearSelectedItemDisplay();
        }
    }

    // Méthode pour effacer l'affichage de l'item sélectionné
    public void ClearSelectedItemDisplay()
    {
        Debug.Log("Item Selected was clear");
        selectedItem = null; // Réinitialiser l'item sélectionné
        selectedItemIcon.enabled = false;
        selectedItemDescription.text = "";
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    // Méthode pour équiper l'item sélectionné
    public void EquipSelectedItem()
    {
        Debug.Log("Contenant Slot : " + selectedItem);

        if (selectedItem != null && selectedItem.equippable)
        {
            selectedItem.isEquipped = true; // Marquer l'item comme équipé
            Debug.Log("Equipped item : " + selectedItem.itemName);

            // Déplacer l'item vers un slot d'équipement libre
            if (equipSlot1.item == null)
            {
                equipSlot1.AddItem(selectedItem, 1);
            }
            else if (equipSlot2.item == null)
            {
                equipSlot2.AddItem(selectedItem, 1);
            }
            DisplaySelectedItem(selectedItem); // Mettre à jour l'affichage

            // Activer/désactiver les boutons en fonction de l'état de l'item
            equipButton.gameObject.SetActive(false);
            unequipButton.gameObject.SetActive(true);
        }
    }

    // Méthode pour déséquiper l'item sélectionné
    public void UnequipSelectedItem()
    {
        if (selectedItem != null && selectedItem.equippable)
        {
            selectedItem.isEquipped = false; // Marquer l'item comme déséquipé
            Debug.Log("Unequipped item: " + selectedItem.itemName);

            // Retirer l'item des slots d'équipement
            if (equipSlot1.item == selectedItem)
            {
                equipSlot1.ClearSlot();
            }
            else if (equipSlot2.item == selectedItem)
            {
                equipSlot2.ClearSlot();
            }
            DisplaySelectedItem(selectedItem); // Mettre à jour l'affichage

            // Activer/désactiver les boutons en fonction de l'état de l'item
            equipButton.gameObject.SetActive(true);
            unequipButton.gameObject.SetActive(false);
        }
    }

    // Méthode pour mettre à jour les slots d'équipement
    void UpdateEquipSlots()
    {
        if (equipSlot1.item != null)
        {
            equipSlot1.AddItem(equipSlot1.item, 1);
        }
        else
        {
            equipSlot1.ClearSlot();
        }

        if (equipSlot2.item != null)
        {
            equipSlot2.AddItem(equipSlot2.item, 1);
        }
        else
        {
            equipSlot2.ClearSlot();
        }
    }

    // Méthode pour vérifier si le pointeur est au-dessus d'un objet UI
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<InventorySlot>() != null || result.gameObject.GetComponent<Button>() != null)
            {
                return true; // Retourne vrai si le pointeur est sur un slot d'item ou un bouton
            }
        }
        return false; // Retourne faux si le pointeur n'est pas sur un slot d'item ou un bouton
    }
}
