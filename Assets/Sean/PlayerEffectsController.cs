using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    public UiController ui;
    [SerializeField] private GameObject characterSprite;


    [SerializeField] private bool shakeCameraOnJump;

    [SerializeField] private bool shakeCameraOnLand;

    [SerializeField] private bool shakeCameraOnAttack;

    [SerializeField] private bool shakeCameraOnHit;

    void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }
    public void ShakeCameraOnJump()
    {
        if(!shakeCameraOnJump) return;  
        iTween.ShakePosition(mainCamera, new Vector3(0.0f, 0.04f, 0.0f), 0.15f);
    }

    public void ShakeCameraOnLand()
    {
        if(!shakeCameraOnLand) return;  
        iTween.ShakePosition(mainCamera, new Vector3(0.0f, -0.04f, 0.0f), 0.15f);
        // ShakePlayerScale();
    }

    public void updateGui(){
        ui.updateCurrentHealth();
    }
    public void ShakePlayerScale()
    {
        iTween.ShakeScale(characterSprite, new Vector3(1.0f, -0.1f, 1.0f), 0.15f);
    }

    public void ShakeCameraOnAttack()
    {
        if(!shakeCameraOnAttack) return;
        iTween.ShakePosition(mainCamera, new Vector3(0.1f, 0.0f, 0.0f), 0.10f); 
    }

    public void ShakeCameraOnHit()
    {
        if(!shakeCameraOnHit) return;
        iTween.ShakePosition(mainCamera, new Vector3(0.2f, 0.2f, 0.0f), 0.30f);
    }
}
