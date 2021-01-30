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
    public List<GameObject> enemies;
    public exitType usedExit;
    public GameObject roomController;
    public bool isCleared;
    void Start()
    {
        // Load the next level.
        //nextLevel = RoomController.getNextLevel(this.parent);
        if (enemies.Count == 0){
            isCleared = true;
        }
        else {
            isCleared = false;
        }
        // Set the all the exits to have the current Room controller.
        //GameObject[] lgs = GameObject.FindGameObjectsWithTag("Exit");
        assignExits();
    }
    public void assignExits(){
        foreach(Transform child in transform.GetChild(0)){
            if(child.tag == "Exit"){
                child.GetComponent<Exit>().room = this.gameObject;
            }
        }
    }
    void onClear(){
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
