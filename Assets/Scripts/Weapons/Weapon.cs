﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo;
    public int maxAmmo;
    public int targetLayer;
    public float fireRate;
    public float fireTime;
    public float reloadTime;
    public Camera mainCamera;

    public static Action ResetUIAmmo;
    public static Action<float> UpdateUIAmmo;
    public static Action NormalCrosshair;
    public static Action HitCrosshair;
    public static Action OutOfAmmoCrosshair;
    protected bool isReloading;
    public static Action<Vector2, int> SetBulletholes;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        isReloading = false;
    }
    public void Shoot()
    {
        if (ammo > 0)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer && hit.transform.tag != "EnemyShield")
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit();
                HitCrosshair?.Invoke();
                StartCoroutine(HitShoot());
            }
            else
            {
                SetBulletholes?.Invoke(mousePosition2D, hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder);

            }
            fireTime = 0;
            UpdateUIAmmo?.Invoke(maxAmmo);
            ammo--;
            if(ammo <= 0)
            {
                OutOfAmmoCrosshair?.Invoke();
            }
        }
    }
    public void ShotShotgun()
    {
        if (ammo > 0)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit();
                HitCrosshair?.Invoke();
                StartCoroutine(HitShoot());

            }
            else
            {
                SetBulletholes?.Invoke(mousePosition2D, hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            }
            fireTime = 0;
            UpdateUIAmmo?.Invoke(maxAmmo);
            
            ammo--;
            if(ammo <= 0)
            {
                OutOfAmmoCrosshair?.Invoke();
            }
        }
    }
    public void Reload()
    {
        if (ammo < maxAmmo && Input.GetKeyDown(KeyCode.R) && !PauseMenu.GetPause())
        {
            OutOfAmmoCrosshair?.Invoke();
            StartCoroutine(Reloading());
        }
    }
    IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        ResetUIAmmo?.Invoke();
        NormalCrosshair?.Invoke();
        isReloading = false;
    }
    IEnumerator HitShoot()
    {
        yield return new WaitForSeconds(0.1f);
        NormalCrosshair?.Invoke();
    }
}