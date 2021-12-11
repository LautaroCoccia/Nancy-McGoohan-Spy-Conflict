﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnHitBasicEnemy : MonoBehaviour, IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] int score;
    public static Action<int> OnTakeDamage;
    public static Action OnKill;
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        baseEnemy.DeathScream();
        baseEnemy.InstanciateBlood();
        OnTakeDamage?.Invoke(score);
        OnKill();
        baseEnemy.OnEnemyDeath();
    }

    public void InstantDead()
    {
        OnHit(Weapon.DamageInfo.strong);
    }
}
