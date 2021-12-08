using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
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
    [SerializeField] List<ObstacleInfo> coverPosition;
    ObstacleInfo actualCover;
    public float speed = 4.7f;
    public static Action<int> OnHitPlayer;

    [Space(15)]
    [SerializeField] BasicSpecial specialSkill;

    public NavMeshAgent agent;
    public float maxDistanceToSelectNewCover = 12.0f;
    public float coverPositionZoffset = 0.9f;
    int uncoverPosIndex = 0;
    bool selectShortDistanceCover = false;
    private void OnEnable()
    {
        if(specialSkill)
        specialSkill.OnSkillEnd += SetDestinationAndNextState;

    }
    private void OnDisable()
    {

        if (specialSkill)
            specialSkill.OnSkillEnd -= SetDestinationAndNextState;
    }
    protected override void Start()
    {
        actualCover = coverPosition[UnityEngine.Random.Range(0,coverPosition.Count)];
        agent.updateRotation = false;
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        //LostCover();
    }
    protected override IEnumerator Choice()
    {
        animator.SetTrigger("Idle");

        choising = true;
        yield return new WaitForSeconds(choisingTime);
        if(switchTimerVsProbSpecial)
        {
            timerToSpecial += Time.deltaTime;
            if (timerToSpecial >= timerToWaitSpecial) state = State.specialAction;
        }
        switch (UnityEngine.Random.Range(0, 101))
        {
            case int n when n >= (probToShoot + probToSpecial):
                SelectNewCoverPosition();
                break;
            case int n when n < probToShoot:
                state = State.uncover;
                SelectUncoverPosition();
                break;
            case int n when n < (probToShoot + probToSpecial) && n> probToShoot
            && !switchTimerVsProbSpecial && probToSpecial!=0:
                state = State.specialAction;
                break;
        }
        choising = false;
    }
    void LostCover()
    {
        //if (!actualCoverTransform)
        //{
        //    StopCoroutine(Choice());
        //    choising = false;
        //    SelectCoverAndMove();
        //}
    }
    public void SetObstaclesList(List<ObstacleInfo> obstacles)
    {
        coverPosition = obstacles;
    }
    protected override void GoToNextCoverPosition()
    {
        agent.SetDestination(actualCover.transform.position + Vector3.forward * coverPositionZoffset);
        stateToAfterMove = State.choice;
        state = State.move;
    }
    protected override void WaitToStopMove()
    {
        setMoveAnimationDirection();
        if (agent.remainingDistance == 0)
        {
            state = stateToAfterMove;  
        }
    }
    protected override void Uncover()
    {
        agent.SetDestination(actualCover.shootPosition[uncoverPosIndex].position);
        setMoveAnimationDirection();
        stateToAfterMove = State.shoot;
        state = State.move;
    }
    protected override IEnumerator Shoot()
    {
        animator.SetTrigger("Idle");
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(timeToWaitAndShoot);
        flashInWeapon.enabled = true;
        yield return new WaitForSeconds(timeWaitEndShoot);
        ShootSound();
        if (UnityEngine.Random.Range(0, 101) < probToHit)
        {
            OnHitPlayer?.Invoke(damage);
        }
        flashInWeapon.enabled = false;
        yield return new WaitForSeconds(timeWaitAndCover);
        
        shooting = false;
        animator.SetTrigger("Idle");
        actualCover.slotOccuped[uncoverPosIndex] = false;
        state = State.startMove;
    }
    protected override void Death()
    {
        agent.isStopped = true;
        animator.SetTrigger("Idle");
        animator.SetTrigger("Death");
        StartCoroutine(DyingEnemy());
    }
    protected override void SpecialAction()
    {
        if (specialSkill)
        {
            specialSkill.Skill();
            if(state == State.specialAction)
            {
                state = State.choice;
            }
        }
        else
        {
            state = State.choice;
        }
    }
    protected void SelectNewCoverPosition()
    {
        if (selectShortDistanceCover)
        {
            List<ObstacleInfo> auxList = new List<ObstacleInfo>();
            foreach (ObstacleInfo obj in coverPosition)
            {
                if (selectShortDistanceCover && maxDistanceToSelectNewCover > Mathf.Abs(transform.position.x - obj.transform.position.x) && obj != actualCover)
                {
                    auxList.Add(obj);
                }
            }
            if (auxList.Count != 0)
                actualCover = auxList[UnityEngine.Random.Range(0, auxList.Count)];
        }
        else
        {
            actualCover = coverPosition[UnityEngine.Random.Range(0,coverPosition.Count)];
        }
        state = State.startMove;

    }
    protected void SelectUncoverPosition()
    {
        if (actualCover.shootPosition.Count > 0)
        {
            List<int> auxlist = new List<int>();
            for(int i = 0; i < actualCover.slotOccuped.Count;i++)
            {
                
                if (!actualCover.slotOccuped[i])
                {
                    auxlist.Add(i);
                }
            }
            if (auxlist.Count > 0)
            {
                uncoverPosIndex = auxlist[UnityEngine.Random.Range(0, auxlist.Count)];
                actualCover.slotOccuped[uncoverPosIndex] = true;
                state = State.uncover;
            }
            else
            {
                state = State.choice;
            }
        }
        else
        {
            state = State.choice;
        }
    }
    void ShootSound()
    {
        AkSoundEngine.PostEvent("enemy_shoot", gameObject);
    }
    public void DeathScream()
    {
        AkSoundEngine.PostEvent("enemy_death", gameObject);
    }
    public void InstanciateBlood()
    {
        Instantiate(bloodParticleSystem, transform.position, Quaternion.identity);
    }
    void setMoveAnimationDirection()
    {
        if(agent.velocity.x < 0.0f)
        {
            animator.SetBool("RightMove", false);
            animator.SetBool("LeftMove",true);
        }
        else if (agent.velocity.x > 0.0f)
        {
            animator.SetBool("LeftMove", false);
            animator.SetBool("RightMove",true);
        }
        else if(agent.remainingDistance == 0)
        {

            animator.SetBool("LeftMove", false);
            animator.SetBool("RightMove", false);
        }
    }
    public void OnEnemyDeath()
    {
        state = State.death;
    }
    IEnumerator DyingEnemy()
    {
        Collider2D coll = gameObject.GetComponent<Collider2D>();
        coll.enabled = false;
        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void SetDestination(Vector3 des)
    {
        agent.SetDestination(des  + Vector3.forward * coverPositionZoffset);
    }
    public void SetDestinationAndNextState(Vector3 des,State next)
    {
        SetDestination(des);
        stateToAfterMove = next;
    }
}
