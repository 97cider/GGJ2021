using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject characterSprite;


    [SerializeField] private bool shakeCameraOnJump;

    [SerializeField] private bool shakeCameraOnLand;

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

    public void ShakePlayerScale()
    {
        iTween.ShakeScale(characterSprite, new Vector3(1.0f, -0.1f, 1.0f), 0.15f);
    }
}
