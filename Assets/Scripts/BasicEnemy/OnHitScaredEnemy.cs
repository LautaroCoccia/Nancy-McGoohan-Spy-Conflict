using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDrop))]
public class OnHitScaredEnemy : TypeOfDamage, IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    ItemDrop itemDrop;
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        baseEnemy.InstanciateBlood();
        itemDrop.Drop();
        lvlManager.AddKill();
        lvlManager.AddScore(score);
        Destroy(gameObject);
    }
    
}
