using System.Collections;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    public float arrowSpeed = 10f; // Vitesse de la flèche
    public Transform firePoint; // Point de départ de la flèche (par exemple, la main de l'archer)
    public GameObject arrowPrefab; // Préfabriqué de la flèche

    private GameObject player;
    public ArcherGuardian archerGuardian;
    private bool isAttacking = false; // Indique si l'archer est en train d'attaquer

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Détection de l'entrée du joueur dans la zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            archerGuardian.DetectPlayer(true);
            StartCoroutine(AttackPlayer());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            archerGuardian.DetectPlayer(true);
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    // Détection de la sortie du joueur de la zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            archerGuardian.DetectPlayer(false);
        }
    }

    public IEnumerator AttackPlayer()
    {
        if (!isAttacking)
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
