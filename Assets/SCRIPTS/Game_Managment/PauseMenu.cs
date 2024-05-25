using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public string levelToLoad;

    public GameObject settingsWindow, canvasPauseMenu;

    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        LaunchPauseMenu();
    }

    private void LaunchPauseMenu()
    {
        if ((player.GetButtonDown("Controller_PauseButton") && PlayerMovement.instance.useController) || (player.GetButtonDown("KeyBoard_PauseButton") && !PlayerMovement.instance.useController))
        {
            if (!gameIsPaused)
            {
                Pause();
                gameObject.SetActive(true);
                PlayerMovement.instance.ShowAndUnlockCursor();
                Debug.LogError("Game is paused");
            }
        }
    }

    public void ResumeGame()
    {
        if (gameIsPaused)
        {
            PlayerMovement.instance.HideAndLockCursor();
            canvasPauseMenu.SetActive(false);
            Time.timeScale = 1;
            gameIsPaused = false;
        }
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void MainMenuButton()
    {
        canvasPauseMenu.SetActive(false);
        SceneManager.LoadScene(levelToLoad);
        Debug.LogError("Return to main menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Pause()
    {
        canvasPauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
}
