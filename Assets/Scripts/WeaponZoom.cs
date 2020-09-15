using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;

    int zoomedInFOV = 20;
    int zoomedOutFOV = 60;

    float zoomOutSensitivity = 1f;
    float zoomInSensitivity = 0.4f;

    [SerializeField] Weapon weapon;

    [SerializeField] RigidbodyFirstPersonController fpsController;

    [SerializeField] Image reticle;


    private void OnDisable()
    {
        ZoomOut();
    }


    void Update()
    {
        if (Input.GetMouseButton(1) && (weapon.name != "Skorpion VZ"))    //zoomed button
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        //fpsCamera.fieldOfView = zoomedInFOV;

        fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, zoomedInFOV, Time.deltaTime * 15);    //slowly change the fov

        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;

        weapon.WeaponZoomedPos(true);   //*to method*

        if (weapon.name == "UMP-45")    //without this, the UMP still has the reticle on while zoomed in
        {
            reticle.enabled = false;
        }
    }

    private void ZoomOut()
    {
        //fpsCamera.fieldOfView = zoomedOutFOV;

        fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, zoomedOutFOV, Time.deltaTime * 15);

        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;

        if (weapon.name != "Skorpion VZ")   //without this there is an error as the skorpion is trying to moved to its zoomedoutpos which doesnt exist as it only stays hitfired
        {
            weapon.WeaponZoomedPos(false);
        }

        if (reticle != null)    //without this, it causes an error when the game is stopped as the OnDisable method runs this method again but the reference no longer exists
        {
            reticle.enabled = true;
        }
    }
}
