﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ScaredSpecial : BasicSpecial
{
    List<Transform> scapePoint;
    bool inProgress = false;
    public delegate List<Transform> GetScapePoints();
    public static GetScapePoints scapePointsGetter;
    BaseEnemy myBase;
    bool activeAfterSkillNow;
    private void Awake()
    {
        SetScapePoints(scapePointsGetter?.Invoke());
        myBase = GetComponent<BaseEnemy>();
        activeAfterSkillNow = false;
    }
    public override bool Skill()
    {
        
        if (!inProgress && !activeAfterSkillNow)
        {

            Vector3 aux = scapePoint[0].position;
            for(short i = 1; i < scapePoint.Count;i++)
            {
                if(Vector3.Distance(transform.position,aux) > 
                    Vector3.Distance(transform.position, scapePoint[i].position))
                {
                    aux = scapePoint[i].position;
                }
            }
            myBase.SetDestination(aux);
            myBase.stateToAfterMove = StateEnemy.State.specialAction;
            myBase.state = StateEnemy.State.move;
            activeAfterSkillNow = true;
            inProgress = true;
        }
        else if (activeAfterSkillNow)
        {
            AfterSkill();
        }
        return inProgress;
    }
    public override bool AfterSkill()
    {
        Destroy(transform.gameObject);
        return false;
    }
    public void SetScapePoints(List<Transform> newPoints)
    {
        scapePoint = newPoints;
    }
    
}
