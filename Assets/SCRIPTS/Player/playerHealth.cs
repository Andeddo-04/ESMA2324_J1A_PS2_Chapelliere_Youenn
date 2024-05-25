using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 100;

    public int currentHealth;

    public bool isInvicible = false, isAlive = true;

    public SpriteRenderer graphics;

    public GameObject crossHair;

    private PlayerHealthbar healthBar;
    
    private GameObject healthBarGameObject;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;

        healthBarGameObject = GameObject.FindGameObjectWithTag("PlayerHealthBar");

        healthBar = healthBarGameObject.GetComponent<PlayerHealthbar>();
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        //healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false;
        //PlayerMovement.instance.animator.SetTrigger("Die");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.characterBoxCollider.enabled = false;

        crossHair.SetActive(false);

        PlayerMovement.instance.rbRenderer.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        //PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.characterBoxCollider.enabled = true;
        PlayerMovement.instance.rbRenderer.enabled = true;

        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.33f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.33f);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        while (isInvicible)
        {
            yield return new WaitForSeconds(3f);
            isInvicible = false;
        }
    }

}
