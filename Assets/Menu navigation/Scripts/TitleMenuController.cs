using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleMenuController : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;

    [Header("Main Menu")]
    [SerializeField] Button creditsButton;

    [Header("Credits Menu")]
    [SerializeField] Button backButton;

    private void Start()
    {
        SetMenu(mainMenu);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsButton()
    {
        SetMenu(creditsMenu);
        backButton.Select();
    }

    public void OnExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
        #else
        Application.Quit();
        #endif
    }

    public void OnBackButton()
    {
        SetMenu(mainMenu);
        creditsButton.Select();
    }

    private void SetMenu(GameObject nextMenu)
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);

        nextMenu.SetActive(true);
    }
}
