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
    
    public GameObject health;
    public GameObject hp;

    public Canvas canvas;
    // Start is called before the first frame update
    public bool playerLoaded;
    void Start()
    {
        playerLoaded = false;
    }

    public void updateCurrentHealth(){
        int p = player.GetComponent<PlayerStats>().getMaxHP();
        int c = player.GetComponent<PlayerStats>().getCurrentHp();
        for (int i = p-1; i > c-1; i--){
            var thp = health.transform.GetChild(i);
            thp.GetChild(1).GetComponent<Image>().enabled = false;
        }
    }
    public void updateHealth(){
        int p = player.GetComponent<PlayerStats>().getMaxHP();

        Debug.Log($"Current max hp via uicont: {player.GetComponent<PlayerStats>().getMaxHP()}");
        for(int i =0; i < p; i++){
            var thp = Instantiate(hp, health.transform);
            thp.transform.localScale = new Vector3(1,1,1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null && !playerLoaded){
            Debug.Log(player);
            updateHealth();
            // do accessory updating here.
            playerLoaded = true;

            player.GetComponent<PlayerEffectsController>().ui = this;
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
