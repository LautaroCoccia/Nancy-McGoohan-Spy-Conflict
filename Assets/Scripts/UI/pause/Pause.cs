using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pause : MonoBehaviour
{
    static bool isPaused;
    public GameObject pauseLayer;
    public GameObject[] popUp;
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
            AkSoundEngine.PostEvent("pause_game", gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent("pause_resume", gameObject);
            pauseLayer.SetActive(false);
            Time.timeScale = 1.0f;
            isPaused = false;
            foreach(GameObject a in popUp)
            {
                a.SetActive(false);
            }
        }
    }
    public static bool GetPause()
    {
        return isPaused;
    }
}
