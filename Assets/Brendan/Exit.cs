using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Exit : MonoBehaviour
{
    public GameObject room;
    public enum exitType {Left, Right, Down};
    // Start is called before the first frame update
    public exitType thisExit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
            Debug.LogWarning("Col");
            GameObject obj = other.gameObject;
            if (obj.layer == 6){
                //room.GetComponent<RoomController>().scene..GetComponent<SceneHandler>().tileMap.GetComponent<Room>().usedExit.GetComponent<Exit>().thisExit = thisExit;
                //room.GetComponent<RoomController>().setLevel();
                Room r = room.GetComponent<Room>();
                room.GetComponent<Room>().usedExit = thisExit;
                Debug.LogError(r.usedExit);
                room.GetComponent<Room>().roomController.GetComponent<RoomController>().setLevel();
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
    }
}
