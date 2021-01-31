using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagemer : MonoBehaviour
{
    // Start is called before the first frame update
    
    private IEnumerator moveToPlayScene(){
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
