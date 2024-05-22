using System.Collections;
using UnityEngine;


public class ArcherGuardian : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;

    public SpriteRenderer mobSpriteRenderer;

    public BoxCollider2D boxCollider2;

    public Transform[] waypoints;

    public GameObject detectionArea;

    public ArcherGuardianHealth archerGuardianHealth;

    public LayerMask collisionLayer;

    public bool playerIsDetected = false, isFacingRight = false;

    public int damageOnCollision;

    public float speed;

        

    private Transform target;

    private int desPoint;

    //public PlayerHealth playerHealth;

    void Start()
    {
        target = waypoints[0];
    }


    void Update()
    {
        if (archerGuardianHealth.isAlive && playerIsDetected == false)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);


            // Si l'ennemie est quasiment arrivé a sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                desPoint = (desPoint + 1) % waypoints.Length; // % = reste division
                target = waypoints[desPoint];
                mobSpriteRenderer.flipX = !mobSpriteRenderer.flipX;

                //isFacingRight devient son opposé (TRUE ou FALSE)
                //on multiplie le scale X du joueur par -1
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

        else if (archerGuardianHealth.isAlive && playerIsDetected == true)
        {
            return;
        }
    }

    public void DetectPlayer(bool _newvalue)
    {
        playerIsDetected = _newvalue;
        detectionArea.SetActive(false);
    }
            

    

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        playerHealth = collision.transform.GetComponent<playerHealth>();
    //        playerHealth.TakeDamage(10);
    //    }
    //}

}