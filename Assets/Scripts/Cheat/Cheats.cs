using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Cheats : MonoBehaviour
{
    public SpawnManager[] spawns;
    //public int bufferLength = 10;
    //List<char> command;
    //public string killAllEnemies;
    /*private void Start()
    {
        command = new List<char>();
    }
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            command.Add(Input.inputString[0]);
            for(int i = 0; i < killAllEnemies.Length &&  i < command.Count;i++)
            {
                if (command[i] == killAllEnemies[i])
                {
                    if(i == killAllEnemies.Length-1)
                    {
                        Debug.Log("killAllEnemies");
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }*/

    public void KillEnemies()
    {
        Debug.Log("cheat");
        foreach (SpawnManager e in spawns)
        {
            foreach(GameObject g in e.enemiesSpawned)
            {
                if(g)
                g.GetComponent<IHitable>().InstantDead();
            }
        }
    }
}
