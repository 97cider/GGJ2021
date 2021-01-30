using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public Image weaponIcon;
    public Image accessoryIcon;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            //weaponIcon.mainTexture = player.GetComponent<PlayerStats>().GetWeapon().
            Debug.Log("wew");
        }
    }
}
