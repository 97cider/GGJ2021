using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Exit;
public class RoomController : MonoBehaviour
{
    public GameObject scene;
    public GameObject _currentMap;
    public GameObject _exitTest;
    public Camera mainCamera;
    private Vector3 leftMost, rightMost, downMost;
    public List<GameObject> _LoadedRooms;
    public GameObject player;
    private float zoffset;
    // Start is called before the first frame update
    void Start()
    {

        //zoffset = mainCamera.transform.position.z;
        zoffset = 0;
        //this._getTilemaps();
        if (_LoadedRooms.Count > 0){
            var current_level = this.pickRandomRoom();
            scene.GetComponent<SceneHandler>().tileMap = current_level;
            this._currentMap = GameObject.Instantiate(scene.GetComponent<SceneHandler>().tileMap);
            this.setNextLevel();
            //scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = next_level;
            //scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = setNextLevel();

            // Spawn the player
            Transform t =  scene.GetComponent<SceneHandler>().tileMap.transform.GetChild(0).transform.Find("Spawn");
            Debug.Log(t.position);
            //Vector3 spawner = scene.GetComponent<SceneHandler>().tileMap.transform.Find("Spawn").position;
            Debug.Log($"Player is spawning at {t.position}");
            Instantiate(player, t.position, Quaternion.identity);
        }
        leftMost = new Vector3(0,0,zoffset);
        rightMost = new Vector3(0,0,zoffset);
        downMost = new Vector3(0,0,zoffset);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setNextLevel(){
        var new_level = this.pickRandomRoom();
        while (new_level == scene.GetComponent<SceneHandler>().tileMap){
            new_level = this.pickRandomRoom();
        }
        // Set the current level in the room controller
        //_currentMap.GetComponent<Room>().nextLevel = new_level;
        // Set the current level in the scene
        scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = new_level;
    }
    public void destroyRoom(){
        Destroy(this._currentMap);
    }
    public void setLevel(){
        if (scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel != null){
            //scene.GetComponent<SceneHandler>().tileMap = scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel;
            var new_level = this.pickRandomRoom();
            //Remember to do check
            while (new_level == scene.GetComponent<SceneHandler>().tileMap){
                new_level = this.pickRandomRoom();
            }
            if (scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Left){
                // We exit stage left. Spawn a room to the left, pan to it, and then set the current room to that room
                // Instantiate the next level far away from the left
                var _currentNextMap = Instantiate(scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel, leftMost+new Vector3(-15, 0,0), Quaternion.identity);
                leftMost = leftMost+new Vector3(-15, 0,0);
                rightMost = leftMost;
                downMost = leftMost;
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                scene.GetComponent<SceneHandler>().tileMap = _currentNextMap;
                //var next_level_exit = scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel.transform.position;
                //Destroy(_currentMap);

            }
            else if (scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Right){
                var _currentNextMap = Instantiate(scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel, rightMost+new Vector3(15, 0,0), Quaternion.identity);
                rightMost = rightMost+new Vector3(15, 0,0);
                leftMost = rightMost;
                downMost = rightMost;
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                scene.GetComponent<SceneHandler>().tileMap = _currentNextMap;
                //var next_level_exit = scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel.transform.position;

            }
            else if (scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Down){
                var _currentNextMap = Instantiate(scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel, downMost+new Vector3(0, -10,0), Quaternion.identity);
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                downMost = downMost+new Vector3(0, -10,0);
                rightMost = downMost;
                leftMost = downMost;
                scene.GetComponent<SceneHandler>().tileMap = _currentNextMap;
                //var next_level_exit = scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel.transform.position;
            }
            else{
                Debug.Log("test");
            }
            //_currentMap = GameObject.Instantiate(scene.GetComponent<SceneHandler>().tileMap);
            //scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = new_level;
        }
        else{
                Debug.Log("Something is borked");
            }
    }
    GameObject pickRandomRoom(){
        if (_LoadedRooms.Count == 0){
            Debug.Log("You have no rooms!");
            return null;
        }
        else{
            var level = Random.Range(0, _LoadedRooms.Count);
            return _LoadedRooms[level];
        }
    }
}
