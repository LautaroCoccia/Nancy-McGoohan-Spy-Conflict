using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    choice,
    uncover,
    move,
    shoot,
    death,
    specialAction,
    startMove
}
public abstract class StateEnemy : MonoBehaviour
{
    public State state;
    protected bool choising = false;
    protected bool shooting = false;
    protected bool endAction = false;
    [SerializeField] protected float choisingTime = 0.0f;
    [SerializeField] RangeAttribute myRange;
    [SerializeField][Range(0,100)] protected int probToShoot = 0;
    [SerializeField][Range(0,100)] protected int probToSpecial = 0;
    [SerializeField] protected bool switchTimerVsProbSpecial;
    protected float timerToSpecial = 0.0f;
    [SerializeField]protected float timerToWaitSpecial = 3.0f;
    public State stateToAfterMove = State.choice;
    
    protected virtual void Start()
    {
        state = State.startMove;
    }
    protected virtual void Update()
    {
        switch (state)
        {
            case State.choice:
                if(!choising)StartCoroutine(Choice());
                break;
            case State.uncover:
                Uncover();
                break;
            case State.move:
                WaitToStopMove();
                break;
            case State.shoot:
                if (!shooting) { StartCoroutine(Shoot()); shooting = true; }
                break;
            case State.death:
                Death();
                break;
            case State.specialAction:
                SpecialAction();
                break;
            case State.startMove:
                GoToNextCoverPosition();
                break;
        }
    }
    protected abstract IEnumerator Choice();
    protected abstract void Uncover();
    protected abstract void GoToNextCoverPosition();
    protected abstract void WaitToStopMove();
    protected abstract IEnumerator Shoot();
    protected abstract void Death();
    protected abstract void SpecialAction();
}
