using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeController : MonoBehaviour
{
    string screenModeKey = "screenMode";
    // Start is called before the first frame update
    void Start()
    {
        SwitchScreenMode();
    }
    public bool GetFullScreen() 
    {
        return Screen.fullScreen;
    }

    public void SwitchScreenMode() 
    {
        if(!PlayerPrefs.HasKey(screenModeKey))
        {
            Debug.Log("no hay");
            Screen.fullScreen = false;
            PlayerPrefs.SetInt(screenModeKey,0);
        }
        else
        {
           
            if(PlayerPrefs.GetInt(screenModeKey) == 1)
            {
                Screen.fullScreen = false;
                PlayerPrefs.SetInt(screenModeKey,0);
            }
            else
            {
                Screen.fullScreen = true;
                PlayerPrefs.SetInt(screenModeKey,1);
            }
        }
        
    }
}
