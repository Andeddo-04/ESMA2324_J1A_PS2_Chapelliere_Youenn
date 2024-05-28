using UnityEngine;

public class BasicGuardian : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;
    public SpriteRenderer mobSpriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Transform[] waypoints;
    public BasicGuardianHealth basicGuardianHealth;
    public GameObject detectionArea;
    public LayerMask collisionLayer, hiddenPlayerLayer;
    public float speed, distanceToStop;

    private GameObject player;
    private Transform target;
    private int desPoint;
    private bool playerIsDetected = false;
    private bool isFacingRight = false;

    void Start()
    {
        target = waypoints[0];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (basicGuardianHealth.isAlive && !playerIsDetected)
        {
            MoveToPoint();
        }
        else if (basicGuardianHealth.isAlive && playerIsDetected)
        {
            FollowPlayer();
        }
    }

    void MoveToPoint()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            desPoint = (desPoint + 1) % waypoints.Length;
            target = waypoints[desPoint];
            Flip(direction.x);
        }
    }

    void FollowPlayer()
    {
        float playerPositionX = player.transform.position.x;
        float enemyPositionX = transform.position.x;
        float distance = Mathf.Abs(playerPositionX - enemyPositionX);

        if (distance > distanceToStop)
        {
            float directionX = playerPositionX > enemyPositionX ? 1f : -1f;
            mobRigidbody.velocity = new Vector2(directionX * speed, mobRigidbody.velocity.y);
            Flip(directionX);
        }
        else
        {
            mobRigidbody.velocity = new Vector2(0, mobRigidbody.velocity.y);
        }
    }

    void Flip(float directionX)
    {
        if ((directionX > 0 && isFacingRight) || (directionX < 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void DetectPlayer(bool newValue)
    {
        playerIsDetected = newValue;
        detectionArea.SetActive(!newValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si le joueur est caché et si la collision est avec un objet sur le layer caché
        if (HideHimSelf.instance.IsPlayerHidden() && ((1 << collision.gameObject.layer) & hiddenPlayerLayer) != 0)
        {
            // Ignore la collision entre l'objet avec lequel le joueur entre en collision et le BoxCollider de ce script
            Physics2D.IgnoreCollision(collision.collider, boxCollider2D, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Vérifie si le joueur n'est plus caché et si la collision est avec un objet sur le layer caché
        if (!HideHimSelf.instance.IsPlayerHidden() && ((1 << collision.gameObject.layer) & hiddenPlayerLayer) != 0)
        {
            // Réactive la collision entre l'objet avec lequel le joueur sort de collision et le BoxCollider de ce script
            Physics2D.IgnoreCollision(collision.collider, boxCollider2D, false);
        }
    }

}
