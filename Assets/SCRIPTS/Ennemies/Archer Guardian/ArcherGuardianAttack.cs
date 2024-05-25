using System.Collections;
using UnityEngine;

public class ArcherGuardianAttack : MonoBehaviour
{
    public ArcherGuardian archerGuardian;

    public Transform firePoint; // Point de départ de la flèche (par exemple, la main de l'archer)
    
    public GameObject arrowPrefab; // Préfabriqué de la flèche


    private GameObject player;

    public float arrowSpeed = 10f;
    
    private bool isAttacking = false; // Indique si l'archer est en train d'attaquer


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (archerGuardian.playerIsDetected == true)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    public IEnumerator AttackPlayer()
    {
        if (!isAttacking && PlayerHealth.instance.isAlive)
        {
            isAttacking = true;

            while (archerGuardian.playerIsDetected)
            {
                // Créez la flèche à partir du prefab
                GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

                // Calculer la direction vers le joueur
                Vector2 direction = (player.transform.position - firePoint.position).normalized;

                // Obtenez le Rigidbody2D de la flèche pour appliquer la force
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

                // Appliquer une vitesse constante à la flèche
                arrowRb.velocity = direction * arrowSpeed;

                // Aligner la rotation de la flèche avec sa trajectoire
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                yield return new WaitForSeconds(1.75f); // Attendez le cooldown
            }

            isAttacking = false;
        }
    }
}
