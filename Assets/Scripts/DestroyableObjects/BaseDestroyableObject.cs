using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDestroyableObject : MonoBehaviour
{
    public LevelManager lvlManager;
    public int lives = 3;
    public int score = 25;
    private void Start()
    {
        lvlManager = LevelManager.Get();
    }
}
