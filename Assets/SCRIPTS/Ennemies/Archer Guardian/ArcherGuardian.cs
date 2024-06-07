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

            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                desPoint = (desPoint + 1) % waypoints.Length;
                target = waypoints[desPoint];
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
}