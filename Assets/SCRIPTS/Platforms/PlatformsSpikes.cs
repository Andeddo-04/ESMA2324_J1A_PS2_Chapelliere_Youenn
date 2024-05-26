using UnityEngine;

public class PlatformsSpikes : MonoBehaviour
{
    public GameObject spikeGmaeObjet;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 direction = transform.position - player.transform.position;
        float distance = direction.magnitude; // .magnitude permet d'avoir la distance du vecteur, dans le cas ci, c'est la distance entre l'entitée et le joueur
    }
}