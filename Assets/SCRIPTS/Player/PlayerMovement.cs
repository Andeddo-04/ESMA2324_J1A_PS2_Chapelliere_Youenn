using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D characterSprite;

    public SpriteRenderer characterSpriteRenderer;

    public BoxCollider2D characterBoxCollider;

    public GameObject crossHair, newPositionOfCrossHair;//, canvasMainMenu, canvasUI, canvaspauseMenu;

    public static PlayerMovement instance;

    public CrossHairMovement crossHairMovement;

    public int playerId = 0;

    public float moveSpeed;

    public bool completeDongeon = false, useController = false;

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
            Debug.LogWarning("Il n'a plus d'instance de playerMovement dans la scène");
            return;
        }

        instance = this;
    }



    ////////// * Méthode Update() * \\\\\\\\\\
    void Update()
    {
        //if (canvaspauseMenu.activeSelf == false && canvasMainMenu.activeSelf == false)
        //{
        //    MovePlayer();
        //    crossHairMovement.MoveCrossHair();
        //    crossHairTracker();


        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
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

        MovePlayer();
        crossHairMovement.MoveCrossHair();
        crossHairTracker();
    }

    ////////// * Méthode MovePlayer() * \\\\\\\\\\
    void MovePlayer()
    {
        if (useController)
        {
            ////////// * Contrôle à la manette * \\\\\\\\\\
            controller_horizontalMovement = player.GetAxis("Controler_MoveHorizontal") * moveSpeed * Time.deltaTime;

            Vector3 targetVelocityWhisControler = new Vector2(controller_horizontalMovement, 0.0f);
            characterSprite.velocity = Vector3.SmoothDamp(characterSprite.velocity, targetVelocityWhisControler, ref velocity, 0.05f);
        }

        if (!useController)
        {
            ////////// * Contrôle au clavier * \\\\\\\\\\
            keyboard_horizontalMovement = player.GetAxis("KeyBoard_MoveHorizontal") * moveSpeed * Time.deltaTime;

            Vector3 targetVelocityWhisKeyBoard = new Vector2(keyboard_horizontalMovement, 0.0f);
            characterSprite.velocity = Vector3.SmoothDamp(characterSprite.velocity, targetVelocityWhisKeyBoard, ref velocity, 0.05f);
        }
    }

    ////////// * Méthode crossHairTracker() * \\\\\\\\\\
    void crossHairTracker()
    {
        GameObject.FindGameObjectWithTag("crossHairTracker").transform.position = newPositionOfCrossHair.transform.position;
    }

    public void SetControllerUsage(bool useController)
    {
        this.useController = useController;
    }

    public void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}