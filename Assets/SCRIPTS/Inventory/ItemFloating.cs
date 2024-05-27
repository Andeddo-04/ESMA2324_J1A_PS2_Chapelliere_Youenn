using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloating : MonoBehaviour
{

    [SerializeField]
    // Amplitude de la fonction sin()
    private float amplitude;

    [SerializeField]
    // position originale de l'item
    private float originalY;

    // Appellée avant la première frame
    void Start()
    {
        // Don't forget where we started
        originalY = transform.position.y;
    }

    // Appellée à chaque frame
    void Update()
    {
        // Occilation suivant la fonction sin()
        float newY = originalY + Mathf.Sin(Time.time) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
