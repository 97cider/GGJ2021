using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public GameObject _roomController;
    public Transform targetLocation;
    public bool hasReachedDest;
    public GameObject moveAwayFrom;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        hasReachedDest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLocation != null) {
            var tmpCam = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            if (tmpCam != targetLocation.position && !hasReachedDest)
            {
                Debug.Log("not there yet");
                var tmpvect = new Vector3(targetLocation.position.x, targetLocation.position.y, this.transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, tmpvect, speed*Time.deltaTime);    
            }
            else{
                // the target destination has been reached
                Debug.Log("has reached target");
                hasReachedDest = true;
                _roomController.GetComponent<RoomController>().destroyRoom();
                _roomController.GetComponent<RoomController>()._currentMap =   _roomController.GetComponent<RoomController>().scene.GetComponent<SceneHandler>().tileMap;
                _roomController.GetComponent<RoomController>().setNextLevel();
                //var next_level = _roomController.GetComponent<RoomController>().setNextLevel();
                //_roomController.scene.GetComponent<SceneHandler>().tileMap.GetComponent<Room>().nextLevel = next_level;
                targetLocation = null;
                hasReachedDest = false;
            }
        }
    }
}
