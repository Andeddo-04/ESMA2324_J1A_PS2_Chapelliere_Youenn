using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D rb;

    public SpriteRenderer rbRenderer;

    public BoxCollider2D characterBoxCollider;

    public GameObject newPosition;

    public CrosshairMovement crosshairMovement;

    public static PlayerMovement instance;

    public float moveSpeed;

    public bool useController = false, canBeDetected = true;


    ////////// * Variables priv�es * \\\\\\\\\\

    private GameObject sceneManager;

    private PauseMenu pauseMenu;

    private float controller_horizontalMovement, keyboard_horizontalMovement;

    private int playerId = 0;

    private Vector3 velocity;

    private Player player;

    ////////// * M�thode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il n'a plus d'instance de PlayerMovement dans la sc�ne");
            return;
        }

        instance = this;

        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");

        pauseMenu = sceneManager.GetComponent<PauseMenu>();
    }


    ////////// * M�thode Update() * \\\\\\\\\\
    void Update()
    {
        MovePlayer();
        //SelectControls();
        crossHairTracker();
        crosshairMovement.MoveCrossHair();
    }

    ////////// * M�thode MovePlayer() * \\\\\\\\\\
    void MovePlayer()
    {
        if (useController)
        {
            ////////// * Contr�le � la manette * \\\\\\\\\\
            controller_horizontalMovement = player.GetAxis("Controller_HorizontalMovement") * moveSpeed;

            rb.velocity = new Vector2(controller_horizontalMovement, rb.velocity.y);
        }

        if (!useController)
        {
            ////////// * Contr�le au clavier * \\\\\\\\\\
            keyboard_horizontalMovement = player.GetAxis("KeyBoard_HorizontalMovement") * moveSpeed;

            rb.velocity = new Vector2(keyboard_horizontalMovement, rb.velocity.y);
        }
    }

    void crossHairTracker()
    {
        GameObject.FindGameObjectWithTag("CrossHairTracker").transform.position = newPosition.transform.position;
    }

    ////////// *  * \\\\\\\\\\
    public void SetControllerUsage(bool _useController)
    {
        useController = _useController;
    }

    public void CanBeDetectedChanger(bool _newValue)
    {
        canBeDetected = _newValue;
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

    //void SelectControls()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        useController = !useController;
    //    }
    //}
}