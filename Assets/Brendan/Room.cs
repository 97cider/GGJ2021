using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Exit;
using static Helper;
public class Room : MonoBehaviour
{
    // Start is called before the first frame update

    //public enum Exits {Left, Right, Down}; 
    public List<GameObject> availableExits;
    public GameObject nextLevel;
    public exitType usedExit;
    public GameObject roomController;
    public List<GameObject> enemies;
    public bool isCleared;
    public bool loadedEnemies;
    void Update(){
        //Debug.LogError(enemies.Count);
        if (!isCleared && !loadedEnemies){
            var tmp = wrangleEnemies();
            if(tmp.Count >= 1){
                enemies = tmp;
                loadedEnemies = true;
            }
        }
    }
    void Start()
    {
        // Load the next level.
        //nextLevel = RoomController.getNextLevel(this.parent);
        this.usedExit = exitType.None;
        loadedEnemies = false;
        isCleared = false;
        //uncomment when actually done
        // if (enemies.Count == 0){
        //     isCleared = true;
        // }
        // else {
        //     isCleared = false;
        // }
        // Set the all the exits to have the current Room controller.
        //GameObject[] lgs = GameObject.FindGameObjectsWithTag("Exit");
        assignExits();
        playDoorIdle();
    }
    public void handleDeath(GameObject deadEntity){
        enemies.Remove(deadEntity);
        if (enemies.Count == 0){
            openDoors();
            playDoorOpeningAnimation();
            this.isCleared = true;
            
        }
    }
    public List<GameObject> wrangleEnemies(){
        GameObject[] tmp_enem = GameObject.FindGameObjectsWithTag("Enemy") as GameObject[];
        List<GameObject> tmpEnemies = new List<GameObject>();
        foreach(GameObject enemy in tmp_enem){
            enemy.GetComponent<Enemy>().DieEvents.AddListener(handleDeath);
            tmpEnemies.Add(enemy);
        }
        return tmpEnemies;
    }
    public void assignExits(){
        foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                child.GetComponent<Exit>().room = this.gameObject;
            }
        }
    }
    public void playDoorIdle(){
          foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                //child.GetComponent<Animation>().Play("MC_WALk");
                child.GetChild(0).GetComponent<Animator>().Play("doorIdle");
            }
        }
    }
    public void playDoorOpeningAnimation(){
        foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                child.GetChild(0).GetComponent<Animator>().Play("unlockDoor2");
            }
        }
    }
    public void closeDoors(){
        foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                child.GetComponent<Exit>().lockedDoor.enabled = true;
            }
        }
    }
    public void openDoors(){
         foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                child.GetComponent<Exit>().lockedDoor.enabled = false;
            }
        }
    }
    void onClear(){
    }
}
