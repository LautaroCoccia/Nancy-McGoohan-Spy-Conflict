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
    [SerializeField] List<Transform> intermediateSpots;
    const float positionCorrectionForSorting = 0.7f;
    Transform actualCoverTransform;
    Vector3 intermediatePosition;
    bool intermediateMove;
    Vector3 nextPos;
    Vector3 actualCover;
    int transformIndex;
    public float speed = 4.7f;
    public static Action<int> OnHitPlayer;

    [Space(15)]
    [SerializeField] BasicSpecial specialSkill;

    // Start is called before the first frame update
    protected override void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        //animator.SetBool("IsMoving", true);
        base.Start();
        intermediateMove = false;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        LostCover();
    }
    protected override IEnumerator Choice()
    {
        animator.SetTrigger("Idle");

        //animator.SetBool("IsMoving", false);
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
                SelectCoverAndMove();
                break;
            case int n when n < probToShoot:
                if (actualCoverTransform)
                {
                    state = State.uncover;
                    SelectUncoverPosition(barrelPositions[transformIndex].shootPosition);
                    animator.SetTrigger("MoveRight");
                    //animator.SetBool("IsMoving", true);
                }
                break;
            case int n when n < (probToShoot + probToSpecial) && n> probToShoot
            && !switchTimerVsProbSpecial && probToSpecial!=0:
                state = State.specialAction;
                animator.SetTrigger("MoveRight");

                //animator.SetBool("IsMoving", true);
                break;
        }
        choising = false;
    }
    void SelectCoverAndMove()
    {
        state = State.move;
        
        SelectCoverPosition(barrelPositions);

    }
    void LostCover()
    {
        if (!actualCoverTransform)
        {
            StopCoroutine(Choice());
            choising = false;
            SelectCoverAndMove();
        }
    }
    public void SetObstaclesList(List<ObstacleInfo> obstacles,List<Transform> intermediatesSpotL)
    {
        intermediateSpots = intermediatesSpotL;
        barrelPositions = obstacles;
        transformIndex = UnityEngine.Random.Range(0, barrelPositions.Count);
        nextPos = new Vector3(barrelPositions[transformIndex].transform.position.x,
                              barrelPositions[transformIndex].transform.position.y ,
                              barrelPositions[transformIndex].transform.position.z + positionCorrectionForSorting);
        actualCover = nextPos;
        actualCoverTransform = barrelPositions[transformIndex].transform;
    }
    protected override void Move()
    {
        animator.SetTrigger("MoveRight");
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        if (transform.position == nextPos && !intermediateMove)
        {
            state = State.choice;
            animator.SetTrigger("Idle");
        }
        else if(transform.position == nextPos && intermediateMove)
        {
            SelectIntermediate();
            
        }
    }
    void SelectIntermediate()
    {
        Vector3 aux;
        if (intermediateMove)
        {
            aux = intermediatePosition;
            intermediatePosition = nextPos;
            nextPos = aux;
            intermediateMove = false;
        }

        foreach (Transform a in intermediateSpots)
        {
            if (Vector3.Distance(intermediatePosition, transform.position) >= Vector3.Distance(a.position, transform.position) &&
                Vector3.Distance(nextPos,transform.position) >= Vector3.Distance(a.position,nextPos) && intermediatePosition != transform.position)
            {
                intermediatePosition = a.position;
                intermediateMove = true;
            }
        }
        if (intermediateMove)
        {
            aux = intermediatePosition;
            intermediatePosition = nextPos;
            nextPos = aux;
        }

    }
    protected override void Uncover()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        if (transform.position == nextPos)
        {
            animator.SetTrigger("Idle");
            state = State.shoot;
        }
    }
    protected override IEnumerator Shoot()
    {
        animator.SetTrigger("Shoot");
        //animator.SetBool("IsMoving", false);
        //OnShoot?.Invoke();
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
        state = State.Cover;
        //animator.SetBool("IsMoving", true);
    }
    protected override void Cover()
    {
        //animator.SetTrigger("Idle");
        SelectActualCoverPosition();
        Move();
    }
    protected override void SpecialAction()
    {
        if (!specialSkill)
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
            aux = new Vector3(barrelPositions[transformIndex].transform.position.x,
                              barrelPositions[transformIndex].transform.position.y ,
                          barrelPositions[transformIndex].transform.position.z + positionCorrectionForSorting);
        } while (aux == nextPos);
        nextPos = aux;
        actualCover = aux;
        SelectIntermediate();
        actualCoverTransform = barrelPositions[transformIndex].transform;
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
}
