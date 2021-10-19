using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyableWall : BaseDestroyableObject, IHitable
{
    [SerializeField] GameObject nextState;
    public static Action setNewWallState;
    public void OnHit()
    {
        lives--;
        lvlManager.AddScore(score);

        if (lives == 0)
        {
            setNewWallState?.Invoke();
            //Destroy(gameObject);
        }
    }
}
