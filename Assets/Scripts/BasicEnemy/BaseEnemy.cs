using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseEnemy : StateEnemy
{
    [SerializeField] GameObject bloodParticleSystem;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer flashInWeapon;
    [SerializeField] float timeToWaitAndShoot;
    [SerializeField] float timeWaitEndShoot;
    [SerializeField] float timeWaitAndCover;
    [SerializeField] float timeMaxTime;
    [SerializeField] int damage;
    [SerializeField] [Range(0, 100)] protected int probToHit = 0;
    [SerializeField] List<ObstacleInfo> barrelPositions;
    const float positionCorrectionForSorting = 0.2f;

    Vector3 nextPos;
    Vector3 actualCover;
    int transformIndex;
    public const float speed = 4.7f;
    public static Action<int> OnHitPlayer;

    [Space(15)]
    [SerializeField] BasicSpecial specialSkill;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator.SetBool("IsMoving", true);
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override IEnumerator Choice()
    {
        animator.SetBool("IsMoving", false);
        choising = true;
        yield return new WaitForSeconds(choisingTime);
        if(switchTimerVsProbSpecial)
        {
            timerToSpecial += Time.deltaTime;
            if (timerToSpecial >= timerToWaitSpecial) state = State.specialAction;
        }
        switch(UnityEngine.Random.Range(0,101))
        {
            case int n when n >= (probToShoot + probToSpecial):
                state = State.move;
                SelectCoverPosition(barrelPositions);
                animator.SetBool("IsMoving", true);
                break;
            case int n when n < probToShoot:
                state = State.uncover;
                SelectUncoverPosition(barrelPositions[transformIndex].shootPosition);
                animator.SetBool("IsMoving", true);
                break;
            case int n when n < (probToShoot + probToSpecial) && n> probToShoot
            && !switchTimerVsProbSpecial && probToSpecial!=0:
                state = State.specialAction;
                animator.SetBool("IsMoving", true);
                break;
        }
        choising = false;
    }
    public void SetObstaclesList(List<ObstacleInfo> obstacles)
    {
        barrelPositions = obstacles;
        transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
        nextPos = new Vector3(barrelPositions[transformIndex].coverPosition.position.x,
                              barrelPositions[transformIndex].coverPosition.position.y + positionCorrectionForSorting,
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
        animator.SetBool("IsMoving", false);
        yield return new WaitForSeconds(timeToWaitAndShoot);
        flashInWeapon.enabled = true;
        yield return new WaitForSeconds(timeWaitEndShoot);
        
        if (UnityEngine.Random.Range(0, 101) < probToHit)
        {
                OnHitPlayer?.Invoke(damage);
        }
        flashInWeapon.enabled = false;
        yield return new WaitForSeconds(timeWaitAndCover);
        shooting = false;
        state = State.Cover;
        animator.SetBool("IsMoving", true);


    }
    protected override void Cover()
    {
        SelectActualCoverPosition();
        Move();
    }
    protected override void SpecialAction()
    {
        if (specialSkill != null)
        {
            if(specialSkill.Skill())
            {
                state = State.choice;
            }
        }
        else
        {
            state = State.choice;
        }
    }
    protected void SelectCoverPosition(List<ObstacleInfo> positions)
    {
        Vector3 aux;
        do
        {
            transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
            if(barrelPositions[transformIndex] == null)
            {
                barrelPositions.RemoveAt(transformIndex);
                transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
            }
            aux = new Vector3(barrelPositions[transformIndex].coverPosition.position.x,
                              barrelPositions[transformIndex].coverPosition.position.y + positionCorrectionForSorting,
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
            if(position[index] == null)
            {
                aux = Vector3.zero;
                state = State.move;
            }
            else
            {
                aux = new Vector3(position[index].position.x,
                              position[index].position.y + positionCorrectionForSorting,
                          transform.position.z);
            }
            
        } while (aux == nextPos);
        nextPos = aux;
    }
    protected void SelectActualCoverPosition()
    {
        nextPos = actualCover;
    }

    public void InstanciateBlood()
    {
        Instantiate(bloodParticleSystem, transform.position, Quaternion.identity);
    }
}
