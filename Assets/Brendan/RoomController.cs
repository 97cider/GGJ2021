using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Exit;
public class RoomController : MonoBehaviour
{
    public GameObject _tileMap;
    public GameObject _scene;
    public GameObject _nextMap;
    private GameObject _currentMap;
    public GameObject _exitTest;
    public Camera mainCamera;
    public List<GameObject> _LoadedRooms;
    // Start is called before the first frame update
    void Start()
    {
        //this._getTilemaps();
        if (_LoadedRooms.Count > 0){
            var current_level = this.pickRandomRoom();
            _scene.GetComponent<SceneHandler>().tileMap = current_level;
            _currentMap = GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setNextLevel(){
        var new_level = this.pickRandomRoom();
        while (new_level == _scene.GetComponent<SceneHandler>().tileMap){
            new_level = this.pickRandomRoom();
        }
        
        // Set the transform of the next level to the right a bunch
        // new_level.GetComponent<Transform>
    }
    public void setLevel(){
        if (_scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel != null){
            _scene.GetComponent<SceneHandler>().tileMap = _scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel;
            var new_level = this.pickRandomRoom();
            //Remember to do check
            while (new_level == _scene.GetComponent<SceneHandler>().tileMap){
                new_level = this.pickRandomRoom();
            }
            // Wait a bit to destroy the current map to get a proper pan
            //Destroy(_currentMap);

            // Instantiate the next map a little bit in some direction.
            Debug.Log()
            if (_currentMap.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Left){
                
                var leftPos = _currentMap.transform.position + new Vector3(-10, 0, 0);

                _currentMap = GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap, leftPos, Quaternion.identity);
                
                var exitObj = _currentMap.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit;
                Vector3 screenpoint  = mainCamera.WorldToViewportPoint(exitObj.transform.position);
                bool isOnScreen = screenpoint.z > 0 && screenpoint.x > 0 && screenpoint.x < 1 && screenpoint.y > 0 && screenpoint.y < 1;
                var target = mainCamera.transform.position + new Vector3(-10, 0,0);
                var norm = (target - mainCamera.transform.position).normalized;
                var speed = 1f;
                while(isOnScreen){
                    // Move the camera
                    mainCamera.transform.position += norm * speed * Time.deltaTime;
                    isOnScreen = screenpoint.z > 0 && screenpoint.x > 0 && screenpoint.x < 1 && screenpoint.y > 0 && screenpoint.y < 1;
                }

            }
            else if (_currentMap.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Right){
            
            }
            else if (_currentMap.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit == exitType.Down){

            }
            else{
                Debug.Log("test");
            }
            _currentMap = GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap);
            _scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = new_level;
        }
        else{
            var new_level = this.pickRandomRoom();
            while (new_level == _scene.GetComponent<SceneHandler>().tileMap){
                new_level = this.pickRandomRoom();
            }

            var next_level = this.pickRandomRoom();
            // When we have more than 2 levels, edit the check to look at both this level and the previous
            while (next_level == new_level){
                next_level = this.pickRandomRoom();
            }

            //_currentMap.GetComponent<SceneHandler>().tileMap
            Destroy(_currentMap);
            _scene.GetComponent<SceneHandler>().tileMap = new_level;
            _scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = next_level;
            _currentMap = GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap);
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
