using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagemer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject creditsPanel;

    public GameObject controlsPanel;

    public void enableCreditsPanel(){
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void enableControlsPanel(){
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
    public void closeAll(){
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
    public void moveToPlayScene(){
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            closeAll();
        }
    }
}
