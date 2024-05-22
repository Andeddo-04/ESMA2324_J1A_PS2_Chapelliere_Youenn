using System.Collections;
using UnityEngine;

public class TankyGuardianAttack : MonoBehaviour
{
    public Collider2D guardianCollider;

    public SpriteRenderer guardianSpriteRenderer;

    public GameObject attackHitbox;

    public TankyGuardian tankyGuardian;

    public playerHealth playerHealth;

    public int damage;

    public bool canAttack = true;

    public static TankyGuardianAttack instance;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de TankyGuardianAttack dans la scène");
            return;
        }

        instance = this;
    }

    public void Start()
    {
        instance.guardianCollider.isTrigger = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerHealth = collision.transform.GetComponent<playerHealth>();

            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(AttackProcess());
            }
        }
    }

    public IEnumerator AttackProcess()
    {
        instance.guardianSpriteRenderer.enabled = true;

        playerHealth.TakeDamage(damage);

        yield return new WaitForSeconds(0.33f);

        instance.guardianSpriteRenderer.enabled = false;

        yield return new WaitForSeconds(1.5f);

        canAttack = true;
    }
}
