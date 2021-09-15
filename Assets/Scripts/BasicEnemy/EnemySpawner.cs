using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> barrelPositions; //TODO informacion del nivel, LevelManager
    [SerializeField] static int enemiesAlive;
    [SerializeField] int maxEnemiesAlive;
    [SerializeField] float minTimeToSpawn;
    [SerializeField] float maxTimeToSpawn;
    [SerializeField] float actualTime;
    [SerializeField] float timeToSpawn;
    [SerializeField] List<GameObject> enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive < maxEnemiesAlive)
        {
            actualTime += Time.deltaTime;
        }
        if(actualTime >= timeToSpawn)
        {
            actualTime = 0;
            timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)]);
        newEnemy.transform.position = new Vector3(transform.position.x, transform.position.y, newEnemy.transform.position.z);
        newEnemy.gameObject.GetComponent<EnemyFSM>().SetObstaclesList(barrelPositions);
        enemiesAlive++;
    }
}
