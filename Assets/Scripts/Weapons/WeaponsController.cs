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
    int actualWeapon;

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
            if (Input.GetKeyDown(KeyCode.Alpha1) && actualWeapon != 0)
            {
                SelectWeapon(WeaponType.Gun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && actualWeapon != 1)
            {
                SelectWeapon(WeaponType.SubmachineGun);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && actualWeapon != 2)
            {
                SelectWeapon(WeaponType.Shotgun);
            }
        }
        else if(Input.GetKeyDown(selectWeapon2) && actualWeapon != 1)
        {
            SelectWeapon(WeaponType.SubmachineGun);
        }
        else if (Input.GetKeyDown(selectWeapon3) && actualWeapon != 2)
        {
            SelectWeapon(WeaponType.Shotgun);
        }
    }
    void SelectWeapon(WeaponType newValue)
    {
        actualWeapon = (int) newValue;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[actualWeapon] != weapons[i])
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
}
