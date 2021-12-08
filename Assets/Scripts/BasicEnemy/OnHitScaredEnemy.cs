using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(ItemDrop))]
public class OnHitScaredEnemy : MonoBehaviour , IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    ItemDrop itemDrop;
    [SerializeField] int score;
    public static Action<int> OnTakeDamage;
    public static Action OnKill;
    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        baseEnemy.DeathScream();
        baseEnemy.InstanciateBlood();
        itemDrop.Drop();
        OnTakeDamage?.Invoke(score);
        OnKill();
        baseEnemy.OnEnemyDeath();
    }
    
}
