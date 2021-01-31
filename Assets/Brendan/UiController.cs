using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public Image weaponIcon;
    public Weapon weapon;
    public Image accessoryIcon;
    public Accessory accessory;
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

            // Weapon icon setting
            Weapon w = player.GetComponent<PlayerStats>().GetWeapon();
            weapon = w;
            weaponIcon.sprite = w.weaponSprite;

            // Accessory icon setting
            Accessory a = player.GetComponent<PlayerStats>().getCurrentAccessory();
            accessory = a;
            accessoryIcon.sprite = a.accessorySprite;
        }
    }
}
