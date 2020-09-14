using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;

    int zoomedInFOV = 20;
    int zoomedOutFOV = 60;

    float zoomOutSensitivity = 1f;
    float zoomInSensitivity = 0.4f;


    [SerializeField] Weapon weapon;

    RigidbodyFirstPersonController fpsController;

    private void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            fpsCamera.fieldOfView = zoomedInFOV;
            fpsController.mouseLook.XSensitivity = zoomInSensitivity;
            fpsController.mouseLook.YSensitivity = zoomInSensitivity;

            weapon.WeaponZoomedPos(true);
        }
        else
        {
            fpsCamera.fieldOfView = zoomedOutFOV;
            fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
            fpsController.mouseLook.YSensitivity = zoomOutSensitivity;

            weapon.WeaponZoomedPos(false);
        }
    }
}
