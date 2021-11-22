using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimWeapons : MonoBehaviour
{
    [SerializeField] Animator myAnim;
    
    private void OnEnable()
    {
        WeaponsController.OnWeaponChanged += SetNewWeapon;
        WeaponsController.OnSetReloadMode += SetReloadMode;
        WeaponsController.OnStartAnim += StartAnim;
    }
    private void OnDisable()
    {
        WeaponsController.OnWeaponChanged -= SetNewWeapon;
        WeaponsController.OnSetReloadMode -= SetReloadMode;
        WeaponsController.OnStartAnim -= StartAnim;
    }
    void Start()
    {
        SetNewWeapon(WeaponsController.WeaponType.Gun);
        SetReloadMode(false);
    }
    public void SetNewWeapon(WeaponsController.WeaponType wt)
    {
        myAnim.SetInteger("WeaponType", (int)wt);
    }
    public void SetReloadMode(bool shootmodeON)
    {
        myAnim.SetBool("ShootTvsReloadF", !shootmodeON);
    }
    public void StartAnim()
    {
        myAnim.SetTrigger("StartAnim");
    }
}