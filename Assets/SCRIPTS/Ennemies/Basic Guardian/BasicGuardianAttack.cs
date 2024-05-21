using System.Collections;
using UnityEngine;

public class BasicGuardianAttack : MonoBehaviour
{

    private GameObject attackHitbox;

    private BasicGuardian basicGuardian;

    public playerHealth playerHealth;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox = attackHitbox.GetComponent<GameObject>();
        basicGuardian = basicGuardian.GetComponent<BasicGuardian>();
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerHealth = collision.transform.GetComponent<playerHealth>();
            StartCoroutine(AttackProcess());
        }
    }

    public IEnumerator AttackProcess()
    {
        
        basicGuardian.attackHitbox.SetActive(true);
        playerHealth.TakeDamage(damage);
        basicGuardian.attackHitbox.SetActive(false);
        yield return new WaitForSeconds(0.75f);
    }
}
