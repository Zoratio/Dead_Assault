using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandle : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] GameObject weapons;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        //GetComponentInChildren<Weapon>().enabled = false; //*if i just wanted to disable the muzzle flash*
        weapons.SetActive(false);

        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
