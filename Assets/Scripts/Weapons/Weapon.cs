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

    public static Action ResetUIAmmo;
    public static Action<float> UpdateUIAmmo;
    public static Action NormalCrosshair;
    public static Action HitCrosshair;
    public static Action OutOfAmmoCrosshair;
    public static Action<Vector2, int> SetBulletholes;
    public static Action<bool> ResetMultiplier;
    protected bool isReloading;
   
    // Start is called before the first frame update
    /*void Start()
    {
        mainCamera = Camera.main;
        isReloading = false;
    }
    public void Shoot(int typeOfDamage)
    {
        if (ammo > 0)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit(typeOfDamage);
                HitCrosshair?.Invoke();
                StartCoroutine(HitShoot());
            }
            else
            {
                SetBulletholes?.Invoke(mousePosition2D, hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
                ResetMultiplier?.Invoke(false);
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
    }*/
}