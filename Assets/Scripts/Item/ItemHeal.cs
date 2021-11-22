using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemHeal : BasicItem
{
    [SerializeField] int healPower;
    public static Action<int> OnHealPlayer;
    private void OnEnable()
    {
        AkSoundEngine.PostEvent("medkit_spawn", gameObject);
    }
    public override void OnHit(Weapon.DamageInfo damageInfo)
    { 
        AkSoundEngine.PostEvent("medkit_heal", gameObject);
        OnHealPlayer?.Invoke(healPower);
        Destroy(gameObject);
    }
}
