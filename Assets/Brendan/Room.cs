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
    public void moveToLevel(){
        // This function takes in an exit as a parameter, and moves the camera away from the 
        // the exit. Once the camera is sufficiently away, spawn in the new level.
        
        // for testing, just take in a door
        var exit = usedExit;
        var transform_of_door = exit.transform;
        var typeOfDoor = exit.GetComponent<Exit>().thisExit;
        if (typeOfDoor == exitType.Left){
            // Spawn a new scene to the right
            Instantiate(nextLevel, new Vector3(10, 0, 0), Quaternion.identity);

            // Move 
        }
    }
    void onClear(){
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
