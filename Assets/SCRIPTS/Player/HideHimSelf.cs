using Rewired;
using UnityEngine;

public class HideHimSelf : MonoBehaviour
{
    ////////// * Variables publiques * \\\\\\\\\\

    public Rigidbody2D rb;

    public SpriteRenderer rbRenderer;

    public BoxCollider2D characterBoxCollider;

    ////////// * Variables privées * \\\\\\\\\\

    private int playerId = 0;

    private Player player;

    ////////// * Méthode Awake() * \\\\\\\\\\
    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.instance.useController && player.GetButtonDown("Controller_CoverUp"))
        {
            Debug.LogWarning("Je suis cacher");
        }

        if (!PlayerMovement.instance.useController && player.GetButtonDown("KeyBoard_CoverUp"))
        {
            Debug.LogWarning("Je suis cacher");
        }
    }
}
