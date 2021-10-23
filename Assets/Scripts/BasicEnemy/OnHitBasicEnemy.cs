using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBasicEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    public void OnHit(int typeOfDamage)
    {
        lvlManager.AddKill();
        lvlManager.AddScore(score);
        Destroy(gameObject);
    }

}
