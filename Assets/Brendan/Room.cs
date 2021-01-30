using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Exit;
using static RoomController;
public class Room : MonoBehaviour
{
    // Start is called before the first frame update

    //public enum Exits {Left, Right, Down}; 
    public List<GameObject> availableExits;
    public GameObject nextLevel;
    public List<GameObject> enemies;
    public GameObject usedExit;
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
    }
    void onClear(){
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
