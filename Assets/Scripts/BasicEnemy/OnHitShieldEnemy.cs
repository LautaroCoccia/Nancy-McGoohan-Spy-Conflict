using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnHitShieldEnemy : TypeOfDamage, IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] int score;
    [SerializeField] int lives;
    public static Action CanDamage; 
    LevelManager lvlManager = LevelManager.Get();
    SpriteRenderer sr;
    Color col;
    
    bool enemyState = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        col = sr.color;
        sr.color = Color.white; 
    }
    public void OnHit(int typeOfDamage)
    {
        if(typeOfDamage == (int)DamageType.strong && !enemyState)
        {
            sr.color = Color.red;
            lvlManager.AddScore(25);
            enemyState = true;
        }
        else
        {
            baseEnemy.InstanciateBlood();
            lvlManager.AddKill();
            lvlManager.AddScore(score);
            Destroy(gameObject);
        }
    }
}