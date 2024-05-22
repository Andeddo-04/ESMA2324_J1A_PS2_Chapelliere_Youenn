using System.Collections;
using UnityEngine;

public class NobleGuardianAttack : MonoBehaviour
{
    public Collider2D guardianCollider;

    public SpriteRenderer guardianSpriteRenderer;

    public GameObject attackHitbox;

    public NobleGuardian nobleGuardian;

    public int damage;

    public bool canAttack = true;

    public static NobleGuardianAttack instance;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BasicGuardianAttack dans la scène");
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
        if (collision.transform.CompareTag("Player") && playerHealth.instance.isAlive)
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
        
        playerHealth.instance.TakeDamage(damage);

        yield return new WaitForSeconds(0.33f);

        instance.guardianSpriteRenderer.enabled = false;
        
        yield return new WaitForSeconds(3f);

        canAttack = true;
    }
}
