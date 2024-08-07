using System.Collections;
using UnityEngine;

public class BasicGuardianAttack : MonoBehaviour
{
    public Collider2D guardianCollider;

    public SpriteRenderer guardianSpriteRenderer;

    public GameObject attackHitbox;

    public BasicGuardian basicGuardian;

    public int damage;

    public bool canAttack = true;

    public static BasicGuardianAttack instance;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BasicGuardianAttack dans la sc�ne");
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
        if (collision.transform.CompareTag("Player") && PlayerHealth.instance.isAlive && PlayerMovement.instance.canBeDetected)
        {
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
        
        PlayerHealth.instance.TakeDamage(damage);

        yield return new WaitForSeconds(0.33f);

        instance.guardianSpriteRenderer.enabled = false;
        
        yield return new WaitForSeconds(1.5f);

        canAttack = true;
    }
}
