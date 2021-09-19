using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int health;
    [SerializeField] int killCounter;
    [SerializeField] float timer;
    ScreenShake shaker;
    public static Action<int> UpdateUIScore;
    public static Action<int> UpdateUIKillCounter;
    public static Action<float> UpdateUITimer;
    public static Action<int> UpdateUIHealth;
    public static Action LoseCondition;
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
    private void Start()
    {
        shaker = Camera.main.GetComponent<ScreenShake>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            UpdateUITimer?.Invoke(timer);
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnHitPlayer(10);
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
    public void OnHitPlayer(int damage)
    {
        health -= damage;
        UpdateUIHealth?.Invoke(health);
        shaker.Shake();
        if (health <= 0)
        {
            LoseCondition?.Invoke();
            Time.timeScale = 0;
        }
    }
}