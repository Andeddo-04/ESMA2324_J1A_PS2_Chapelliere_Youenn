using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform initialParent, elevator; // Assignez l'ascenseur via l'inspecteur
    private Transform player; // R�f�rence au joueur

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifiez si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            PlatformsElevator.instance.isPlayerInElevator = true;
            player = other.transform;
            initialParent = player.parent; // Stockez le parent initial
            player.SetParent(elevator);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // V�rifiez si l'objet sortant est le joueur
        if (other.CompareTag("Player"))
        {
            PlatformsElevator.instance.isPlayerInElevator = false;
            player.SetParent(initialParent); // R�affectez le joueur � son parent initial
            player = null;
        }
    }
}
