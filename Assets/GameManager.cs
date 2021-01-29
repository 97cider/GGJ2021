using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool isPaused;

    [SerializeField] private GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        UpdatePauseUI();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        UpdatePauseUI();
    }

    private void UpdatePauseUI()
    {
        pauseMenu.SetActive(isPaused);
    }

    public void ToggleGameState()
    {
        if (isPaused) 
        {
            this.ResumeGame();
            return;
        }
        this.PauseGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Awake()
    {
        this.pauseMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameState();
        }
    }
}
