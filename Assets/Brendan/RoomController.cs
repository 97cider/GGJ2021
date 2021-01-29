using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomController : MonoBehaviour
{
    public GameObject _tileMap;
    public GameObject _scene;
    public GameObject _nextMap;
    private GameObject _currentMap;
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
        var new_level = this.pickRandomRoom();
        while (new_level == _scene.GetComponent<SceneHandler>().tileMap){
            new_level = this.pickRandomRoom();
        }
        //_currentMap.GetComponent<SceneHandler>().tileMap
        Destroy(_currentMap);
        _scene.GetComponent<SceneHandler>().tileMap = new_level;
        _currentMap = GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap);
    }
    public List<GameObject> _LoadedRooms;
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
