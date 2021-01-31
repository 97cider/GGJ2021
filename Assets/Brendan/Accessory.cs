using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    // Start is called before the first frame update
    public string AccessoryName;
    public Sprite accessorySprite;
    public string description;

    // Character controller based modfiers
    public float movementSpeedModifier;
    public float jumpSpeedModifier;
    public int maxJumpScalar;

    // Player stats based modifiers
    public int maxHPModifier;
    public int baselineDamageModifier;
    public void getRichTextDescription(){
        var mhp_color = "";
        var speed_color = "";
        var jcolor = "";
        var mx_color = "";
        if (maxHPModifier > 3){
            mhp_color = "green";
        }
        else if (maxHPModifier == 3){
            mhp_color = "grey";
        }
        else{
            mhp_color = "red";
        }
        if (movementSpeedModifier > 1){
            speed_color = "green";
        }
        else if (movementSpeedModifier == 1){
            speed_color = "grey";
        }
        else{
            speed_color = "red";
        }
        if (jumpSpeedModifier > 1){
            jcolor = "green";
        }
        else if (jumpSpeedModifier == 1){
            jcolor = "grey";
        }
        else{
            jcolor = "red";
        }
        if (maxJumpScalar > 1){
            mx_color = "green";
        }
        else if (maxJumpScalar == 1){
            mx_color = "grey";
        }
        else{
            mx_color = "red";
        }
        var strs = $"<color={mhp_color}>{maxHPModifier}</color>: Max HP \n<color={speed_color}>{movementSpeedModifier}</color>: Speed \n<color={jcolor}>{jumpSpeedModifier}</color>: Jump Speed \n<color={mx_color}>{maxJumpScalar}</color>: Max Jumps \n";
        this.description = strs;
    }
    void Start()
    {
        getRichTextDescription();    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(this.description);
    }
}
