using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBasicEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    public void OnHit()
    {
        lvlManager.AddScore(score);
        lvlManager.AddKill();
        Destroy(gameObject);
    }

}
