using Rewired;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryNavigation : MonoBehaviour
{
    private Player player;
    private int playerId = 0;
    private int selectedRowIndex = 0;
    private int selectedColumnIndex = 0;
    private InventorySlot[,] slots;
    private int rows = 6;  // Ajustez selon la configuration de votre inventaire
    private int columns = 5;  // Ajustez selon la configuration de votre inventaire

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        // Initialisation de la grille d'inventaire
        slots = new InventorySlot[rows, columns];
        InventorySlot[] allSlots = InventoryUI.instance.itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                if (index < allSlots.Length)
                {
                    slots[i, j] = allSlots[index];
                }
                else
                {
                    slots[i, j] = null;  // Remplir avec null si hors limites
                }
            }
        }

        HighlightSlot(selectedRowIndex, selectedColumnIndex);
    }

    void Update()
    {
        if (PlayerMovement.instance.useController)
        {
            if (player.GetButtonDown("Controller_NextItem"))
            {
                ChangeSelectedSlot(0, 1);  // Droite
            }
            if (player.GetButtonDown("Controller_PreviousItem"))
            {
                ChangeSelectedSlot(0, -1);  // Gauche
            }
            if (player.GetButtonDown("Controller_NextRow"))
            {
                ChangeSelectedSlot(1, 0);  // Bas
            }
            if (player.GetButtonDown("Controller_PreviousRow"))
            {
                ChangeSelectedSlot(-1, 0);  // Haut
            }
            if (player.GetButtonDown("Controller_SelectItem"))
            {
                InventoryUI.instance.DisplaySelectedItem(slots[selectedRowIndex, selectedColumnIndex].item);
            }
        }
        else
        {
            if (player.GetButtonDown("Mouse_SelectItem")) // Si le joueur appuie sur la touche de sélection d'item
            {
                if (InventoryUI.instance.IsPointerOverUIObject()) // Si la souris est au-dessus d'un objet UI
                {
                    InventorySlot clickedSlot = GetClickedSlotOrButton();

                    if (clickedSlot != null)
                    {
                        InventoryUI.instance.DisplaySelectedItem(clickedSlot.item);
                    }
                    else
                    {
                        InventoryUI.instance.ClearSelectedItemDisplay();
                    }
                }
                else
                {
                    InventoryUI.instance.ClearSelectedItemDisplay(); // Effacer l'affichage si le clic est en dehors des slots d'item
                }
            }
        }
    }


    // Méthode pour obtenir le slot d'inventaire ou le bouton cliqué
    public InventorySlot GetClickedSlotOrButton()
    {
        // Créer un nouvel événement de données de pointeur à la position actuelle de la souris
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Liste pour stocker les résultats du raycast
        List<RaycastResult> results = new List<RaycastResult>();

        // Effectuer un raycast pour détecter les objets sous la position actuelle de la souris
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Parcourir les résultats du raycast
        foreach (var result in results)
        {
            // Vérifier si l'objet de résultat a un composant InventorySlot
            InventorySlot slot = result.gameObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                return slot; // Retourner le slot d'inventaire cliqué
            }

            // Vérifier si l'objet de résultat a un composant Button
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke(); // Appeler l'action du bouton
                return null; // Retourner null car nous avons cliqué sur un bouton
            }
        }
        return null; // Retourner null si aucun slot d'inventaire ou bouton n'a été cliqué
    }



    void ChangeSelectedSlot(int rowChange, int columnChange)
    {
        selectedRowIndex = (selectedRowIndex + rowChange + rows) % rows;
        selectedColumnIndex = (selectedColumnIndex + columnChange + columns) % columns;

        // Vérifier si le nouveau slot sélectionné n'est pas null
        while (slots[selectedRowIndex, selectedColumnIndex] == null)
        {
            selectedRowIndex = (selectedRowIndex + rowChange + rows) % rows;
            selectedColumnIndex = (selectedColumnIndex + columnChange + columns) % columns;
        }

        HighlightSlot(selectedRowIndex, selectedColumnIndex);
    }

    void HighlightSlot(int rowIndex, int columnIndex)
    {
        // Assurez-vous de mettre en place un système visuel pour mettre en évidence le slot sélectionné
        Debug.Log("Slot sélectionné: " + rowIndex + ", " + columnIndex);
    }
}
