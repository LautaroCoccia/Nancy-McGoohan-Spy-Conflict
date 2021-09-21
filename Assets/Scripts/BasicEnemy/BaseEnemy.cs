using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseEnemy : StateEnemy
{
    [SerializeField] List<Transform> barrelPositions;
    [SerializeField] float timeMaxTime;
    Vector3 nextPos;
    int transformIndex;
    float speed = 6.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
    }
    public void SetObstaclesList(List<Transform> obstacles)
    {
        barrelPositions = obstacles;
        transformIndex = Random.Range(0, barrelPositions.Count);
        nextPos = new Vector3(barrelPositions[transformIndex].position.x,
                              barrelPositions[transformIndex].position.y,
                              transform.position.z);
    }
    protected override IEnumerator Choice()
    {
        choising = true;
        yield return new WaitForSeconds(choisingTime);

        switch(Random.Range(0,101))
        {
            case int n when n >= probToShoot:
                state = State.move;
                break;
            case int n when n < probToShoot:
                state = State.uncover;
                break;
        }
        choising = false;
    }
    
    
    protected override void Move()
    {
        if(transform.position == nextPos)
        {
            Vector3 aux;
            do
            {
                transformIndex = Random.Range(0, barrelPositions.Count);
                aux = new Vector3(barrelPositions[transformIndex].position.x,
                                  barrelPositions[transformIndex].position.y,
                              transform.position.z);
            } while (aux == nextPos);
            nextPos = aux;
            state = State.choice;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    protected override void Uncover()
    {

        state = State.shoot;
    }
    protected override void Shoot()
    {

        state = State.Cover;
    }
    protected override void Cover()
    {

        state = State.choice;
    }
    protected override void SpecialAction()
    {

    }
}
