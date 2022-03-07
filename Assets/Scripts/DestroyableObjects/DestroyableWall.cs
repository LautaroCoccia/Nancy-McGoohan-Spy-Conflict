using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyableWall : BaseDestroyableObject, IHitable
{
    [SerializeField] GameObject nextState;
    public static Action<GameObject> setNewWallState;
    public static Action<int> OnTakeDamage;
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        lives--;
        OnTakeDamage?.Invoke(score);

        if (lives == 0)
        {
            setNewWallState?.Invoke(gameObject);
            //Destroy(gameObject);
        }
    }
    public void InstantDead()
    {
        OnHit(Weapon.DamageInfo.strong);
    }
}
