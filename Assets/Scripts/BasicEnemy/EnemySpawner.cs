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

    public delegate List<ObstacleInfo> GetOsbstaclesInfoAction();
    public static GetOsbstaclesInfoAction getOsbstaclesInfoAction;
    public delegate List<Transform> GetIntermediatePointAction();
    public static GetIntermediatePointAction getIntermediatePointAction;

    const int normalEnemy = 0;
    const int shieldEnemy = 1;
    const int scaredEnemy = 2;
    public static Action specialSet;

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
                aux = normalEnemy;
                break;
            case int _ when aux >= probNormalSpawn && aux < probNormalSpawn + probShieldSpawn:
                aux = shieldEnemy;
                break;
            case int _ when aux >= probNormalSpawn + probShieldSpawn &&
            aux < probNormalSpawn + probShieldSpawn + probScaredSpawn:
                aux = scaredEnemy;
                break;
        }

        GameObject newEnemy = Instantiate(enemyPrefab[aux]);
        newEnemy.transform.position = new Vector3(transform.position.x, transform.position.y, newEnemy.transform.position.z);
        newEnemy.gameObject.GetComponent<BaseEnemy>().SetObstaclesList(GetObstacles(),GetIntermediatePoints());
        
        enemiesAlive++;

    }

    List<ObstacleInfo> GetObstacles()
    {
        return getOsbstaclesInfoAction?.Invoke();
    }
    List<Transform> GetIntermediatePoints()
    {
        return getIntermediatePointAction?.Invoke();
    }
    
}
