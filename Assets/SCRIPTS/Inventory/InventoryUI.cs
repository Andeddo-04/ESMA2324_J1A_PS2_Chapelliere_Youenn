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
    InventorySlot[] slots; // Tableau de slots d'inventaire
    public InventoryItem selectedItem; // R�f�rence � l'item s�lectionn�

    public static InventoryUI instance; // Instance singleton de InventoryUI

    void Awake()
    {
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
    void UpdateUI()
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
            selectedItemIcon.enabled = false;
            selectedItemDescription.text = "";
            equipButton.gameObject.SetActive(false);
            unequipButton.gameObject.SetActive(false);
        }
    }

    
    // M�thode pour effacer l'affichage des informations de l'item s�lectionn�
    public void ClearSelectedItemDisplay()
    {
        selectedItem = null; // Effacer l'item s�lectionn�
        selectedItemIcon.sprite = null;
        selectedItemIcon.enabled = false;
        selectedItemDescription.text = "";
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    // M�thode pour �quiper l'item s�lectionn�
    public void EquipSelectedItem()
    {
        Debug.Log("Contenant Slot : " + selectedItem);

        if (selectedItem != null && selectedItem.equippable)
        {
            selectedItem.isEquipped = true;
            Debug.Log("Equipped item : " + selectedItem.itemName);

            // D�placer l'item vers un slot d'�quipement libre
            if (equipSlot1.item == null)
            {
                equipSlot1.AddItem(selectedItem, 1);
            }
            else if (equipSlot2.item == null)
            {
                equipSlot2.AddItem(selectedItem, 1);
            }
            DisplaySelectedItem(selectedItem); // Mettre � jour l'affichage

            // Activer/d�sactiver les boutons en fonction de l'�tat de l'item
            equipButton.gameObject.SetActive(false);
            unequipButton.gameObject.SetActive(true);
        }
    }

    // M�thode pour d�s�quiper l'item s�lectionn�
    public void UnequipSelectedItem()
    {
        if (selectedItem != null && selectedItem.equippable)
        {
            selectedItem.isEquipped = false;
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
            DisplaySelectedItem(selectedItem); // Mettre � jour l'affichage

            // Activer/d�sactiver les boutons en fonction de l'�tat de l'item
            equipButton.gameObject.SetActive(true);
            unequipButton.gameObject.SetActive(false);
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

    // M�thode appel�e chaque frame pour d�tecter les clics globaux
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // V�rifiez si le clic ne se trouve pas sur un slot d'item
            if (!IsPointerOverUIObject())
            {
                ClearSelectedItemDisplay(); // Effacer l'affichage si le clic est en dehors des slots d'item
            }
        }
    }

    // M�thode pour v�rifier si le pointeur est au-dessus d'un objet UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Debug.Log(eventDataCurrentPosition.position);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<InventorySlot>() != null)
            {
                return true; // Retourne vrai si le pointeur est sur un slot d'item
            }
        }
        return false; // Retourne faux si le pointeur n'est pas sur un slot d'item
    }

    public void Test()
    {
        Debug.Log("dd");
    }
}
