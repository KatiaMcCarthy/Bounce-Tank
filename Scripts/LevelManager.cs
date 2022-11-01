using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gameUI;

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnLoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnOptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnReturnToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OnResumeGame()
    {
        Debug.Log("resume game thing");

        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        FindObjectOfType<PlayerController>().SetActionMap("Game");
        Debug.Log(FindObjectOfType<PlayerInput>().currentActionMap);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        gameUI.SetActive(true);
    }

    public void OnPauseMenuOpen()
    {
        Debug.Log("pause menu thing");
        mainMenuPanel.SetActive(true);
        FindObjectOfType<PlayerController>().SetActionMap("UI");
        Debug.Log(FindObjectOfType<PlayerInput>().currentActionMap);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gameUI.SetActive(false);
    }
}
