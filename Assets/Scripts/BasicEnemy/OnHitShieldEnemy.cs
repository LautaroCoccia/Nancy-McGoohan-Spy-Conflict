using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitShieldEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
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

    // Update is called once per frame
    void Update()
    {

    }

    public int OnHit()
    {
        switch(enemyState)
        {
            case States.shield:
                sr.color = col;
                enemyState = States.notShield;
                return 25;
                break;
            case States.notShield:
                Destroy(gameObject);
                return score;
                break;
            default:
                return score;
            break;
        }
    }
}
