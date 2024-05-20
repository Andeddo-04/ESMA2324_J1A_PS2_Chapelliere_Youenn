using System.Collections;
using UnityEngine;

public class TankyGardianDetectionArea : MonoBehaviour
{
    public TankyGuardian tankyGuardian;

    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // D�tection de l'entr�e du joueur dans la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tankyGuardian.DetectPlayer(true);
        }
    }
}
