using Rewired;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    public Transform groundCheck;

    public LayerMask[] canJumpingCollisionLayer;

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
            Debug.LogWarning("Il n'a plus d'instance de JumpController dans la scène");
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
            // Vérifie si la touche de saut est pressée et si l'objet est au sol
            if (player.GetButtonDown("Controller_Jump") && IsGrounded())
            {
                Jump();
            }
        }

        if (!playerMovement.useController)
        {
            // Vérifie si la touche espace est pressée et si l'objet est au sol
            if (player.GetButtonDown("KeyBoard_Jump") && IsGrounded())
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // Ajoute une vélocité verticale uniquement si la vélocité actuelle en Y est inférieure à maxVelocityY
        if (Mathf.Abs(rb.velocity.y) < maxVelocityY)
        {
            PlayerMovement.instance.animator.SetBool("Jump", true);

            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity += jumpVelocity;

            PlayerMovement.instance.animator.SetBool("Jump", false);
        }
    }

    bool IsGrounded()
    {
        // Vérifie si un cercle de rayon groundCheckRadius à la position groundCheck.position touche un collider de chaque layer spécifié dans la liste canJumpingCollisionLayer
        foreach (LayerMask layer in canJumpingCollisionLayer)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layer))
            {
                isGrounded = true;
                return true;
            }
        }

        isGrounded = false;
        return false;
    }
}
