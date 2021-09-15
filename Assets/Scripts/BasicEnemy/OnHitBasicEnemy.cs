using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBasicEnemy : MonoBehaviour, IHitable
{
    [SerializeField] int score;
    LevelManager lvlManager = LevelManager.Get();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        lvlManager.AddScore(score);
        lvlManager.AddKill();
        Destroy(gameObject);
    }

}
