using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyToSpawn, this.gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
