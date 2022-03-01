using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public enum WeaponType
    {
        Gun,
        SubmachineGun,
        Shotgun
    }

    public KeyCode selectWeapon1;
    public KeyCode selectWeapon2;
    public KeyCode selectWeapon3;

    [SerializeField] List<Weapon> weapon;
    
    public static Action<WeaponType> OnWeaponChanged;
    public static Action<bool> OnSetReloadMode;
    public static Action OnStartAnim;
    public float scaleBulletHole = 0.3f;
    public float sumDifBulletHole = 1.0f;
    public float bulletZDistance = 1.0f;
    int actualWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < weapon.Count; i++)
        {
            weapon[i] = Instantiate<Weapon>(weapon[i]);
            weapon[i].InitWeapon();
        }
        weapon[actualWeapon].InitWeapon();
        SelectWeapon(WeaponType.Gun);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (weapon[actualWeapon].fireTime < weapon[actualWeapon].fireRate)
            {
                weapon[actualWeapon].fireTime += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && actualWeapon != (int)WeaponType.Gun)
            {
                SelectWeapon(WeaponType.Gun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && actualWeapon != (int)WeaponType.SubmachineGun)
            {
                SelectWeapon(WeaponType.SubmachineGun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && actualWeapon != (int)WeaponType.Shotgun)
            {
                SelectWeapon(WeaponType.Shotgun);
            }

            if (weapon[actualWeapon].GetShootingMode())
            {
                TryShoot(weapon[actualWeapon].damageInfo);
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                TryReload();
            }
        }
    }
    public void TryShoot(Weapon.DamageInfo damageInfo)
    {
        if (weapon[actualWeapon].ammo > 0)
        {
            AkSoundEngine.SetSwitch("gun_shoot", weapon[actualWeapon].shootWithAmmo, gameObject);
            AkSoundEngine.PostEvent("shoot", gameObject);
            OnStartAnim?.Invoke();
            Vector3 mousePosition = weapon[actualWeapon].mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            Debug.Log(hit.transform.name);
            if (hit.collider != null && hit.transform.gameObject.layer == weapon[actualWeapon].targetLayer)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit(damageInfo);
                weapon[actualWeapon].SetHitCrosshair();
                StartCoroutine(HitShoot());
            }
            else if (hit.collider != null)
            {
                
                int newSortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                newSortingOrder++;
                GameObject bulletObj =  Instantiate(weapon[actualWeapon].BulletHolePrefab,transform);
                bulletObj.transform.position = mousePosition2D;
                //hit.transform.position.z
                bulletObj.transform.position = new Vector3(bulletObj.transform.position.x, bulletObj.transform.position.y,
                    hit.transform.position.z);
                
                ResizeBasedInZ aux = bulletObj.GetComponent<ResizeBasedInZ>();
                aux.diffToSum = sumDifBulletHole;
                aux.SetDistanceMark(bulletZDistance, scaleBulletHole);
                aux.AutoScaleNow();
                bulletObj.GetComponentInChildren<SpriteRenderer>().sortingOrder = newSortingOrder;
                
                //ResetMultiplier?.Invoke(false);
                weapon[actualWeapon].SetMultiplier(false);
            }
            weapon[actualWeapon].fireTime = 0;
            
            //UpdateUIAmmo?.Invoke(maxAmmo);
            weapon[actualWeapon].UpdateAmmo();
            weapon[actualWeapon].ammo--;
            if (weapon[actualWeapon].ammo <= 0)
            {
                //OutOfAmmoCrosshair?.Invoke();
                weapon[actualWeapon].SetOutOfAmmoCrosshair();
            }
        }
        else
        {
            if (!IsEventPlayingOnGameObject("shoot", gameObject))
            {
                AkSoundEngine.SetSwitch("gun_shoot", weapon[actualWeapon].shootWithinAmmo, gameObject);
                AkSoundEngine.PostEvent("shoot", gameObject);
            }
        }
    }
    public void TryReload()
    {
        if(weapon[actualWeapon].ammo < weapon[actualWeapon].maxAmmo)
        {
            //OutOfAmmoCrosshair?.Invoke();
            weapon[actualWeapon].SetOutOfAmmoCrosshair();
            StartCoroutine(Reloading());
        }
    }
    IEnumerator Reloading()
    {
        weapon[actualWeapon].isReloading = true;
        OnSetReloadMode?.Invoke(true);
        OnStartAnim?.Invoke();
        AkSoundEngine.SetSwitch("gun_reload", weapon[actualWeapon].reloadingSound, gameObject);
        AkSoundEngine.PostEvent("reload", gameObject);
        yield return new WaitForSeconds(weapon[actualWeapon].reloadTime);
        weapon[actualWeapon].ammo = weapon[actualWeapon].maxAmmo;
        //ResetUIAmmo?.Invoke();
        weapon[actualWeapon].ResetAmmo();
        //NormalCrosshair?.Invoke();
        weapon[actualWeapon].SetNormalCrosshair();
        weapon[actualWeapon].isReloading = false;
        OnSetReloadMode?.Invoke(false);

    }
    IEnumerator HitShoot()
    {
        yield return new WaitForSeconds(0.2f);
        //NormalCrosshair?.Invoke();
        weapon[actualWeapon].SetNormalCrosshair();
    }
    void SelectWeapon(WeaponType newValue)
    {
        actualWeapon = (int) newValue;
        //for (int i = 0; i < weapon.Count; i++)
        //{
        //    if (weapon[actualWeapon] != weapon[i])
        //    {
        //        weapon[i].SetActive(false);
        //    }
        //    else
        //    {
        //        weapon[i].SetActive(true);
        //    }
        //}
        OnWeaponChanged?.Invoke(newValue);
    }
    public WeaponType GetActualWeapon()
    {
        return (WeaponType)actualWeapon;
    }

    public static bool IsEventPlayingOnGameObject(string eventName, GameObject go)
    {
        uint[] playingIds = new uint[50];
        uint testEventId = AkSoundEngine.GetIDFromString(eventName);
        uint count = (uint)playingIds.Length;
        AKRESULT result = AkSoundEngine.GetPlayingIDsFromGameObject(go, ref count, playingIds);

        for (int i = 0; i < count; i++)
        {
            uint playingId = playingIds[i];
            uint eventId = AkSoundEngine.GetEventIDFromPlayingID(playingId);

            if (eventId == testEventId)
                return true;
        }

        return false;
    }
}
