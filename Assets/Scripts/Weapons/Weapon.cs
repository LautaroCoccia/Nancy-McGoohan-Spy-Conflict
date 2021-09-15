using System.Collections;
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
    public static Action<int> UpdateUICrosshair;

    enum WeaponStates
    {
        Idle,
        Shoot,
        OutOfAmmo
    }
    WeaponStates weaponStates;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        weaponStates = WeaponStates.Idle;
        UpdateUICrosshair?.Invoke((int)(weaponStates));
    }
    private void Update()
    {
        
    }
    public void Shot()
    {
        if(ammo > 0)
        {
            weaponStates = WeaponStates.Shoot;
            UpdateUICrosshair?.Invoke((int)(weaponStates));
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer && hit.transform.tag != "EnemyShield")
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit();
            }
            ammo--;
            fireTime = 0;
            UpdateUIAmmo?.Invoke(maxAmmo);

            weaponStates = WeaponStates.Idle;
            UpdateUICrosshair?.Invoke((int)(weaponStates));
        }
        else
        {
            weaponStates = WeaponStates.OutOfAmmo;
            UpdateUICrosshair?.Invoke((int)(weaponStates));
        }
    }
    public void ShotShotgun()
    {
        if (ammo > 0)
        {
            weaponStates = WeaponStates.Shoot;
            UpdateUICrosshair?.Invoke((int)(weaponStates));

            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit();
            }
            ammo--;
            fireTime = 0;
            UpdateUIAmmo?.Invoke(maxAmmo);

            weaponStates = WeaponStates.Idle;
            UpdateUICrosshair?.Invoke((int)(weaponStates));
        }
        else
        {
            weaponStates = WeaponStates.OutOfAmmo;
            UpdateUICrosshair?.Invoke((int)(weaponStates));
        }
    }
    public void Reload()
    {
        if (ammo != maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reloading());
            
        }
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        ResetUIAmmo?.Invoke();
        UpdateUICrosshair?.Invoke((int)(weaponStates));
    }
}