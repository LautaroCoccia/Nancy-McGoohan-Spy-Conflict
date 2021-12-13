using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour
{
    [SerializeField] List<string> soundKeys;
    [SerializeField] List<string> rtpcName;
    [SerializeField] List<Slider> slider;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < soundKeys.Count ; i++)
        {
            if(PlayerPrefs.HasKey(soundKeys[i]))
            {
                AkSoundEngine.SetRTPCValue(rtpcName[i],PlayerPrefs.GetFloat(soundKeys[i]));
                slider[i].value = PlayerPrefs.GetFloat(soundKeys[i]);
            }
            else 
            {
                PlayerPrefs.SetFloat(soundKeys[i],slider[i].value);
            }
        }
        
    }
    public void SetRtpcNewValue(int index)
    {
        AkSoundEngine.SetRTPCValue(rtpcName[index],slider[index].value);
        PlayerPrefs.SetFloat(soundKeys[index],slider[index].value);
    }
}
