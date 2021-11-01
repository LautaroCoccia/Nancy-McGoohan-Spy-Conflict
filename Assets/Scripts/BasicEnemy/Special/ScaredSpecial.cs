using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScaredSpecial : BasicSpecial
{
    List<Transform> scapePoint;
    bool inProgress = false;
    public delegate List<Transform> GetScapePoints();
    public static GetScapePoints scapePointsGetter;
    Vector3 aux;
    float speed;
    private void Awake()
    {
        SetScapePoints(scapePointsGetter?.Invoke());
        speed = GetComponent<BaseEnemy>().speed;
    }
    public override bool Skill()
    {
        if (!inProgress)
        {
            aux = scapePoint[0].position;
            for(short i = 1; i < scapePoint.Count;i++)
            {
                if(Vector3.Distance(transform.position,aux) > 
                    Vector3.Distance(transform.position, scapePoint[i].position))
                {
                    aux = scapePoint[i].position;
                }
            }
            inProgress = true;
            StartCoroutine(MoveToFinalPoint());
        }
        return inProgress;
    }
    public void SetScapePoints(List<Transform> newPoints)
    {
        scapePoint = newPoints;
    }
    IEnumerator MoveToFinalPoint()
    {
        while (inProgress)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, aux, speed * Time.deltaTime);
            if (transform.position == aux) inProgress = false;
        }
        Destroy(transform.gameObject);
    }
    
}
