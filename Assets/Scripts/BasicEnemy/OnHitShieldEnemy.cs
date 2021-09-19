using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitShieldEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
    [SerializeField] int lives;
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
        lives = 2;
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
                sr.color = col;
                lives--;
                lvlManager.AddScore(25);
                enemyState = States.notShield;
                break;
            case States.notShield:
                lives--;
                lvlManager.AddScore(score);
                lvlManager.AddKill();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
