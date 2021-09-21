using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseEnemy : StateEnemy
{
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
    public void SetObstaclesList(List<ObstacleInfo> obstacles)
    {
        barrelPositions = obstacles;
        transformIndex = Random.Range(0, barrelPositions.Count);
        nextPos = new Vector3(barrelPositions[transformIndex].coverPosition.position.x,
                              barrelPositions[transformIndex].coverPosition.position.y,
                              transform.position.z);
        actualCover = nextPos;
    }
    protected override void Move()
    {
        if (transform.position == nextPos)
        {
            switch(state)
            {
                case State.move:
                    SelectCoverPosition(barrelPositions);
                    state = State.choice;
                    break;

                case State.uncover:
                    SelectUncoverPosition(barrelPositions[transformIndex].shootPosition);
                    state = State.shoot;
                    break;
                case State.Cover:
                    SelectActualCoverPosition();
                    state = State.choice;
                    break;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    protected override void Uncover()
    {
        Move();
    }
    protected override void Shoot()
    {
        




        state = State.Cover;
    }
    protected override void Cover()
    {
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
            transformIndex = Random.Range(0, barrelPositions.Count);
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
            index = Random.Range(0, position.Count);
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
