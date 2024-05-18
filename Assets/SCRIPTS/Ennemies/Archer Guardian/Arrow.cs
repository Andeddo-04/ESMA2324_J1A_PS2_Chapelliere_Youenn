using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float destroyDelay = 5f; // Délai avant la destruction de la flèche

    // Appelé lorsque la flèche est activée
    private void OnEnable()
    {
        // Détruit la flèche après un certain délai
        Destroy(gameObject, destroyDelay);
    }
}
