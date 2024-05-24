using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    public LayerMask[] layerMasks;

    public string[] tagsToTrack;

    public int damage;
    
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        foreach (var tag in tagsToTrack)
        {
            if (collider.CompareTag(tag))
            {
                MakeDamage(collider.gameObject, tag);
                DestroyArrow();
                return; // Arrêter après avoir infligé des dommages
            }
        }

        foreach (var layerMask in layerMasks)
        {
            if ((layerMask & (1 << collider.gameObject.layer)) != 0)
            {
                DestroyArrow();
            }
        }
        
    }

    void MakeDamage(GameObject target, string tag)
    {
        string healthComponentName = tag + "Health";

        Debug.LogWarning("Health Component == " + healthComponentName);

        var healthComponent = target.GetComponent(healthComponentName);

        Debug.LogWarning("Target == " + target);

        if (healthComponent != null)
        {
            var takeDamageMethod = healthComponent.GetType().GetMethod("TakeDamages");

            Debug.LogWarning("Damage Methode == " + takeDamageMethod);

            if (takeDamageMethod != null)
            {
                takeDamageMethod.Invoke(healthComponent, new object[] { damage });

                Debug.LogWarning("ouh");
            }
        }
    }

    // Appelé lorsque la flèche est activée
    void DestroyArrow()
    { 
        // Détruit la flèche
        Destroy(gameObject);
    }
}
