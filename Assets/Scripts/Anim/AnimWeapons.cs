using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimWeapons : MonoBehaviour
{
    [SerializeField] Animator myAnim;
    public static Action<WeaponType> OnSetNewWeapon;
    public static Action<bool> OnSetReloadMode;
    public static Action OnStartAnim;
    public enum WeaponType
    {
        Gun, Submachinegun, Shotgun
    }
    private void OnEnable()
    {
        OnSetNewWeapon += SetNewWeapon;
        OnSetReloadMode += SetReloadMode;
        OnStartAnim += StartAnim;
    }
    private void OnDisable()
    {
        OnSetNewWeapon -= SetNewWeapon;
        OnSetReloadMode -= SetReloadMode;
        OnStartAnim -= StartAnim;
    }
    void Start()
    {
        SetNewWeapon(WeaponType.Gun);
        SetReloadMode(false);
    }

    public void SetNewWeapon(WeaponType wt)
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
