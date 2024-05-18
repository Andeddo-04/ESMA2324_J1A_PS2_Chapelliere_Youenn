using UnityEngine;

public class BasicGuardianAttack : MonoBehaviour
{

    private GameObject attackHitbox;

    public playerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox = attackHitbox.GetComponent<GameObject>();
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerHealth = collision.transform.GetComponent<playerHealth>();
            playerHealth.TakeDamage(10);
        }
    }
}
