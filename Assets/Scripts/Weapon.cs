﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    float range = 100f;
    float damage = 20f;

    [SerializeField] Transform regularZoom;
    [SerializeField] Transform aimZoom;

    [SerializeField] Ammo ammoSlot;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (ammoSlot.GetAmmoCount() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceAmmo();
        }
        else
        {
            Debug.Log("out of ammo");
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) return;

            target.TakeDamage(damage);
        }
        else
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
            transform.localRotation = aimZoom.localRotation;
            transform.localPosition = aimZoom.localPosition;
        }
        else
        {
            transform.localRotation = regularZoom.localRotation;
            transform.localPosition = regularZoom.localPosition;
        }
    }
}
