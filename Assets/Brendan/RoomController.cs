using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomController : MonoBehaviour
{
    public GameObject _tileMap;
    public GameObject _scene;
    public GameObject _nextMap;
    // Start is called before the first frame update
    void Start()
    {
        //this._getTilemaps();
        if (_LoadedRooms.Count > 0){
            var current_level = this.pickRandomRoom();
            _scene.GetComponent<SceneHandler>().tileMap = current_level;
            GameObject.Instantiate(_scene.GetComponent<SceneHandler>().tileMap);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
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
