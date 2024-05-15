using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D rb;

    public SpriteRenderer rbRenderer;

    public BoxCollider2D characterBoxCollider;

    //public GameObject canvasMainMenu, canvasUI, canvaspauseMenu;

    public LayerMask groundCollisionLayer;

    public static PlayerMovement instance;

    public Transform groundCheck;

    public int playerId = 0;

    public float moveSpeed, groundCheckRadius, buttonTime, jumpAmount, jumpTime;

    public bool useController = false, isGrounded, jumping;


    ////////// * Variables privées * \\\\\\\\\\

    private float controller_horizontalMovement, keyboard_horizontalMovement;

    private bool isAiming = false, endOfAiming;

    private Vector3 velocity, controller_AttackDirection, aim;

    private Player player;

    ////////// * Méthode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }


    ////////// * Méthode Update() * \\\\\\\\\\
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCollisionLayer);
        
        //if (canvaspauseMenu.activeSelf == false && canvasMainMenu.activeSelf == false)
        //{
        MovePlayer();

        //}

        //else if (canvasMainMenu.activeSelf == true && canvaspauseMenu.activeSelf == false && canvasUI.activeSelf == false)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

        //else if (canvasMainMenu.activeSelf == false && canvaspauseMenu.activeSelf == true && canvasUI.activeSelf == true)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}

    }

    ////////// * Méthode MovePlayer() * \\\\\\\\\\
    void MovePlayer()
    {
        if (useController)
        {
            ////////// * Contrôle à la manette * \\\\\\\\\\
            controller_horizontalMovement = player.GetAxis("Controller_HorizontalMovement") * moveSpeed;

            Vector3 targetVelocityWhisControler = new Vector2(controller_horizontalMovement, 0.0f);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocityWhisControler, ref velocity, 0.05f);

        }

        if (!useController)
        {
            ////////// * Contrôle au clavier * \\\\\\\\\\
            keyboard_horizontalMovement = player.GetAxis("KeyBoard_HorizontalMovement") * moveSpeed;

            Vector3 targetVelocityWhisKeyBoard = new Vector2(keyboard_horizontalMovement, 0.0f);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocityWhisKeyBoard, ref velocity, 0.05f);

            if (player.GetButtonDown("KeyBoard_Jump") && isGrounded)
            {
                jumping = true;
                jumpTime = 0;
            }

            if (jumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
                jumpTime += Time.deltaTime;
            }

            if (player.GetButtonUp("KeyBoard_Jump") | jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
    }

    ////////// *  * \\\\\\\\\\
    public void SetControllerUsage(bool useController)
    {
        this.useController = useController;
    }

    public void ShowAndUnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}