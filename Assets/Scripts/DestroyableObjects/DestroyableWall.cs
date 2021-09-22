using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : BaseDestroyableObject, IHitable
{
    public void OnHit()
    {
        lives--;
        lvlManager.AddScore(score);
        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
