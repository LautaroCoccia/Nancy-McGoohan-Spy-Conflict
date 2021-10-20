using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnHitShieldEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
    [SerializeField] int lives;
    public static Action CanDamage; 
    LevelManager lvlManager = LevelManager.Get();
    SpriteRenderer sr;
    Color col;
    enum States
    {
        shield,
        notShield
    }
    States enemyState;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        col = sr.color;
        sr.color = Color.white; 
        enemyState = States.shield;
    }
    public void OnHit()
    {
        switch (enemyState)
        {
            case States.shield:
                    sr.color = Color.red;
                    lives--;
                    lvlManager.AddScore(25);
                    enemyState = States.notShield;
                break;
            case States.notShield:
                lives--;
                lvlManager.AddKill();
                lvlManager.AddScore(score);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}