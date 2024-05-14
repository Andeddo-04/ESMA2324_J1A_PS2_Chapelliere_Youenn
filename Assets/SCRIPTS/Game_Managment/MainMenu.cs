using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public static bool gameIsPaused = true;

    public GameObject settingsWindow, canvasMainMenu, canvasUI; //, canvasPauseMenu;

    public PlayerMovement playerMovement;

    //private void Update()
    //{
    //    if (canvasMainMenu.activeSelf == true && canvasPauseMenu.activeSelf == false && canvasUI.activeSelf == false)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //}

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        canvasMainMenu.SetActive(false);
        canvasUI.SetActive(true);
        playerMovement.HideAndLockCursor();
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Paused()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
    }
}