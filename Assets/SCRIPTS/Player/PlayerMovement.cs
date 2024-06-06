using Rewired;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables publiques
    public Rigidbody2D rb;
    
    public SpriteRenderer rbRenderer;
    
    public BoxCollider2D characterBoxCollider;
    
    public GameObject newPosition, inventoryUI;
    
    public CrosshairMovement crosshairMovement;
    
    public static PlayerMovement instance;

    public Animator animator;

    public float moveSpeed;
    
    public bool useController = false, canBeDetected = true, inventoryIsActive = false;

    // Variables priv�es
    private GameObject sceneManager;
    
    private PauseMenu pauseMenu;

    private bool isFacingRight;

    private float controller_horizontalMovement, keyboard_horizontalMovement;
    
    private int playerId = 0;
    
    private Vector3 velocity;
    
    private Player player;

    // M�thode Awake()
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (instance != null)
        {
            Debug.LogWarning("Il y a d�j� une instance de PlayerMovement dans la sc�ne");
            return;
        }

        instance = this;

        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneManager == null)
        {
            Debug.LogError("Aucun GameObject avec le tag 'SceneManager' n'a �t� trouv�.");
        }
        else
        {
            pauseMenu = sceneManager.GetComponent<PauseMenu>();
            if (pauseMenu == null)
            {
                Debug.LogError("Le composant 'PauseMenu' n'a pas �t� trouv� sur 'SceneManager'.");
            }
        }

        Transform canvasInventoryTransform = GameObject.Find("Canvas - Inventory")?.transform;
        if (canvasInventoryTransform != null)
        {
            Transform panelInventoryTransform = canvasInventoryTransform.Find("Panel - Inventory");
            if (panelInventoryTransform != null)
            {
                inventoryUI = panelInventoryTransform.gameObject;
            }
            else
            {
                Debug.LogError("'panel - Inventory' n'a pas �t� trouv� sous 'Canvas - Inventory'.");
            }
        }
        else
        {
            Debug.LogError("'Canvas - Inventory' n'a pas �t� trouv�.");
        }
    }

    // M�thode Update()
    void Update()
    {
        MovePlayer();
        crossHairTracker();
        AttcksHitboxsTracker();
        crosshairMovement.MoveCrossHair();
        OpenInventory();
    }

    // M�thode MovePlayer()
    void MovePlayer()
    {
        if (inventoryIsActive == false)
        {
            if (useController)
            {
                // Contr�le � la manette
                controller_horizontalMovement = player.GetAxis("Controller_HorizontalMovement") * moveSpeed;
                rb.velocity = new Vector2(controller_horizontalMovement, rb.velocity.y);

                Flip(controller_horizontalMovement);

                controller_horizontalMovement = Mathf.Abs(rb.velocity.x); //convertit la valeur de la vitesse en chiffre positif pour l'animator
                animator.SetFloat("playerSpeed", controller_horizontalMovement);
            }
            else
            {
                // Contr�le au clavier
                keyboard_horizontalMovement = player.GetAxis("KeyBoard_HorizontalMovement") * moveSpeed;
                rb.velocity = new Vector2(keyboard_horizontalMovement, rb.velocity.y);

                Flip(keyboard_horizontalMovement);

                keyboard_horizontalMovement = Mathf.Abs(rb.velocity.x); //convertit la valeur de la vitesse en chiffre positif pour l'animator
                animator.SetFloat("playerSpeed", keyboard_horizontalMovement);
            }
        }

        else
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    void crossHairTracker()
    {
        GameObject crossHairTracker = GameObject.FindGameObjectWithTag("CrossHairTracker");
        if (crossHairTracker != null && newPosition != null)
        {
            crossHairTracker.transform.position = newPosition.transform.position;
        }
        else
        {
            Debug.LogError("Le 'CrossHairTracker' ou 'newPosition' n'a pas �t� trouv�.");
        }
    }

    void AttcksHitboxsTracker()
    {
        GameObject crossHairTracker = GameObject.FindGameObjectWithTag("AttcksHitboxsTracker");
        if (crossHairTracker != null && newPosition != null)
        {
            crossHairTracker.transform.position = newPosition.transform.position;
        }
        else
        {
            Debug.LogError("Le 'CrossHairTracker' ou 'newPosition' n'a pas �t� trouv�.");
        }
    }

    // M�thodes diverses
    public void SetControllerUsage(bool _useController)
    {
        useController = _useController;
    }

    public void CanBeDetected()
    {
        canBeDetected = true;
    }
    public void CantBeDetected()
    {
        canBeDetected = false;
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

    public void OpenInventory()
    {
        if (inventoryUI == null)
        {
            Debug.LogError("inventoryUI n'est pas assign�.");
            return;
        }

        if (useController && player.GetButtonDown("Controller_Inventory"))
        {
            ToggleInventory();
        }
        else if (!useController && player.GetButtonDown("KeyBoard_Inventory"))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        inventoryIsActive = !inventoryIsActive;
        inventoryUI.SetActive(inventoryIsActive);

        if (inventoryIsActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
            
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Flip(float _directionX)
    {
        if (_directionX < 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (_directionX > 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
