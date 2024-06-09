using Rewired;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; // Parent des slots d'items dans l'interface utilisateur
    public GameObject inventoryUI; // R�f�rence � l'interface utilisateur de l'inventaire

    public Image selectedItemIcon; // Ic�ne de l'item s�lectionn�
    public Text selectedItemDescription; // Description de l'item s�lectionn�
    public Button equipButton; // Bouton pour �quiper l'item
    public Button unequipButton; // Bouton pour d�s�quiper l'item

    public InventorySlot equipSlot1; // Premier slot d'�quipement
    public InventorySlot equipSlot2; // Deuxi�me slot d'�quipement

    Inventory inventory; // R�f�rence � l'inventaire
    public InventorySlot[] slots; // Tableau de slots d'inventaire

    public InventoryItem selectedItem; // R�f�rence � l'item s�lectionn�

    InventorySlot inventorySlot;

    public static InventoryUI instance; // Instance singleton de InventoryUI

    Player player; // R�f�rence au joueur
    int playerID = 0; // ID du joueur

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerID); // Obtenir la r�f�rence du joueur avec Rewired
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryUI found!");
            return;
        }
        instance = this; // Initialiser l'instance singleton
    }

    void Start()
    {
        inventory = Inventory.instance; // Initialiser la r�f�rence � l'inventaire
        inventory.onItemChangedCallback += UpdateUI; // Abonner UpdateUI � l'�v�nement de changement d'item

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // Obtenir les slots enfants
        UpdateUI(); // Mettre � jour l'interface utilisateur initialement

        // Ajouter des �couteurs d'�v�nements aux boutons
        equipButton.onClick.AddListener(EquipSelectedItem);
        unequipButton.onClick.AddListener(UnequipSelectedItem);
        Debug.Log("�couteurs d'�v�nements ajout�s avec succ�s");
    }

    // M�thode pour mettre � jour l'interface utilisateur de l'inventaire
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
                slots[i].ClearSlot(); // Effacer le slot si aucun item n'est pr�sent
            }
        }
        ClearSelectedItemDisplay(); // Effacer l'affichage de l'item s�lectionn� initialement

        // Mettre � jour l'affichage des slots d'�quipement
        UpdateEquipSlots();
    }

    // M�thode pour afficher les informations de l'item s�lectionn�
    public void DisplaySelectedItem(InventoryItem item)
    {
        selectedItem = item; // Mettre � jour l'item s�lectionn�

        if (selectedItem != null)
        {
            selectedItemIcon.sprite = item.itemIcon;
            selectedItemIcon.enabled = true;
            selectedItemDescription.text = item.description;

            // Activer/d�sactiver les boutons en fonction de l'�tat de l'item
            equipButton.gameObject.SetActive(item.equippable && !item.isEquipped);
            unequipButton.gameObject.SetActive(item.equippable && item.isEquipped);

            Debug.Log("Item Selected : " + selectedItem);
        }
        else
        {
            // Si aucun item n'est s�lectionn�, d�sactivez les boutons et cachez les informations de l'item
            ClearSelectedItemDisplay();
        }
    }

    // M�thode pour effacer l'affichage de l'item s�lectionn�
    public void ClearSelectedItemDisplay()
    {
        Debug.Log("Item Selected was clear");
        selectedItem = null; // R�initialiser l'item s�lectionn�
        selectedItemIcon.enabled = false;
        selectedItemDescription.text = "";
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    // M�thode pour �quiper l'item s�lectionn�
    // M�thode pour �quiper l'item s�lectionn�
    // M�thode pour �quiper l'item s�lectionn�
    public void EquipSelectedItem()
    {
        // V�rifier si un item est s�lectionn� et s'il est �quipable
        if (selectedItem != null && selectedItem.equippable)
        {
            // V�rifier si la halberd est d�j� �quip�e
            if (equipSlot1.item != null && equipSlot1.item.itemName == "Halberd")
            {
                Debug.Log("Cannot equip another item while the Halberd is equipped.");
                return; // Sortir de la m�thode si la halberd est d�j� �quip�e
            }

            // V�rifier si les deux emplacements d'�quipement sont non nuls
            if (equipSlot1.item != null && equipSlot2.item != null)
            {
                Debug.Log("Both equipment slots are occupied. Cannot equip new item.");
                return; // Sortir de la m�thode si les deux emplacements d'�quipement sont occup�s
            }

            // Cas sp�cial : si le joueur ne poss�de qu'un emplacement d'�quipement disponible
            if (equipSlot1.item != null && selectedItem.itemName == "Halberd")
            {
                Debug.Log("Cannot equip Halberd. Only one equipment slot available.");
                return; // Sortir de la m�thode si le joueur poss�de d�j� un emplacement d'�quipement et tente d'�quiper la halberd
            }

            selectedItem.isEquipped = true; // Marquer l'item comme �quip�
            Debug.Log("Equipped item : " + selectedItem.itemName);

            // D�placer l'item vers un slot d'�quipement libre
            if (equipSlot1.item == null)
            {
                equipSlot1.AddItem(selectedItem, 1);

                ActiveWeapon();
            }
            else if (equipSlot2.item == null)
            {
                equipSlot2.AddItem(selectedItem, 1);

                ActiveWeapon();
            }

            // Retirer l'item de l'emplacement de base
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == selectedItem)
                {
                    slot.ClearSlot();
                    break;
                }
            }

            DisplaySelectedItem(selectedItem); // Mettre � jour l'affichage de l'item s�lectionn�
        }
    }



    // M�thode pour d�s�quiper l'item s�lectionn�
    public void UnequipSelectedItem()
    {
        // V�rifier si un item est s�lectionn� et s'il est �quipable
        if (selectedItem != null && selectedItem.equippable)
        {
            selectedItem.isEquipped = false; // Marquer l'item comme non �quip�
            Debug.Log("Unequipped item: " + selectedItem.itemName);

            // Retirer l'item des slots d'�quipement
            if (equipSlot1.item == selectedItem)
            {
                equipSlot1.ClearSlot();
            }
            else if (equipSlot2.item == selectedItem)
            {
                equipSlot2.ClearSlot();
            }

            // Trouver le premier emplacement de base disponible et y ajouter l'item
            InventorySlot firstAvailableSlot = FindFirstAvailableBasicSlot();
            if (firstAvailableSlot != null)
            {
                firstAvailableSlot.AddItem(selectedItem, 1);
            }

            DisplaySelectedItem(selectedItem); // Mettre � jour l'affichage de l'item s�lectionn�
        }
    }



    // M�thode pour mettre � jour les slots d'�quipement
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

    // M�thode pour v�rifier si le pointeur est au-dessus d'un objet UI
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

    // M�thode pour trouver le premier emplacement de base disponible
    public InventorySlot FindFirstAvailableBasicSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            // V�rifie si le slot n'est pas �quip� et s'il n'y a pas d'item dans le slot
            if (slot.item == null)
            {
                return slot;
            }
        }
        return null;
    }

    void ActiveWeapon()
    {
        if (selectedItem.itemName == "Sword")
        {
            AttackController.instance.useSword = true;
        }

        else if (selectedItem.itemName == "Halberd")
        {
            AttackController.instance.useSword = false;
        }

        else if (selectedItem.itemName == "Bow")
        {
            AttackController.instance.useBow = true;
        }
    }
}