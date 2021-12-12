using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnManager : MonoBehaviour
{
    [Serializable]
    public struct probEnemy
    {
        public string name;
        public float prob;
    }
    [SerializeField] probEnemy[] probToSpawn; //prob+prob+prob = 100%
    [Space(10)]

    [SerializeField] int enemiesAlive;
    [SerializeField] int maxEnemiesAlive;
    [SerializeField] float minTimeToSpawn;
    [SerializeField] float maxTimeToSpawn;
    [SerializeField] float actualTime;
    [SerializeField] float timeToSpawn;
    [SerializeField] Transform[] spawner;
    [SerializeField] List<GameObject> enemyPrefab;

    public delegate List<ObstacleInfo> GetOsbstaclesInfoAction();
    public static GetOsbstaclesInfoAction getOsbstaclesInfoAction;

    public static Action specialSet;
    public List<GameObject> enemiesSpawned;
    float probabilityChoosed = 0.0f;
    float cumulativeProbability = 0.0f;
    float maxProb = 0;
    void Start()
    {
        enemiesAlive = 0;
        maxProb = 0;
        for (int i = 0; i < probToSpawn.Length; i++)
        {
            maxProb += probToSpawn[i].prob;
        }
    }
    void Update()
    {
        if (enemiesAlive < maxEnemiesAlive)
        {
            actualTime += Time.deltaTime;
        }
        if (actualTime >= timeToSpawn && enemiesAlive < maxEnemiesAlive)
        {
            actualTime = 0; 
            timeToSpawn = UnityEngine.Random.Range(minTimeToSpawn, maxTimeToSpawn);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {   
        probabilityChoosed = UnityEngine.Random.Range(0, maxProb);
        cumulativeProbability = 0;
        for (int i = 0; i < probToSpawn.Length;i++)
        {
            cumulativeProbability += probToSpawn[i].prob;
            if (probabilityChoosed < cumulativeProbability)
            {
                Spawn(i);
                break;
            }
        }
    }
    void Spawn(int index)
    {
        GameObject newEnemy = Instantiate(enemyPrefab[index]);
        newEnemy.transform.position = spawner[UnityEngine.Random.Range(0, spawner.Length)].position;
        newEnemy.gameObject.GetComponent<BaseEnemy>().SetObstaclesList(GetObstacles());
        enemiesSpawned.Add(newEnemy);
        enemiesAlive++;
    }
    List<ObstacleInfo> GetObstacles()
    {
        return getOsbstaclesInfoAction?.Invoke();
    }
}
