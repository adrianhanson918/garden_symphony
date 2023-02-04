using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameHud;

    private bool isPaused = false;
    private Controls controls;
    private InputAction menu;


    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        menu = controls.UI.Menu;
        menu.Enable();

        menu.performed += TogglePause;
    }

    private void OnDisable()
    {
        menu.performed -= TogglePause;

        menu.Disable();
    }

    public void PauseGame()
    {
        //Stop time
        isPaused = true;
        Time.timeScale = 0;

        //Enable menus
        //gameHud.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        //Restart time
        isPaused = false;
        Time.timeScale = 1;

        //Disable menus
        //gameHud.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void GameOver()
    {
        //Show menu
        //gameHud.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
        #else
        Application.Quit();
        #endif
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (isPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }
}
