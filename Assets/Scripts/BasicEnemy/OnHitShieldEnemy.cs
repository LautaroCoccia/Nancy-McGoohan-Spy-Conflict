using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnHitShieldEnemy : MonoBehaviour, IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] int score;
    [SerializeField] int lives;
    public static Action CanDamage;
    public static Action<int> OnTakeDamage;
    public static Action OnKill;
    SpriteRenderer sr;
    Color col;
    
    bool enemyState = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        col = sr.color;
        sr.color = Color.blue; 
    }
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        if(damageInfo == Weapon.DamageInfo.strong && !enemyState)
        {
            sr.color = Color.red;
            OnTakeDamage?.Invoke(25);
            enemyState = true;
        }
        else
        {
            baseEnemy.InstanciateBlood();
            OnTakeDamage?.Invoke(score);
            OnKill();
            Destroy(gameObject);
        }
    }
}