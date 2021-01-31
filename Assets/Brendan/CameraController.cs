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
                _roomController.GetComponent<RoomController>().player.GetComponent<CharacterController2D>().m_Rigidbody2D.velocity = new Vector2(0,0); 
                _roomController.GetComponent<RoomController>().player.GetComponent<CharacterController2D>().m_Rigidbody2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else{
                // the target destination has been reached
                Debug.Log("has reached target");
                hasReachedDest = true;
                _roomController.GetComponent<RoomController>().destroyRoom();
                _roomController.GetComponent<RoomController>()._currentMap = _roomController.GetComponent<RoomController>()._nextMap;
                Debug.LogError(_roomController.GetComponent<RoomController>()._currentMap);
                _roomController.GetComponent<RoomController>().setNextLevel();
                _roomController.GetComponent<RoomController>().movePlayer();
                _roomController.GetComponent<RoomController>().player.GetComponent<PlayerStats>().canMove = true;
                _roomController.GetComponent<RoomController>().player.GetComponent<CharacterController2D>().m_Rigidbody2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                targetLocation = null;
                hasReachedDest = false;
            }
        }
    }
}
