using System.Collections;
using UnityEngine;

public class NobleGuardianAttack : MonoBehaviour
{
    public Collider2D guardianCollider;
    public SpriteRenderer guardianSpriteRenderer;
    public GameObject attackHitbox;
    public NobleGuardian nobleGuardian;
    public NobleGuardianHealth nobleGuardianHealth;  // Assurez-vous que ce champ est assign�
    public int baseDamage;
    private int currentDamage;
    public float baseAttackInterval = 3f;
    private float currentAttackInterval;
    public bool canAttack = true;
    public static NobleGuardianAttack instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BasicGuardianAttack dans la sc�ne");
            return;
        }
        instance = this;

        // Assigner nobleGuardianHealth s'il n'est pas d�j� assign�
        if (nobleGuardianHealth == null)
        {
            nobleGuardianHealth = GetComponent<NobleGuardianHealth>();
            if (nobleGuardianHealth == null)
            {
                Debug.LogError("NobleGuardianHealth n'est pas assign� et ne peut pas �tre trouv� sur le GameObject.");
            }
        }
    }

    public void Start()
    {
        if (guardianCollider == null || guardianSpriteRenderer == null || nobleGuardianHealth == null)
        {
            Debug.LogError("Un ou plusieurs composants ne sont pas assign�s.");
            return;
        }

        guardianCollider.isTrigger = true;
        currentDamage = baseDamage;
        currentAttackInterval = baseAttackInterval;
        UpdateStats();
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
        guardianSpriteRenderer.enabled = true;
        PlayerHealth.instance.TakeDamage(currentDamage);
        yield return new WaitForSeconds(0.33f);
        guardianSpriteRenderer.enabled = false;
        yield return new WaitForSeconds(currentAttackInterval);
        canAttack = true;
    }

    public void UpdateStats()
    {
        if (nobleGuardianHealth == null)
        {
            Debug.LogError("NobleGuardianHealth n'est pas assign�.");
            return;
        }

        float healthPercentage = (float)nobleGuardianHealth.currentHealth / nobleGuardianHealth.maxHealth;

        if (healthPercentage <= 0.25f)
        {
            currentDamage = Mathf.FloorToInt(baseDamage * 0.25f);
            currentAttackInterval = baseAttackInterval * 2f;
        }
        else if (healthPercentage <= 0.5f)
        {
            currentDamage = Mathf.FloorToInt(baseDamage * 0.5f);
            currentAttackInterval = baseAttackInterval * 1.5f;
        }
        else if (healthPercentage <= 0.75f)
        {
            currentDamage = Mathf.FloorToInt(baseDamage * 0.75f);
            currentAttackInterval = baseAttackInterval * 1.25f;
        }
        else
        {
            currentDamage = baseDamage;
            currentAttackInterval = baseAttackInterval;
        }
    }
}