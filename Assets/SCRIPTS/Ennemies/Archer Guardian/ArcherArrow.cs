using UnityEngine;

public class ArcherArrow : MonoBehaviour
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
                MakeDamages();
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

    void MakeDamages()
    {
        PlayerHealth.instance.TakeDamage(damage);
        DestroyArrow();
    }
    
    // Appel� lorsque la fl�che est activ�e
    void DestroyArrow()
    { 
        // D�truit la fl�che
        Destroy(gameObject);
    }
}
