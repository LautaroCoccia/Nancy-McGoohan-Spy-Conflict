using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons;
    public static Action<int> UpdateUIAmmoImage;
    int actualWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon(0);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && actualWeapon != 0)
        {
            SelectWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && actualWeapon != 1)
        {
            SelectWeapon(1);
        }
    }
    void SelectWeapon(int newValue)
    {
        actualWeapon = newValue;
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
        UpdateUIAmmoImage?.Invoke(actualWeapon);
    }
}
