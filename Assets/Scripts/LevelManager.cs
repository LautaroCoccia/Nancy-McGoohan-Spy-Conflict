using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int killCounter;
    [SerializeField] float timer;
    public static Action<int> UpdateUIScore;
    public static Action<int> UpdateUIKillCounter;
    public static Action<float> UpdateUITimer;

    private static LevelManager instanceLevelManager;
    public static LevelManager Get()
    {
        return instanceLevelManager;
    }
    private void Awake()
    {
        if(instanceLevelManager == null)
        {
            instanceLevelManager = this;
        }
        else if(instanceLevelManager != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(timer >0)
        {
            timer -= 1* Time.deltaTime;
            UpdateUITimer?.Invoke(timer);
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        UpdateUIScore?.Invoke(score);
    }
    public void AddKill()
    {
        killCounter++;
        UpdateUIKillCounter?.Invoke(killCounter);
    }
}