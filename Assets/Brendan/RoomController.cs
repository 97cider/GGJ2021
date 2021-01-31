using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Exit;
public class RoomController : MonoBehaviour
{
    public GameObject scene;
    public GameObject _currentMap;
    public GameObject _nextMap;
    public GameObject _exitTest;
    public Camera mainCamera;
    private Vector3 leftMost, rightMost, downMost;
    public List<GameObject> _LoadedRooms;
    public UiController uiController;
    public GameObject player;
    public ItemController itemController;
    public int movement;
    private float zoffset;
    // Start is called before the first frame update
    void Start()
    {

        //zoffset = mainCamera.transform.position.z;
        zoffset = 0;
        //this._getTilemaps();
    
            var current_level = this.pickRandomRoom();
            _currentMap = current_level;
            this._currentMap = GameObject.Instantiate(_currentMap);
            this._currentMap.GetComponent<Room>().roomController = this.gameObject;
            this.setNextLevel();
            //_nextMap = next_level;

            // Spawn the player
            Transform t =  _currentMap.transform.GetChild(0).transform.Find("Spawn");
            //Vector3 spawner = _currentMap.transform.Find("Spawn").position;
            Debug.Log($"Player is spawning at {t.position}");
            Accessory a = itemController.getRandomAccessory();
            Weapon w = itemController.getRandomWeapon();

            player.GetComponent<PlayerStats>().setCurrentAccessory(a);
            player.GetComponent<PlayerStats>().EquipWeapon(w);
            
            player.GetComponent<PlayerStats>().setMaxHP(a.maxHPModifier);
            player.transform.position = t.position;
            //player = Instantiate(player, t.position, Quaternion.identity) as GameObject;
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
        while (new_level.gameObject == _currentMap.gameObject){
            new_level = this.pickRandomRoom();
        }
        Debug.LogError(new_level);
        Debug.LogError(_currentMap);
        // Set the current level in the room controller
        //_nextMap = new_level;
        // Set the current level in the scene
        _nextMap = new_level;
        this._nextMap = new_level;
    }
    public void destroyRoom(){
        Destroy(this._currentMap);
    }
    public void setLevel(){
        if (_nextMap != null){
            //_currentMap = _nextMap;
            var new_level = this.pickRandomRoom();
            //Remember to do check
            while (new_level == _currentMap){
                new_level = this.pickRandomRoom();
            }
            if (_currentMap.GetComponent<Room>().usedExit == exitType.Left){
                // We exit stage left. Spawn a room to the left, pan to it, and then set the current room to that room
                // Instantiate the next level far away from the left
                Debug.LogError("DOING LEFT");
                var _currentNextMap = Instantiate(_nextMap, leftMost+new Vector3(-movement, 0,0), Quaternion.identity) as GameObject;
                this._currentMap.GetComponent<Room>().roomController = this.gameObject;
                _nextMap = _currentNextMap;
                this._nextMap.GetComponent<Room>().roomController = this.gameObject;
                leftMost = leftMost+new Vector3(-movement, 0,0);
                rightMost = leftMost;
                downMost = leftMost;
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                this._currentMap.GetComponent<Room>().assignExits();
                //var next_level_exit = _nextMap.transform.position;
                //Destroy(_currentMap);

            }
            else if (_currentMap.GetComponent<Room>().usedExit == exitType.Right){
                var _currentNextMap = Instantiate(_nextMap, rightMost+new Vector3(movement, 0,0), Quaternion.identity) as GameObject;
                _nextMap = _currentNextMap;
                rightMost = rightMost+new Vector3(movement, 0,0);
                leftMost = rightMost;
                downMost = rightMost;
                Debug.LogError("DOING RIGHT");
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                this._nextMap.GetComponent<Room>().roomController = this.gameObject;
                this._currentMap.GetComponent<Room>().assignExits();
                //var next_level_exit = _nextMap.transform.position;

            }
            else if (_currentMap.GetComponent<Room>().usedExit == exitType.Down){
                var _currentNextMap = Instantiate(_nextMap, downMost+new Vector3(0, -(movement-5),0), Quaternion.identity) as GameObject;
                _nextMap = _currentNextMap;
                mainCamera.GetComponent<CameraController>().targetLocation = _currentNextMap.transform;
                this._nextMap.GetComponent<Room>().roomController = this.gameObject;

                downMost = downMost+new Vector3(0, -(movement-5),0);
                rightMost = downMost;
                leftMost = downMost;
                Debug.LogError("DOING DOWN");
                this._currentMap.GetComponent<Room>().assignExits();
                //var next_level_exit = _nextMap.transform.position;
            }
            else{
                Debug.Log("test");
            }
            //_currentMap = GameObject.Instantiate(_currentMap);
            //_nextMap = new_level;
        }
        else{
                Debug.Log("Something is borked");
            }
    }
    public void movePlayer(){
       var t = _currentMap.transform.GetChild(0).transform.Find("Spawn");
       Debug.LogError($"Teleporint player, currently at {player.transform.position} to {t.position}");
       player.transform.position = t.position;
    }
    public void setNextRoomLevel(){
        this._nextMap.GetComponent<Room>().roomController = this.gameObject;
    }
    private GameObject pickRandomRoom(){
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
