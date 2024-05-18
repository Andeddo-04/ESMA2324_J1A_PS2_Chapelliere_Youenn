using UnityEngine;

public class BasicGuardian : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;

    public SpriteRenderer mobSpriteRenderer;

    public BoxCollider2D boxCollider2;

    public Transform[] waypoints;

    public BasicGuardianHealth basicGuardianHealth;

    public LayerMask collisionLayer;
    
    public float speed;

    public int damageOnCollision;



    private Transform target;

    private int desPoint;

    //public PlayerHealth playerHealth;

    void Start()
    {
        target = waypoints[0];
    }


    void Update()
    {
        if (basicGuardianHealth.isAlive)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);


            // Si l'ennemie est quasiment arrivé a sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                desPoint = (desPoint + 1) % waypoints.Length; // % = reste division
                target = waypoints[desPoint];
                mobSpriteRenderer.flipX = !mobSpriteRenderer.flipX;
            }
        }
    }
}