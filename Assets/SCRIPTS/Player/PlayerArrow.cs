using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    public LayerMask[] layerMasks;

    public string[] tagsToTrack;

    public int damage;

    ////////// * Variables privées * \\\\\\\\\\

    private BasicGuardianHealth basicGuardian;
    private TankyGuardianHealth tankyGuardian;
    private NobleGuardianHealth nobleGuardian;
    //private ArcherGuardianHealth archerGuardian;

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.CompareTag("Basic_Guardian"))
    //    {
    //        basicGuardian = collision.transform.GetComponent<BasicGuardianHealth>();
    //        basicGuardian.TakeDamages();
    //    }

    //    if (collision.transform.CompareTag("Tanky_Guardian"))
    //    {
    //        tankyGuardian = collision.transform.GetComponent<TankyGuardianHealth>();
    //        tankyGuardian.TakeDamages();
    //    }

    //    if (collision.transform.CompareTag("Noble_Guardian"))
    //    {
    //        nobleGuardian = collision.transform.GetComponent<NobleGuardianHealth>();
    //        nobleGuardian.TakeDamages();
    //    }

    //    if (collision.transform.CompareTag("Archer_Guardian"))
    //    {
    //        archerGuardian = collision.transform.GetComponent<ArcherGuardianHealth>();
    //        archerGuardian.TakeDamages();
    //    }
    //}



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
        tag.TakeDamage(damage);
        DestroyArrow();
    }
    
    // Appelé lorsque la flèche est activée
    void DestroyArrow()
    { 
        // Détruit la flèche
        Destroy(gameObject);
    }
}
