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
    public Text roomsCompleted;
    public Text runsCompleted;
    public Text weaponName;
    public Text weaponDescription;
    public Text accessoryDescription;

    public Accessory accessory;
    public GameObject player;
    
    public GameObject health;
    public GameObject hp;
    public GameObject pause;
    public Canvas canvas;
    // Start is called before the first frame update
    public bool playerLoaded;
    public void showPause(string pt){
        pause.transform.GetChild(2).gameObject.GetComponent<Text>().text = pt;
        pause.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pt;
        canvas.GetComponent<GameManager>().ToggleGameState();
    }
    public void setRoomsCompleted(int r){
        this.roomsCompleted.text = (string) r.ToString();
    }
    public void setRunsCompleted(int r){
        this.runsCompleted.text = (string) r.ToString();
    }
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
    public void setWeaponUi(Weapon w){
        weapon = w;
        weaponIcon.sprite = w.weaponSprite;
        weaponDescription.text = w.description;
        weaponName.text = w.name;
    }
    public void setAccessoryUi(Accessory a){
        accessory = a;
        accessoryIcon.sprite = a.accessorySprite;
        a.getRichTextDescription();
        accessoryDescription.text = a.description;
        accessoryName.text = a.AccessoryName;

    }
    public void setHealthUi(int h){
        // Nuke the current health ui
        Debug.LogWarning($"Current Health Children: {health.transform.childCount}");
        for (int i = 0; i < health.transform.childCount; i++){
            Debug.LogWarning($"Destroying: {health.transform.GetChild(i)}");
            Destroy(health.transform.GetChild(i).gameObject);
        }
        // Set the current health value
        for (int i = 0; i < h; i++){
            var thp = Instantiate(hp, health.transform);
            thp.transform.localScale = new Vector3(1,1,1);
        }
    }
    void Update()
    {
        if (player != null && !playerLoaded){
            Debug.Log(player);
            updateHealth();
            // do accessory updating here.
            Debug.LogWarning(player.GetComponent<PlayerStats>().getMaxHP());
            playerLoaded = true;

            player.GetComponent<PlayerEffectsController>().ui = this;
            //weaponIcon.mainTexture = player.GetComponent<PlayerStats>().GetWeapon().

            // Weapon icon setting
            Weapon w = player.GetComponent<PlayerStats>().GetWeapon();
            setWeaponUi(w);
            // Accessory icon setting
            Accessory a = player.GetComponent<PlayerStats>().getCurrentAccessory();
            setAccessoryUi(a);


        }
    }
}
