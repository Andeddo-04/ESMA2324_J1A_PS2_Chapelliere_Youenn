using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float repulseForce = 10f; // Force de repulsion, vous pouvez ajuster cela dans l'éditeur Unity
    public GameObject startPointOfVector;

    private GameObject player;
    private Rigidbody2D playerRB;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calcul de la direction de repulsion
            Vector2 repulseDirection = ((Vector2)startPointOfVector.transform.position - collision.contacts[0].point).normalized;

            // Application de la force de repulsion
            playerRB.AddForce(-repulseDirection * repulseForce, ForceMode2D.Impulse);
        }
    }
}
