using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeController : MonoBehaviour
{
    string screenModeKey = "screenMode";
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public bool GetFullScreen() 
    {
        return Screen.fullScreen;
    }

    public void SwitchScreenMode() 
    {

        DataSave aux;
        aux.saveString = "";
        aux = SaveSystem.LoadArchive(screenModeKey);
        if (!(aux.saveString != "" && aux.saveString != "NULL"))
        {
            Debug.Log("no hay");
            Screen.fullScreen = false;
            PlayerPrefs.SetInt(screenModeKey,0);
        }
        else
        {

            DataSave aux2;
            aux2.saveString = "";
            aux2 = SaveSystem.LoadArchive(screenModeKey);
            if (aux2.saveFloat == 1)
            {
                Screen.fullScreen = false;
                SaveSystem.SaveArchive(0, screenModeKey);
            }
            else
            {
                Screen.fullScreen = true;
                SaveSystem.SaveArchive(1, screenModeKey);
            }
        }
        
    }
}
