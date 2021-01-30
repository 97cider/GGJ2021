using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    // Start is called before the first frame update
    public string AccessoryName;
    public Texture accessorySprite;

    // Character controller based modfiers
    public float movementSpeedModifier;
    public float jumpSpeedModifier;
    public int maxJumpScalar;

    // Player stats based modifiers
    public float maxHPModifier;
    public float baselineDamageModifier;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
