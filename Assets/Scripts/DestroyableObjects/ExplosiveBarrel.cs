using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IHitable
{
    [SerializeField] GameObject explotionPrefab;
    int lives = 3;
    public int OnHit()
    {
        lives--;
        if (lives == 0)
        {
            GameObject obj = Instantiate(explotionPrefab);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
        return 50;
    }
}
