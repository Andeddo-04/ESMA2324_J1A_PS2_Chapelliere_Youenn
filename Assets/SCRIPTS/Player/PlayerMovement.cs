using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D rb;

    public SpriteRenderer rbRenderer;

    public BoxCollider2D characterBoxCollider;

    //public GameObject canvasMainMenu, canvasUI, canvaspauseMenu;

    public static PlayerMovement instance;

    public float moveSpeed;

    public bool useController = false;


    ////////// * Variables privées * \\\\\\\\\\

    private float controller_horizontalMovement, keyboard_horizontalMovement;

    private int playerId = 0;

    private Vector3 velocity;

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
        MovePlayer();
        SelectControls();
        
        
        
        
        
        //if (canvaspauseMenu.activeSelf == false && canvasMainMenu.activeSelf == false)
        //{
        

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

            rb.velocity = new Vector2(controller_horizontalMovement, rb.velocity.y);
        }

        if (!useController)
        {
            ////////// * Contrôle au clavier * \\\\\\\\\\
            keyboard_horizontalMovement = player.GetAxis("KeyBoard_HorizontalMovement") * moveSpeed;

            rb.velocity = new Vector2(keyboard_horizontalMovement, rb.velocity.y);
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

    void SelectControls()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            useController = !useController;
        }
    }
}