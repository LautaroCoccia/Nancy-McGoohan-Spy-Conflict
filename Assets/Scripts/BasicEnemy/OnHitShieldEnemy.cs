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
    }
    public void OnHit(Weapon.DamageInfo damageInfo)
    {
        if(damageInfo == Weapon.DamageInfo.strong && !enemyState)
        {
            AkSoundEngine.SetSwitch("shield", "shieldhit_shotgun", gameObject);
            AkSoundEngine.PostEvent("shield", gameObject);
            sr.color = Color.red;
            OnTakeDamage?.Invoke(25);
            enemyState = true;
        }
        else if(damageInfo != Weapon.DamageInfo.strong && !enemyState)
        {
            AkSoundEngine.SetSwitch("shield", "shieldhit_wrong", gameObject);
            AkSoundEngine.PostEvent("shield", gameObject);
        }
        else if(enemyState)
        {
            baseEnemy.DeathScream();
            baseEnemy.InstanciateBlood();
            OnTakeDamage?.Invoke(score);
            OnKill();
            baseEnemy.OnEnemyDeath();
        }
    }

    public void InstantDead()
    {
        enemyState = true;
        OnHit(Weapon.DamageInfo.strong);
    }
}