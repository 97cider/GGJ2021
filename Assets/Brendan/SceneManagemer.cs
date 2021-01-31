using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagemer : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void moveToPlayScene(){
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
