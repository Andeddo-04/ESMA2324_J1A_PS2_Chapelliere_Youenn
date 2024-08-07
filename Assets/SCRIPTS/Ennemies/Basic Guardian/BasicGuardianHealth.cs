using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicGuardianHealth : MonoBehaviour
{
    //public event Action OnDeath;

    ////////// * Variables publiques * \\\\\\\\\\

    public GameObject basicGuardian;

    public SpriteRenderer graphics;

    public BaseEnemy baseEnemy;

    public int maxhealth = 100, currentHealth;

    public bool isInvicible = false, isAlive = true;

    ////////// * Variables priv�es * \\\\\\\\\\

    //private BasicGuardian basicGuardian;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        //healthBar.SetMaxHealth(maxhealth);
    }

    public void TakeDamages(int _damage)
    {
        if (isAlive)
        {
            currentHealth -= _damage;
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
        basicGuardian.SetActive(false); // Assuming you want to deactivate the enemy
    }

}
