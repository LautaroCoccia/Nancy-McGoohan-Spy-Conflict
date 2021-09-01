using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyFSM : MonoBehaviour, IHitable
{
    [SerializeField] List<Transform> barrelPositions;
    [SerializeField] float timeMaxTime;
    [SerializeField] int score;
    float time;
    Vector3 nextPos;
    Vector3 startPos;
    int transformIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(time < 1 && startPos != nextPos)
        {
            time += Time.deltaTime / timeMaxTime;
        }
        else
        {
            startPos = nextPos;
            Vector3 aux;
            do
            {
                transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
                aux = new Vector3(barrelPositions[transformIndex].position.x,
                                  barrelPositions[transformIndex].position.y,
                              transform.position.z);
            } while (aux == nextPos);
            nextPos = aux;
            time = 0;
        }
        transform.position = Vector3.Lerp(startPos, new Vector3(nextPos.x, nextPos.y, transform.position.z), time);
    }
    public void SetObstaclesList(List<Transform> obstacles)
    {
        barrelPositions = obstacles;
        transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
        startPos = transform.position;
        nextPos = new Vector3(barrelPositions[transformIndex].position.x,
                              barrelPositions[transformIndex].position.y,
                              transform.position.z);
    }

    public int OnHit()
    {
        Destroy(gameObject);
        return score;
    }
}
