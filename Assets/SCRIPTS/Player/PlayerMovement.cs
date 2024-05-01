using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerGameObject;

    public Rigidbody2D playerRigidbody2D;

    public int playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(-1f, 0f) * playerSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(1f, 0f) * playerSpeed;
        }
    }
}
