using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int probNormalSpawn; //prob+prob+prob = 100%
    [SerializeField] int probShieldSpawn; // no necesitan sumar 100 en conjunto.
    [SerializeField] int probScaredSpawn;

    [SerializeField] static int enemiesAlive;
    [SerializeField] int maxEnemiesAlive;
    [SerializeField] float minTimeToSpawn;
    [SerializeField] float maxTimeToSpawn;
    [SerializeField] float actualTime;
    [SerializeField] float timeToSpawn;
    [SerializeField] List<GameObject> enemyPrefab;

    //public static Action<List<ObstacleInfo>> GetOsbstaclesInfoAction;
    public delegate List<ObstacleInfo> GetOsbstaclesInfoAction();
    public static GetOsbstaclesInfoAction getOsbstaclesInfoAction;

    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        enemiesAlive = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive < maxEnemiesAlive)
        {
            actualTime += Time.deltaTime;
        }
        if(actualTime >= timeToSpawn && enemiesAlive < maxEnemiesAlive)
        {
            actualTime = 0;
            timeToSpawn = UnityEngine.Random.Range(minTimeToSpawn, maxTimeToSpawn);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        int aux = UnityEngine.Random.Range(0, probNormalSpawn + probShieldSpawn + probScaredSpawn);
        switch (aux)
        {
            case int _ when aux < probNormalSpawn:
                aux = 0;
                break;
            case int _ when aux >= probNormalSpawn && aux < probNormalSpawn + probShieldSpawn:
                aux = 1;
                break;
            case int _ when aux >= probNormalSpawn + probShieldSpawn &&
            aux < probNormalSpawn + probShieldSpawn + probScaredSpawn:
                aux = 2;
                break;
        }

        GameObject newEnemy = Instantiate(enemyPrefab[aux]);
        newEnemy.transform.position = new Vector3(transform.position.x, transform.position.y, newEnemy.transform.position.z);
        newEnemy.gameObject.GetComponent<BaseEnemy>().SetObstaclesList(GetObstacles());
        enemiesAlive++;
    }

    private void OnDisable()
    {
        
    }

    List<ObstacleInfo> GetObstacles()
    {
        //return GetOsbstaclesInfoAction?.Invoke();
        return getOsbstaclesInfoAction();
    }
}
