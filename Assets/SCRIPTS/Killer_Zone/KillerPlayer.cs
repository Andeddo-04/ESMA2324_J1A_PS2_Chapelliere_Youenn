using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D gameObjectOnCollision)
    {
        if (gameObjectOnCollision.transform.CompareTag("Player"))
        {
            PlayerHealth.instance.Die();
        }
    }
}
