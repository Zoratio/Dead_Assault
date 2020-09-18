using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;  //this array is displaying the private internal classes information



    [System.Serializable]   //show the content of this class in the inspector
    private class AmmoSlot  //this class is only visible to the Ammo class
    {
        public AmmoType ammoType;   //uses the Enum from the AmmoType script
        public int ammoAmount;
    }



    public void ReduceAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--; //reduce the ammo of the correct slot as we know which type is being used
    }

    public void IncreaseAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount; //reduce the ammo of the correct slot as we know which type is being used
    }

    public int GetAmmoCount(AmmoType ammoType)
    { 
        return GetAmmoSlot(ammoType).ammoAmount;    //return the ammo count of the correct slot as we know which type is being used
    }



    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)  //if the array slot matches the enum value, then you know which slot in the inspector is correct
            {
                return slot;
            }
        }
        return null;
    }
}
