﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExplosiveBarrel : BaseDestroyableObject, IHitable
{
    [SerializeField] GameObject explotionPrefab;
    public static Action<int> OnTakeDamage;
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        lives--;

        OnTakeDamage?.Invoke(score);
        if (lives == 0)
        {
            GameObject obj = Instantiate(explotionPrefab,null);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
    public void InstantDead()
    {
        lives = 1;
        OnHit(Weapon.DamageInfo.strong);
    }
}
