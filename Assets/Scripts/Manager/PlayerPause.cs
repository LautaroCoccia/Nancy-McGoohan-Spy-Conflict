using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerPause : MonoBehaviour
{
    public KeyCode pause;
    public static Action onPauseCall;
    private void Update()
    {
        if (Time.timeScale > 0 && Input.GetKeyDown(pause))
        {
            onPauseCall?.Invoke();
        }
    }
}
