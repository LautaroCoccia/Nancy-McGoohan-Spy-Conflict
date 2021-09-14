using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IHitable
{
    [SerializeField] GameObject explotionPrefab;
    LevelManager lvlManager;
    int lives = 3;
    private void Start()
    {
        lvlManager = LevelManager.Get();
    }
    public void OnHit()
    {
        lives--;
        lvlManager.AddScore(25);
        if (lives == 0)
        {
            GameObject obj = Instantiate(explotionPrefab);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
