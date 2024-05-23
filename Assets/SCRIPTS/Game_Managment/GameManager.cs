using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void HideAndLockCursor()
    {
        PlayerMovement.instance.HideAndLockCursor();
    }
}
