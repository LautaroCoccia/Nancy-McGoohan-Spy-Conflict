using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemHeal : BasicItem
{
    [SerializeField] Animator animator;
    [SerializeField] int healPower;
    public static Action<int> OnHealPlayer;
    private void OnEnable()
    {
        animator.SetTrigger("StartAnim");
        AkSoundEngine.PostEvent("medkit_spawn", gameObject);
    }
    private void Update() 
    {
    }
    public override void OnHit(Weapon.DamageInfo damageInfo)
    { 
        AkSoundEngine.PostEvent("medkit_heal", gameObject);
        OnHealPlayer?.Invoke(healPower);
        Destroy(gameObject);
    }
}
