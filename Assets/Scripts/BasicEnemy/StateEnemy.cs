﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateEnemy : MonoBehaviour
{
    protected enum State
    {
        choice,
        Cover,
        uncover,
        move,
        shoot,
        specialAction
    }
    protected State state;
    protected bool choising = false;
    protected bool shooting = false;
    protected bool endAction = false;
    [SerializeField] protected float choisingTime = 0.0f;
    [SerializeField][Range(0,100)] protected int probToShoot = 0;
    protected virtual void Start()
    {
        state = State.move;
    }
    protected virtual void Update()
    {
        switch (state)
        {
            case State.choice:
                if(!choising)StartCoroutine(Choice());
                break;
            case State.Cover:
                Cover();
                break;
            case State.uncover:
                Uncover();
                break;
            case State.move:
                Move();
                break;
            case State.shoot:
                if (!shooting) { StartCoroutine(Shoot()); shooting = true; }
                break;
            case State.specialAction:
                break;
        }
    }

    protected abstract IEnumerator Choice();
    protected abstract void Cover();
    protected abstract void Uncover();
    protected abstract void Move();
    protected abstract IEnumerator Shoot();
    protected abstract void SpecialAction();
}
