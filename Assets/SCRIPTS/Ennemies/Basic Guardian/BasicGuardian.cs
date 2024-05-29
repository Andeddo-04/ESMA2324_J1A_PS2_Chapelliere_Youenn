using UnityEngine;

public class BasicGuardian : MonoBehaviour
{
    public Rigidbody2D mobRigidbody;
    public SpriteRenderer mobSpriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Transform[] waypoints;
    public BasicGuardianHealth basicGuardianHealth;
    public GameObject detectionArea;
    public float speed, distanceToStop;



    private GameObject player;
    private Transform target;
    private int desPoint;
    private bool playerIsDetected = false, isFacingRight = false;
    private string hiddenLayerName = "HiddenPlayer";
    private int hiddenLayer;

    void Start()
    {
        target = waypoints[0];
        player = GameObject.FindGameObjectWithTag("Player");
        hiddenLayer = LayerMask.NameToLayer(hiddenLayerName);
    }

    void Update()
    {
        if (basicGuardianHealth.isAlive && playerIsDetected == false)
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
        else if (basicGuardianHealth.isAlive && playerIsDetected)
        {
            float playerPositionX = player.transform.position.x;
            float enemyPositionX = transform.position.x;
            float distance = Mathf.Abs(playerPositionX - enemyPositionX);

            if (distance > distanceToStop)
            {
                float directionX = playerPositionX > enemyPositionX ? 1f : -1f;
                mobRigidbody.velocity = new Vector2(directionX * speed, mobRigidbody.velocity.y);

                if (directionX > 0 && !isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
                else if (directionX < 0 && isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
            }
            else
            {
                mobRigidbody.velocity = new Vector2(0, mobRigidbody.velocity.y);
            }
        }
    }

    public void DetectPlayer(bool _newvalue)
    {
        playerIsDetected = _newvalue;
        detectionArea.SetActive(false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Vérifiez que le collider de la collision n'est pas null
    //    if (collision.collider == null)
    //    {
    //        Debug.LogError("Collision collider is null.");
    //        return; // Si le collider est null, quittez la méthode
    //    }

    //    // Vérifiez que le BoxCollider2D du gardien est assigné
    //    if (boxCollider2D == null)
    //    {
    //        Debug.LogError("boxCollider2D is not assigned.");
    //        return; // Si le BoxCollider2D n'est pas assigné, quittez la méthode
    //    }

    //    // Vérifiez si le joueur est caché en utilisant l'instance de HideHimSelf
    //    bool isHidden = HideHimSelf.instance != null && HideHimSelf.instance.IsPlayerHidden();

    //    // Vérifiez si le layer de l'objet en collision est le layer caché
    //    bool isInHiddenLayer = collision.gameObject.layer == hiddenLayer;

    //    // Affichez les informations de débogage concernant l'état de la collision
    //    Debug.LogError($"Collision Enter: IsPlayerHidden={isHidden}, collisionLayer={collision.gameObject.layer}, isInHiddenLayer={isInHiddenLayer}");

    //    // Ignorez la collision si le joueur est caché et que l'objet en collision est dans le layer caché
    //    if (isHidden && isInHiddenLayer)
    //    {
    //        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), boxCollider2D, true);
    //        Debug.LogError("Collision ignorée");
    //    }
    //    else
    //    {
    //        Debug.LogError("Collision non ignorée");
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // Vérifiez que le collider de la collision n'est pas null
    //    if (collision.collider == null)
    //    {
    //        Debug.LogError("Collision collider is null.");
    //        return; // Si le collider est null, quittez la méthode
    //    }

    //    // Vérifiez que le BoxCollider2D du gardien est assigné
    //    if (boxCollider2D == null)
    //    {
    //        Debug.LogError("boxCollider2D is not assigned.");
    //        return; // Si le BoxCollider2D n'est pas assigné, quittez la méthode
    //    }

    //    // Vérifiez si le joueur est caché en utilisant l'instance de HideHimSelf
    //    bool isHidden = HideHimSelf.instance != null && HideHimSelf.instance.IsPlayerHidden();
    //    // Vérifiez si le layer de l'objet en collision est le layer caché
    //    bool isInHiddenLayer = collision.gameObject.layer == hiddenLayer;

    //    // Affichez les informations de débogage concernant l'état de la collision
    //    Debug.LogError($"Collision Exit: IsPlayerHidden={isHidden}, collisionLayer={collision.gameObject.layer}, isInHiddenLayer={isInHiddenLayer}");

    //    // Réactivez la collision si le joueur n'est plus caché et que l'objet en collision est dans le layer caché
    //    if (!isHidden && isInHiddenLayer)
    //    {
    //        Physics2D.IgnoreCollision(collision.collider, boxCollider2D, false);
    //        Debug.LogError("Collision réactivée");
    //    }
    //    else
    //    {
    //        Debug.LogError("Collision non réactivée");
    //    }
    //}

}
