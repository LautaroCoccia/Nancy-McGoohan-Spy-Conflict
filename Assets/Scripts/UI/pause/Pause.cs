using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pause : MonoBehaviour
{
    static bool isPaused;
    public GameObject pauseLayer;
    private void OnEnable()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        PlayerPause.onPauseCall += PuaseChange;
    }
    private void OnDisable()
    {
        PlayerPause.onPauseCall -= PuaseChange;
    }
    public void PuaseChange()
    {
        if (!isPaused)
        {
            pauseLayer.SetActive(true);
            Time.timeScale = 0.0f;
            isPaused = true;
        }
        else
        {
            pauseLayer.SetActive(false);
            Time.timeScale = 1.0f;
            isPaused = false;
        }
    }
    public static bool GetPause()
    {
        return isPaused;
    }
}
