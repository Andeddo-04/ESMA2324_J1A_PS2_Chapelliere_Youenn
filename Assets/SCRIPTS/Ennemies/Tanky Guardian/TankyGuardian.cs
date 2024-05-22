using UnityEngine;

public class TankyGuardian : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;

    public SpriteRenderer mobSpriteRenderer;

    public BoxCollider2D boxCollider2D;

    public Transform[] waypoints;

    public TankyGuardianHealth tankyGuardianHealth;

    public GameObject detectionArea;

    public LayerMask collisionLayer;

    public float speed, distanceToStop;


    private GameObject player;

    private Transform target;

    private int desPoint;

    private bool playerIsDetected = false;

    void Start()
    {
        target = waypoints[0];
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (tankyGuardianHealth.isAlive && playerIsDetected == false)
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

        else if (tankyGuardianHealth.isAlive && playerIsDetected)
        {
            float playerPositionX = player.transform.position.x;
            float enemyPositionX = transform.position.x;
            float distance = Mathf.Abs(playerPositionX - enemyPositionX); // Distance entre l'ennemi et le joueur sur l'axe X

            if (distance > distanceToStop)
            {
                // Déterminer la direction du mouvement sur l'axe X
                float directionX = playerPositionX > enemyPositionX ? 1f : -1f;

                // Déplacer l'ennemi vers le joueur
                mobRigidbody.velocity = new Vector2(directionX * speed, mobRigidbody.velocity.y);

                // Flip du sprite de l'ennemi en fonction de la direction
                if (directionX > 0 && !mobSpriteRenderer.flipX)
                {
                    mobSpriteRenderer.flipX = true;
                }
                else if (directionX < 0 && mobSpriteRenderer.flipX)
                {
                    mobSpriteRenderer.flipX = false;
                }
            }
            else
            {
                // Arrêter l'ennemi quand il est à la distance désirée
                mobRigidbody.velocity = new Vector2(0, mobRigidbody.velocity.y);
            }
        }
    }

    public void DetectPlayer(bool _newvalue)
    {
        playerIsDetected = _newvalue;
        detectionArea.SetActive(false);
        
    }

}