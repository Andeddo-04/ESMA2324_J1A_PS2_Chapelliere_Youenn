using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public void HideAndLockCursor()
    {
        playerMovement.HideAndLockCursor();
    }
}
