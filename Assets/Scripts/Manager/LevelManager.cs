using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager :  MonoBehaviour
{
    [SerializeField] string testLoadLevel;
    
    [SerializeField] int health = 3;
    [SerializeField] int maxHealth = 3;
    [SerializeField] float timer = 60;

    [SerializeField] int multiplier = 1;
    [SerializeField] int maxMultiplier = 5;
    [SerializeField] int killCounter;
    [SerializeField] int targetToKill = 10;

    [SerializeField] List<ObstacleInfo> barrelPositions;
    [Space(15)]
    [SerializeField] MusicController musicController;
    
    ScreenShake shaker;

    public static Action<int,int> UpdateUIKillCounter;
    public static Action<float> UpdateUITimer;
    public static Action<int> UpdateUICombo;
    public static Action<int> UpdateUIHealth;
    public static Action LoseCondition;
    public static Action OnWinCondition;

    public static Action<int> OnAddScore;
    public static Action<int> OnResetScore;
    [SerializeField] List<Transform> scapePoints;

    private void Start()
    {
        shaker = Camera.main.GetComponent<ScreenShake>();
        UpdateUIKillCounter?.Invoke(killCounter , targetToKill);
        UpdateUIHealth?.Invoke(health);
        OnResetScore?.Invoke(0);
        Cursor.visible = false;
    }
    // Update is called once per frame
    private void OnEnable()
    {
        ItemHeal.OnHealPlayer+= HealPlayer;
        BaseEnemy.OnHitPlayer += OnHitPlayer;
        SpawnManager.getOsbstaclesInfoAction += GetObstacles;
        DestroyableWallStatesController.DeleteFromObjectList += DeleteObjectFromList;
        ScaredSpecial.scapePointsGetter += GetScapePoints;
        Weapon.ResetMultiplier += UpdateMultiplier;

        OnHitShieldEnemy.OnTakeDamage += AddScore;
        OnHitShieldEnemy.OnKill += OnEnemyKill;
        OnHitScaredEnemy.OnTakeDamage += AddScore;
        OnHitScaredEnemy.OnKill += OnEnemyKill;
        OnHitBasicEnemy.OnTakeDamage += AddScore;
        OnHitBasicEnemy.OnKill += OnEnemyKill;

        DestroyableWall.OnTakeDamage += AddScore;
        ExplosiveBarrel.OnTakeDamage += AddScore;

    }
    private void OnDisable()
    {
        ExplosiveBarrel.OnTakeDamage -= AddScore;
        DestroyableWall.OnTakeDamage -= AddScore;

        OnHitBasicEnemy.OnKill -= OnEnemyKill;
        OnHitBasicEnemy.OnTakeDamage -= AddScore;
        OnHitScaredEnemy.OnKill -= OnEnemyKill;
        OnHitScaredEnemy.OnTakeDamage -= AddScore;
        OnHitShieldEnemy.OnKill -= OnEnemyKill;
        OnHitShieldEnemy.OnTakeDamage -= AddScore;

        Weapon.ResetMultiplier -= UpdateMultiplier;
        ScaredSpecial.scapePointsGetter -= GetScapePoints;
        DestroyableWallStatesController.DeleteFromObjectList -= DeleteObjectFromList;
        SpawnManager.getOsbstaclesInfoAction -= GetObstacles;
        BaseEnemy.OnHitPlayer -= OnHitPlayer;
        ItemHeal.OnHealPlayer -= HealPlayer;
    }
    void Update()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateUITimer?.Invoke(timer);
        }
        else
        {
            Time.timeScale = 0;
            LoseCondition?.Invoke();
            //musicController.StopAllSounds();
           // AkSoundEngine.PostEvent("shoot", gameObject);

        }
    }
    public void AddScore(int addScore)
    {
        //score += addScore * multiplier;
        OnAddScore?.Invoke(addScore * multiplier);
    }
    public void OnEnemyKill()
    {
        killCounter++;
        UpdateUIKillCounter?.Invoke(killCounter , targetToKill);
        UpdateMultiplier(true);
        if(killCounter >= targetToKill)
        {
            OnWinCondition?.Invoke();
            musicController.StopAllSounds();
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }
    public void OnHitPlayer(int damage)
    {
        health -= damage;

        if (health > 0)
        {
            AkSoundEngine.SetSwitch("nancy", "nancy_hurt", gameObject);
            AkSoundEngine.PostEvent("nancy", gameObject);
            Cursor.visible = true;
        }
        else if (health <= 0)
        {
            //Time.timeScale = 0;
            AkSoundEngine.SetSwitch("nancy", "nancy_death", gameObject);
            AkSoundEngine.PostEvent("nancy", gameObject);
            musicController.StopAllSounds();
        }
        UpdateUIHealth?.Invoke(health);
        StartCoroutine(WaitForShakeToLoss());

    }
    IEnumerator WaitForShakeToLoss()
    {
        shaker.Shake();
        yield return new WaitUntil(() => !ScreenShake.isShaking);
        if (health <= 0)
        {
            LoseCondition?.Invoke();
            Time.timeScale = 0;
        }
    }
    public void HealPlayer(int heal)
    {
        health += heal;
        if (maxHealth < health) health = maxHealth;
        UpdateUIHealth?.Invoke(health);
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
        if(hit & killCounter % 2 == 0 && multiplier <= maxMultiplier)
        {
            AkSoundEngine.PostEvent("multiplicate_points", gameObject);
            multiplier++;
        }
        else if(!hit)
        {
            AkSoundEngine.PostEvent("reset_points", gameObject);
            multiplier = 1;
        }
        UpdateUICombo?.Invoke(multiplier);
    }
}