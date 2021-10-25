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

    [SerializeField] List<GameObject> weapons;
    public static Action<WeaponType> OnWeaponChanged;
    WeaponType actualWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon(WeaponType.Gun);
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GetPause())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && actualWeapon != WeaponType.Gun)
            {
                SelectWeapon(WeaponType.Gun);
                AnimWeapons.OnSetNewWeapon(WeaponType.Gun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && actualWeapon != WeaponType.SubmachineGun)
            {
                SelectWeapon(WeaponType.SubmachineGun);
                AnimWeapons.OnSetNewWeapon(WeaponType.SubmachineGun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && actualWeapon != WeaponType.Shotgun)
            {
                SelectWeapon(WeaponType.Shotgun);
                AnimWeapons.OnSetNewWeapon(WeaponType.Shotgun);
            }
        }
    }
    void SelectWeapon(WeaponType newValue)
    {
        actualWeapon = newValue;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[(int)actualWeapon] != weapons[i])
            {
                weapons[i].SetActive(false);
            }
            else
            {
                weapons[i].SetActive(true);
            }
        }
        OnWeaponChanged?.Invoke(newValue);
    }
    public WeaponType GetActualWeapon()
    {
        return actualWeapon;
    }
}
