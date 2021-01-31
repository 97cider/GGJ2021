using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public Image weaponIcon;
    public Weapon weapon;
    public Image accessoryIcon;
    public Text accessoryName;
    public Text weaponName;
    public Text weaponDescription;
    public Text accessoryDescription;

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
            weaponDescription.text = w.description;
            weaponName.text = w.name;

            // Accessory icon setting
            Accessory a = player.GetComponent<PlayerStats>().getCurrentAccessory();
            accessory = a;
            accessoryIcon.sprite = a.accessorySprite;
            a.getRichTextDescription();
            accessoryDescription.text = a.description;
            accessoryName.text = a.AccessoryName;
        }
    }
}
