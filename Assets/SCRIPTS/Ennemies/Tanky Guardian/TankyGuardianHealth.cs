using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankyGuardianHealth : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public GameObject tankyGuardian;

    public SpriteRenderer graphics;

    public BaseEnemy baseEnemy;

    public int maxhealth = 100, currentHealth;

    public bool isInvicible = false, isAlive = true;

    ////////// * Variables privées * \\\\\\\\\\

    //private BasicGuardian basicGuardian;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        //healthBar.SetMaxHealth(maxhealth);
    }

    public void TakeDamages()
    {
        if (isAlive)
        {
            currentHealth -= 50;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    public void Die()
    {
        baseEnemy.TriggerOnDeath();
        tankyGuardian.SetActive(false);
    }
}
