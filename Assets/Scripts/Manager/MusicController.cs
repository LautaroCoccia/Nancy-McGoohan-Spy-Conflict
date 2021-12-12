using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MusicController : MonoBehaviour
{
    [SerializeField] List<string> listOfEvents;
    [SerializeField] List<string> soundKeys;
    [SerializeField] List<string> rtpcName;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listOfEvents.Count ; i++)
        {
            AkSoundEngine.PostEvent(listOfEvents[i], gameObject);
        }

        for (int i = 0; i < soundKeys.Count ; i++)
        {
            if(PlayerPrefs.HasKey(soundKeys[i]))
            {
                AkSoundEngine.SetRTPCValue(rtpcName[i],PlayerPrefs.GetFloat(soundKeys[i]));
            }
        }
    }
    public void StopAllSounds()
    {
        AkSoundEngine.PostEvent("stop_all", gameObject);
    }
}
