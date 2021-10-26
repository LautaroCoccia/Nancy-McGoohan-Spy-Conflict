using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBasicEnemy : MonoBehaviour, IHitable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    public void OnHit(int typeOfDamage)
    {
        baseEnemy.InstanciateBlood();
        lvlManager.AddKill();
        lvlManager.AddScore(score);
        Destroy(gameObject);
    }

}
