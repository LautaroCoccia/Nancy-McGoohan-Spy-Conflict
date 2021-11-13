using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] int score;

    public static Action<int> UpdateUIScore;

    void OnEnable()
    {
        LevelManager.OnAddScore += UpdateScore;
        LevelManager.OnResetScore += ResetValues;
    }
    void OnDisable()
    {
        LevelManager.OnResetScore -= ResetValues;
        LevelManager.OnAddScore -= UpdateScore;
    }
    void ResetValues(int zeroScore)
    {
        score = zeroScore;
    }

    void UpdateScore(int addScore)
    {
        score += addScore;
        UpdateUIScore?.Invoke(score);
    }
}
