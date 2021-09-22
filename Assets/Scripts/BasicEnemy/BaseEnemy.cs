using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseEnemy : StateEnemy
{
    [SerializeField] float timeToWaitAndShoot;
    [SerializeField] float timeWaitEndShoot;
    [SerializeField] float timeWaitAndCover;
    [SerializeField] SpriteRenderer flashInWeapon;
    [SerializeField] float timeMaxTime;
    [SerializeField] List<ObstacleInfo> barrelPositions;
    Vector3 nextPos;
    Vector3 actualCover;
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
    protected override IEnumerator Choice()
    {
        choising = true;
        yield return new WaitForSeconds(choisingTime);

        switch(UnityEngine.Random.Range(0,101))
        {
            case int n when n >= probToShoot:
                state = State.move;
                SelectCoverPosition(barrelPositions);
                break;
            case int n when n < probToShoot:
                state = State.uncover;
                SelectUncoverPosition(barrelPositions[transformIndex].shootPosition);
                break;
        }
        choising = false;
    }
    public void SetObstaclesList(List<ObstacleInfo> obstacles)
    {
        barrelPositions = obstacles;
        transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
        nextPos = new Vector3(barrelPositions[transformIndex].coverPosition.position.x,
                              barrelPositions[transformIndex].coverPosition.position.y,
                              transform.position.z);
        actualCover = nextPos;
    }


    
    protected override void Move()
    {
         transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        if (transform.position == nextPos && state != State.uncover) state = State.choice;

    }
    protected override void Uncover()
    {
        Move();
        if(transform.position == nextPos) state = State.shoot;
    }
    protected override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeToWaitAndShoot);
        flashInWeapon.enabled = true;
        yield return new WaitForSeconds(timeWaitEndShoot);
        
        yield return new WaitForSeconds(timeWaitAndCover);
        state = State.Cover;

    }
    protected override void Cover()
    {
        SelectActualCoverPosition();
        Move();
    }
    protected override void SpecialAction()
    {

    }
    protected void SelectCoverPosition(List<ObstacleInfo> positions)
    {
        Vector3 aux;
        do
        {
            transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
            aux = new Vector3(barrelPositions[transformIndex].coverPosition.position.x,
                              barrelPositions[transformIndex].coverPosition.position.y,
                          transform.position.z);
        } while (aux == nextPos);
        nextPos = aux;
        actualCover = aux;
    }
    protected void SelectUncoverPosition(List<Transform> position)
    {
        Vector3 aux;
        int index = 0;
        do
        {
            index = UnityEngine.Random.Range(0, position.Count);
            aux = new Vector3(position[index].position.x,
                              position[index].position.y,
                          transform.position.z);
        } while (aux == nextPos);
        nextPos = aux;
    }
    protected void SelectActualCoverPosition()
    {
        nextPos = actualCover;
    }
}
