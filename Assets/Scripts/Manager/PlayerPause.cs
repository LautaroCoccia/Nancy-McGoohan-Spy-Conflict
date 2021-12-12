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
        if (Input.GetKeyDown(pause))
        {
            onPauseCall?.Invoke();
        }
    }
}
