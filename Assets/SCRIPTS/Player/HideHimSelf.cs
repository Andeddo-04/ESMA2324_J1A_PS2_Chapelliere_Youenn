using Rewired;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class HideHimSelf : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public Sprite hideSprite; // Assignez le sprite de camouflage via l'inspecteur
    public static HideHimSelf instance;
    public bool isPlayerHidden = false;

    private Sprite defaultSprite;
    private Player player;
    private int playerId = 0, hiddenSortingOrder = 0, revealedSortingOrder = 10, defaultLayer, hiddenLayer;
    [SerializeField] private List<EnemyObject> enemies;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        defaultSprite = spriteRenderer.sprite;
        defaultLayer = gameObject.layer;
        hiddenLayer = LayerMask.NameToLayer("HiddenPlayer");
    }

    void Start()
    {
        // Récupérez la liste des ennemis du Scene_Manager
        Scene_Manager sceneManager = FindObjectOfType<Scene_Manager>();
    }

    void Update()
    {
        if (PlayerMovement.instance.useController)
        {
            if (player.GetButton("Controller_CoverUp"))
            {
                HidePlayer();
            }
            else if (player.GetButtonUp("Controller_CoverUp"))
            {
                RevealPlayer();
            }
        }
        else
        {
            if (player.GetButton("KeyBoard_CoverUp"))
            {
                HidePlayer();
            }
            else if (player.GetButtonUp("KeyBoard_CoverUp"))
            {
                RevealPlayer();
            }
        }
    }

    void HidePlayer()
    {
        PlayerMovement.instance.CantBeDetected();
        spriteRenderer.sprite = hideSprite;
        AdjustBoxCollider(hideSprite);
        spriteRenderer.sortingOrder = hiddenSortingOrder;
        gameObject.layer = hiddenLayer;
        isPlayerHidden = true;
        SetEnemyCollisions(false);

        if (AttackController.instance.useBow)
        {
            AttackController.instance.crosshairMovement.crossHair.SetActive(false);
        }
    }

    void RevealPlayer()
    {
        PlayerMovement.instance.CanBeDetected();
        spriteRenderer.sprite = defaultSprite;
        AdjustBoxCollider(defaultSprite);
        spriteRenderer.sortingOrder = revealedSortingOrder;
        gameObject.layer = defaultLayer;
        isPlayerHidden = false;
        SetEnemyCollisions(true);

        if (AttackController.instance.useBow)
        {
            AttackController.instance.crosshairMovement.crossHair.SetActive(true);
        }
    }

    void AdjustBoxCollider(Sprite sprite)
    {
        if (sprite == null) return;

        Vector2 spriteSize = sprite.bounds.size;
        boxCollider.size = spriteSize;
        boxCollider.offset = Vector2.zero;
    }

    void SetEnemyCollisions(bool enable)
    {
        // Récupère tous les GameObjects présents sur le layer "EnemyLayer"
        var enemies = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.layer == LayerMask.NameToLayer("Ennemie")).ToList();

        foreach (var enemy in enemies)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                Physics2D.IgnoreCollision(boxCollider, enemyCollider, !enable);
            }
        }
    }

    public bool IsPlayerHidden()
    {
        return isPlayerHidden;
    }
}
