using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Exits {Left, Right, Down}; 
    public List<Exits> availableExits;
    public List<GameObject> enemies;
    public bool isCleared;
    void Start()
    {
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
