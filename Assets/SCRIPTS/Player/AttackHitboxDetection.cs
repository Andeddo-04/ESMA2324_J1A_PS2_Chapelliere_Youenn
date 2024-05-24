 using UnityEngine;

public class AttackHitboxDetection : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public PlayerMovement playerMovement;

    public int damage;

    public GameObject attackHitbox;

    public LayerMask ennemiesLayer;

    ////////// * Variables privées * \\\\\\\\\\

    private BasicGuardianHealth basicGuardian;
    private TankyGuardianHealth tankyGuardian;
    private NobleGuardianHealth nobleGuardian;
    private ArcherGuardianHealth archerGuardian;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("BasicGuardian"))
        {
            basicGuardian = collision.transform.GetComponent<BasicGuardianHealth>();
            basicGuardian.TakeDamages(damage);
        }

        if (collision.transform.CompareTag("TankyGuardian"))
        {
            tankyGuardian = collision.transform.GetComponent<TankyGuardianHealth>();
            tankyGuardian.TakeDamages(damage);
        }

        if (collision.transform.CompareTag("NobleGuardian"))
        {
            nobleGuardian = collision.transform.GetComponent<NobleGuardianHealth>();
            nobleGuardian.TakeDamages(damage);
        }

        if (collision.transform.CompareTag("ArcherGuardian"))
        {
            archerGuardian = collision.transform.GetComponent<ArcherGuardianHealth>();
            archerGuardian.TakeDamages(damage);
        }
    }
}