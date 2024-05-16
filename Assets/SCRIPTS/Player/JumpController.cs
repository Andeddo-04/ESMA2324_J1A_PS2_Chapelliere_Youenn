using Rewired;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    public Transform groundCheck;

    public LayerMask groundCollisionLayer;

    public static JumpController instance;

    public PlayerMovement playerMovement;

    public float jumpForce, maxVelocityY, groundCheckRadius; // Vitesse maximale en Y

    public bool isGrounded;


    private Rigidbody2D rb;

    private Player player;

    private int playerId = 0;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de JumpController dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerMovement.useController)
        {
            // V�rifie si la touche de saut est press�e et si l'objet est au sol
            if (player.GetButtonDown("Controller_Jump") && IsGrounded())
            {
                Jump();
            }
        }

        if (!playerMovement.useController)
        {
            // V�rifie si la touche espace est press�e et si l'objet est au sol
            if (player.GetButtonDown("KeyBoard_Jump") && IsGrounded())
            {
                Jump();
            }
        }
        
    }

    void Jump()
    {
        // Ajoute une v�locit� verticale uniquement si la v�locit� actuelle en Y est inf�rieure � maxVelocityY
        if (Mathf.Abs(rb.velocity.y) < maxVelocityY)
        {
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity += jumpVelocity;
        }
    }

    bool IsGrounded()
    {
        // V�rifie si un cercle de rayon groundCheckRadius � la position groundCheck.position touche un collider du layer sp�cifi� (groundCollisionLayer)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCollisionLayer);

        return isGrounded;
    }

}
