using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    // A list of random attributes to pull from
    public List<Attribute> availableModifications;
    public List<Accessory> availableAccessories;
    private GameObject player;
    public Accessory getRandomAccessory(){
        var accessory = Random.Range(0, availableAccessories.Count);
        return availableAccessories[accessory];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
