using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElevatorStarter : MonoBehaviour
{
    public Transform elevator; // Assignez l'ascenseur via l'inspecteur
    private Transform player; // R�f�rence au joueur

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifiez si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            PlatformsElevator.instance.canMove = true;
        }
    }
}
