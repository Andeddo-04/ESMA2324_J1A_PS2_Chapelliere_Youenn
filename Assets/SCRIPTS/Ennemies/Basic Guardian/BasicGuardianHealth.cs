using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicGuardianHealth : MonoBehaviour, IEnemyHealth
{
    public event Action OnDeath;

    ////////// * Variables publiques * \\\\\\\\\\

    public SpriteRenderer graphics;

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
        Debug.Log($"{gameObject.name} is dying.");
        OnDeath?.Invoke();
        gameObject.SetActive(false); // Assuming you want to deactivate the enemy
    }

}
