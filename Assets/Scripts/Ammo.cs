using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    
    public void ReduceAmmo()
    {
        ammoAmount--;
    }

    public int GetAmmoCount()
    {
        return ammoAmount;
    }
}
