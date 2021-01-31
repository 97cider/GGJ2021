using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //var psobj = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerStats>().playerdieEvent;
        //G//ameObject.FindWithTag("Player").gameObject.GetComponent<PlayerStats>().playerdieEvent.AddListener(deathHandler);
    }
    void Start(){
        var psobj = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerStats>().playerdieEvent;
        GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerStats>().playerdieEvent.AddListener(deathHandler);

    }
    void deathHandler(){
        SceneManager.LoadScene("MainMenu");
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameState();
        }
    }
}
