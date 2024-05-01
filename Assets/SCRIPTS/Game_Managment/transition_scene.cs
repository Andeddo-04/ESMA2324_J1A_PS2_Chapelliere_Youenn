using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class transition_scene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {   
            SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
        }
    }
}
