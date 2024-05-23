using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Créez la flèche à partir du prefab
        GameObject arrow = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
