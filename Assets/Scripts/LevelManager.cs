using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    int maxHealth;
    [SerializeField] string testLoadLevel;
    [SerializeField] int score;
    [SerializeField] int multiplier = 1;
    [SerializeField] int maxMultiplier = 5;
    [SerializeField] int health;
    [SerializeField] int killCounter;
    [SerializeField] float timer;
    [SerializeField] List<ObstacleInfo> barrelPositions;
    
    ScreenShake shaker;

    public static Action<int> UpdateUIScore;
    public static Action<int> UpdateUIKillCounter;
    public static Action<float> UpdateUITimer;
    public static Action<int,int> UpdateUIHealth;
    public static Action LoseCondition;

    private static LevelManager instanceLevelManager;

    [SerializeField] List<Transform> scapePoints;
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
        EnemySpawner.getOsbstaclesInfoAction += GetObstacles;
        DestroyableWallStatesController.DeleteFromObjectList += DeleteObjectFromList;
        ScaredSpecial.scapePointsGetter += GetScapePoints;
        Weapon.ResetMultiplier += UpdateMultiplier;
    }
    private void OnDisable()
    {
        ItemHeal.OnHealPlayer -= HealPlayer;
        BaseEnemy.OnHitPlayer -= OnHitPlayer;
        EnemySpawner.getOsbstaclesInfoAction -= GetObstacles;
        DestroyableWallStatesController.DeleteFromObjectList -= DeleteObjectFromList;
        ScaredSpecial.scapePointsGetter -= GetScapePoints;
        Weapon.ResetMultiplier -= UpdateMultiplier;
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
            SceneManager.LoadScene(testLoadLevel);
        }
    }
    public void AddScore(int addScore)
    {
        score += addScore * multiplier;
        UpdateUIScore?.Invoke(score);
    }
    public void AddKill()
    {
        killCounter++;
        UpdateUIKillCounter?.Invoke(killCounter);
        UpdateMultiplier(true);
    }
    public void OnHitPlayer(int damage)
    {
        health -= damage;
        UpdateUIHealth?.Invoke(health,maxHealth);
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
        UpdateUIHealth?.Invoke(health,maxHealth);
    }
    void DeleteObjectFromList(Transform transform)
    {
        for (int i = 0; i < barrelPositions.Count; i++)
        {
            if (barrelPositions[i].transform == transform)
            {
                barrelPositions.RemoveAt(i);
            }    
        }
    }
    List<ObstacleInfo> GetObstacles()
    {
        return barrelPositions;
    }
    List<Transform> GetScapePoints()
    {
        return scapePoints;
    }
    void UpdateMultiplier(bool hit)
    {
        if(hit & killCounter % 2 == 0 )
        {
            multiplier++;
        }
        else if(!hit)
        {
            multiplier = 1;
        }
    }
}