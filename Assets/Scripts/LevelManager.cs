using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] float timer;
    public static Action<float> UpdateUITimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(timer >0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
