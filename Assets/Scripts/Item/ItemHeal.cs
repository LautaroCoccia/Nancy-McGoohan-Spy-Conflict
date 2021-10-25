﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemHeal : BasicItem
{
    [SerializeField] int healPower;
    public static Action<int> OnHealPlayer;
    public override void OnHit(TypeOfDamage.DamageType typeOfDamage)
    { 
        OnHealPlayer?.Invoke(healPower);
        Destroy(gameObject);
    }
}
