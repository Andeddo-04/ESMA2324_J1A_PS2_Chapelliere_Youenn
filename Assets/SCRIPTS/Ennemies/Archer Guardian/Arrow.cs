using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float destroyDelay = 5f; // D�lai avant la destruction de la fl�che

    // Appel� lorsque la fl�che est activ�e
    private void OnEnable()
    {
        // D�truit la fl�che apr�s un certain d�lai
        Destroy(gameObject, destroyDelay);
    }
}
