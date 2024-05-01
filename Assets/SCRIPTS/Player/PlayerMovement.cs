using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D characterSprite;

    public SpriteRenderer characterSpriteRenderer;

    public CapsuleCollider2D characterBoxCollider;

    public GameObject crossHair, newPosition, canvasMainMenu, canvasUI, canvaspauseMenu;

    public static PlayerMovement instance;

    public int playerId = 0;

    public float moveSpeed;

    public bool completeDongeon = false, useController = false;

    ////////// * Variables privées * \\\\\\\\\\

    private float controller_horizontalMovement, controller_verticalMovement, keyboard_horizontalMovement, keyboard_verticalMovement;

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
        if (canvaspauseMenu.activeSelf == false && canvasMainMenu.activeSelf == false)
        {
            MovePlayer();
            MoveCrossHair();
            crossHairTracker();
            //controllerSwitch();


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else if (canvasMainMenu.activeSelf == true && canvaspauseMenu.activeSelf == false && canvasUI.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (canvasMainMenu.activeSelf == false && canvaspauseMenu.activeSelf == true && canvasUI.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    ////////// * Méthode MovePlayer() * \\\\\\\\\\
    void MovePlayer()
    {
        if (useController)
        {
            ////////// * Contrôle à la manette * \\\\\\\\\\
            controller_horizontalMovement = player.GetAxis("Controler_MoveHorizontal") * moveSpeed * Time.deltaTime;
            controller_verticalMovement = player.GetAxis("Controler_MoveVertical") * moveSpeed * Time.deltaTime;

            Vector3 targetVelocityWhisControler = new Vector2(controller_horizontalMovement, controller_verticalMovement);
            characterSprite.velocity = Vector3.SmoothDamp(characterSprite.velocity, targetVelocityWhisControler, ref velocity, 0.05f);
        }

        if (!useController)
        {
            ////////// * Contrôle au clavier * \\\\\\\\\\
            keyboard_horizontalMovement = player.GetAxis("KeyBoard_MoveHorizontal") * moveSpeed * Time.deltaTime;
            keyboard_verticalMovement = player.GetAxis("KeyBoard_MoveVertical") * moveSpeed * Time.deltaTime;

            Vector3 targetVelocityWhisKeyBoard = new Vector2(keyboard_horizontalMovement, keyboard_verticalMovement);
            characterSprite.velocity = Vector3.SmoothDamp(characterSprite.velocity, targetVelocityWhisKeyBoard, ref velocity, 0.05f);
        }
    }

    ////////// * Méthode MoveCrossHair() * \\\\\\\\\\
    void MoveCrossHair()
    {
        ////////// * Contrôle du crosshair à la manette * \\\\\\\\\\
        if (useController)
        {
            controller_AttackDirection = new Vector3(player.GetAxis("Controler_AimHorizontal"), player.GetAxis("Controler_AimVertical"), 0.0f);

            if (controller_AttackDirection.magnitude > 0.0f)
            {
                controller_AttackDirection.Normalize();
                controller_AttackDirection *= 2.0f;
                crossHair.transform.localPosition = controller_AttackDirection;
                crossHair.SetActive(true);
            }

            else
            {
                crossHair.SetActive(false);
            }
        }

        ////////// * Contrôle du crosshair à la sourie * \\\\\\\\\\
        if (!useController)
        {
            // mouse_AttackDirection = new Vector3(player.GetAxis("Mouse_AimHorizontal"), player.GetAxis("Mouse_AimVertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement * 2;

            isAiming = player.GetButton("Mouse_IsAiming");
            endOfAiming = player.GetButtonUp("Mouse_IsAiming");

            if (isAiming)
            {
                crossHair.SetActive(true);

                if (aim.magnitude > 1.0f)
                {
                    aim.Normalize();
                    aim *= 2.0f;
                    crossHair.transform.localPosition = aim;
                }
            }

            else
            {
                crossHair.SetActive(false);
            }
        }
    }

    ////////// * Méthode crossHairTracker() * \\\\\\\\\\
    void crossHairTracker()
    {
        GameObject.FindGameObjectWithTag("crossHairTracker").transform.position = newPosition.transform.position;
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