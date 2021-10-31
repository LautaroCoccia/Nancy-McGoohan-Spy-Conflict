using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int ammo;
    public int maxAmmo;
    public int targetLayer;
    public float fireRate;
    public float fireTime;
    public float reloadTime;
    public Camera mainCamera;
    public GameObject BulletHolePrefab;

    public static Action ResetUIAmmo;
    public static Action<float> OnAmmoChange;
    public static Action NormalCrosshair;
    public static Action HitCrosshair;
    public static Action OutOfAmmoCrosshair;
    //public static Action<Vector2, int> SetBulletholes;
    public static Action<bool> ResetMultiplier;
    public enum DamageInfo
    {
        normal,
        strong
    }
    public DamageInfo damageInfo;
    public enum ShootingMode
    {
        semiautomatic,
        automatic
    }
    public ShootingMode shootingMode;

    public bool isReloading;
   
    // Start is called before the first frame update
    public void InitWeapon()
    {
        mainCamera = Camera.main;
        isReloading = false;
        SetNormalCrosshair();
    }

    public bool GetShootingMode()
    {
        switch (shootingMode)
        {
            case ShootingMode.semiautomatic:
                if (Input.GetMouseButtonDown(0) && fireTime >= fireRate)
                    return true;
                else
                    return false;
            case ShootingMode.automatic:
                if (Input.GetMouseButton(0) && fireTime >= fireRate)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    public void ResetAmmo()
    {
        ResetUIAmmo?.Invoke();
    }
    public void UpdateAmmo()
    {
        OnAmmoChange?.Invoke(maxAmmo);
    }
    public void SetNormalCrosshair()
    {
        NormalCrosshair?.Invoke();
    }
    public void SetHitCrosshair()
    {
        HitCrosshair?.Invoke();
    }
    public void SetOutOfAmmoCrosshair()
    {
        OutOfAmmoCrosshair?.Invoke();
    }
    public void SetMultiplier(bool isActive)
    {
        ResetMultiplier?.Invoke(isActive);
    }
}