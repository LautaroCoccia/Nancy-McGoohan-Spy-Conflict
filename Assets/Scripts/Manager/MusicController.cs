using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicController : MonoBehaviour
{
    [SerializeField] List<string> listOfEvents;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listOfEvents.Count ; i++)
        {
            AkSoundEngine.PostEvent(listOfEvents[i], gameObject);
        }
    }
    public void StopAllSounds()
    {
        AkSoundEngine.PostEvent("stop_all", gameObject);
    }
}
