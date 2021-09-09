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
        shoot
    }
    protected State state;
    protected bool choising = false;
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
                Shoot();
                break;
        }
    }

    protected abstract IEnumerator Choice();
    protected abstract void Cover();
    protected abstract void Uncover();
    protected abstract void Move();
    protected abstract void Shoot();
}
