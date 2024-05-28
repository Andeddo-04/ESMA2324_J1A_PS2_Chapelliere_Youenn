using Rewired;
using UnityEngine;

public class HideHimSelf : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public Sprite hideSprite;
    public static HideHimSelf instance;

    private Sprite defaultSprite;
    private Player player;
    private bool isPlayerHidden = false;
    private int playerId = 0;
    private int hiddenSortingOrder = 0;
    private int revealedSortingOrder = 10;
    private int defaultLayer;
    private int hiddenLayer;

    void Awake()
    {
        instance = this;
        player = ReInput.players.GetPlayer(playerId);

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        defaultSprite = spriteRenderer.sprite;
        defaultLayer = gameObject.layer;
        hiddenLayer = LayerMask.NameToLayer("HiddenPlayer");
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
        Debug.LogWarning("Je suis caché");
    }

    void RevealPlayer()
    {
        PlayerMovement.instance.CanBeDetected();
        spriteRenderer.sprite = defaultSprite;
        AdjustBoxCollider(defaultSprite);
        spriteRenderer.sortingOrder = revealedSortingOrder;
        gameObject.layer = defaultLayer;
        isPlayerHidden = false;
        Debug.LogWarning("Je ne suis plus caché");
    }

    void AdjustBoxCollider(Sprite sprite)
    {
        if (sprite == null) return;

        Vector2 spriteSize = sprite.bounds.size;
        boxCollider.size = spriteSize;
        boxCollider.offset = Vector2.zero;
    }

    public bool IsPlayerHidden()
    {
        return isPlayerHidden;
    }
}
