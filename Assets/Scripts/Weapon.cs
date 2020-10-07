using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem muzzleFlash2;

    [SerializeField] GameObject hitEffect;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 20f;

    [SerializeField] Transform regularZoom;
    [SerializeField] Transform aimZoom;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;
    [SerializeField] float timeBetweenShots = 0.5f;


    void OnEnable() //these 2 methods stop the weapon firerate from being exploited by switching to and fro between 2 weapons
    {
        if (!canShoot)
        {
            Invoke("WaitToShoot", timeBetweenShots);
        }
    }
    void WaitToShoot()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetAmmoCount(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;   //trigger/fire delay
        if (ammoSlot.GetAmmoCount(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceAmmo(ammoType);
        }
        else
        {
            Debug.Log("out of ammo");
        }
        yield return new WaitForSeconds(timeBetweenShots);  //the trigger delay amount
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
        if (muzzleFlash2 != null)   //for the skorpion
        {
            muzzleFlash2.Play();
        }
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) return; //if it didnt hit an 'Enemy'

            target.TakeDamage(damage);
        }
        else   //if you shot into the sky or the 'range' was too far
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject vfx = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));    //creates the object thats holding the particle effect in the location where the raycast hit, with the rotation of the surface so it looks like the sparks are coming off the surface
        Destroy(vfx, 0.1f);
    }

    public void WeaponZoomedPos(bool zoomed) 
    {
        if (zoomed)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimZoom.localPosition, Time.deltaTime * 15);    //lerp so the weapon doesnt snap

            transform.localRotation = Quaternion.Lerp(transform.localRotation, aimZoom.localRotation, Time.deltaTime * 15); //rotation is only really needed because of the UMP needing it


            //transform.localRotation = aimZoom.localRotation;
            //transform.localPosition = aimZoom.localPosition;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, regularZoom.localPosition, Time.deltaTime * 15);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, regularZoom.localRotation, Time.deltaTime * 15);


            //transform.localRotation = regularZoom.localRotation;
            //transform.localPosition = regularZoom.localPosition;
        }
    }
}
