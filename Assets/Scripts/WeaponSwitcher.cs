using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0; //start with this weapon
        
    void Start()
    {
        SetWeaponActive();  //picks the first weapon instantly so the player doesnt spawn without any weapons enabled
    }

    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();  //either key numbers or 
        ProcessScrollWheel();   //scroll wheel

        if (previousWeapon != currentWeapon)    //if they have changed the weapon since last update call
        {
            SetWeaponActive();
        }
    }

    private void ProcessKeyInput()  //WHEN SWITCHING WEAPONS, I SHOULD MAKE THE WEAPON MOVE DOWN OFF THE SCREEN AND ANOTHER SHOULD RISE UP
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;  //skorpion
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;  //UMP
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;  //M4A1
        }
    }

    private void ProcessScrollWheel()  //WHEN SWITCHING WEAPONS, I SHOULD MAKE THE WEAPON MOVE DOWN OFF THE SCREEN AND ANOTHER SHOULD RISE UP
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;    //weapon youre checking to see if its the correct one for the input from the player

        foreach(Transform weapon in transform)  //looks at all the 'Weapon' gameobjects children
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    
}
