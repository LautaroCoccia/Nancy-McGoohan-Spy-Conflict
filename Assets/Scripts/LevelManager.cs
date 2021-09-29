using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] int score;
    int maxHealth;
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
        maxHealth = health;
        if (instanceLevelManager == null)
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
    private void OnEnable()
    {
        ItemHeal.OnHealPlayer+= HealPlayer;
        BaseEnemy.OnHitPlayer += OnHitPlayer;
    }
    private void OnDisable()
    {
        ItemHeal.OnHealPlayer -= HealPlayer;
        BaseEnemy.OnHitPlayer -= OnHitPlayer;
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            UpdateUITimer?.Invoke(timer);
            LoseCondition?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Gameplay");
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
        }
    }
    public void HealPlayer(int heal)
    {
        health += heal;
        if (maxHealth < health) health = maxHealth;
        UpdateUIHealth?.Invoke(health);
    }
}