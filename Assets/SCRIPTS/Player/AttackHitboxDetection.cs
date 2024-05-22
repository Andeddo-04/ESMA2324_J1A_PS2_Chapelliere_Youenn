 using UnityEngine;

public class AttackHitboxDetection : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public PlayerMovement playerMovement;

    

    public GameObject attackHitbox;

    public LayerMask ennemiesLayer;

    ////////// * Variables privées * \\\\\\\\\\

    private BasicGuardianHealth basicGuardian;
    private TankyGuardianHealth tankyGuardian;
    private NobleGuardianHealth nobleGuardian;
    private ArcherGuardianHealth archerGuardian;

    ////////// * Méthode Awake() * \\\\\\\\\\

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Basic_Guardian"))
        {
            basicGuardian = collision.transform.GetComponent<BasicGuardianHealth>();
            basicGuardian.TakeDamages();
        }

        if (collision.transform.CompareTag("Tanky_Guardian"))
        {
            tankyGuardian = collision.transform.GetComponent<TankyGuardianHealth>();
            tankyGuardian.TakeDamages();
        }

        if (collision.transform.CompareTag("Noble_Guardian"))
        {
            nobleGuardian = collision.transform.GetComponent<NobleGuardianHealth>();
            nobleGuardian.TakeDamages();
        }

        if (collision.transform.CompareTag("Archer_Guardian"))
        {
            archerGuardian = collision.transform.GetComponent<ArcherGuardianHealth>();
            archerGuardian.TakeDamages();
        }
    }
}