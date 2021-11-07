﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DestroyableWallStatesController : MonoBehaviour
{
    [SerializeField] List<GameObject> wallStates;
    public static Action<Transform> DeleteFromObjectList;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        DestroyableWall.setNewWallState += SetActiveWallState;
    }
    void SetActiveWallState()
    {
        wallStates[index].SetActive(false);
        
        if (index < wallStates.Count -1)
        {
            index++;
            if(wallStates[index] != null)
            {
                wallStates[index].SetActive(true);
            }
        }
        else
        {
            DeleteFromObjectList?.Invoke(transform);
            Destroy(gameObject);
        }
       
    }
    private void OnDisable()
    {
        DestroyableWall.setNewWallState -= SetActiveWallState;
    }
}
