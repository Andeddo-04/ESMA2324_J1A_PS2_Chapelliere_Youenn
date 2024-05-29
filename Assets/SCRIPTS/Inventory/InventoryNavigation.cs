using UnityEngine;
using Rewired;

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
            if (player.GetButtonDown("Mouse_SelectItem"))
            {
                InventoryUI.instance.DisplaySelectedItem(slots[selectedRowIndex, selectedColumnIndex].item);
            }
        }
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
