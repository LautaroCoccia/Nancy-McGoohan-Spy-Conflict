using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool canSpawnUp;
    [SerializeField] bool canSpawnLeft;
    [SerializeField] bool canSpawnRight;
    [SerializeField] float timeToSpawn;
    [SerializeField] float actualTimeToSpawn;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Vector3 offset;
    enum DirectionToSpawn 
    {
        up,
        left,
        right
    }
    DirectionToSpawn directionToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        actualTimeToSpawn = timeToSpawn;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        actualTimeToSpawn += Time.deltaTime;
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        if (actualTimeToSpawn >= timeToSpawn)
        {
            actualTimeToSpawn = 0;
            do
            {
                directionToSpawn = (DirectionToSpawn)Random.Range((int)DirectionToSpawn.up, (int)DirectionToSpawn.right + 1);
            } while (CanSpawnHere());

            GameObject newEnemy = Instantiate(enemyPrefab);
            switch(directionToSpawn)
            {
                case DirectionToSpawn.up:
                    newEnemy.transform.position = new Vector3(transform.position.x, transform.position.y + offset.y, transform.position.z + offset.z);
                    break;                                                                                                                
                case DirectionToSpawn.left:                                                                                               
                    newEnemy.transform.position = new Vector3(transform.position.x - offset.x, transform.position.y, transform.position.z + offset.z);
                    break;                                                                                                                
                case DirectionToSpawn.right:                                                                                              
                    newEnemy.transform.position = new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z + offset.z);
                    break;
            }
        }
    }
    bool CanSpawnHere()
    {
        return ((canSpawnUp && directionToSpawn == DirectionToSpawn.up) || 
            (canSpawnLeft && directionToSpawn == DirectionToSpawn.left) || 
            (canSpawnRight && directionToSpawn == DirectionToSpawn.right));
    }
}
