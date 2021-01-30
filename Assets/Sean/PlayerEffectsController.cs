using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private bool shakeCameraOnJump;

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
        if(!shakeCameraOnJump) return;  
        iTween.ShakePosition(mainCamera, new Vector3(0.0f, -0.04f, 0.0f), 0.15f);
    }
}
