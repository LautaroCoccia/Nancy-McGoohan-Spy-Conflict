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
            DataSave aux = SaveSystem.LoadArchive(soundKeys[i]);
            if (aux)
            {
                AkSoundEngine.SetRTPCValue(rtpcName[i], aux.saveFloat);
                slider[i].value = aux.saveFloat;
            }
            else 
            {
                DataSave aux2 = new DataSave(slider[i].value, soundKeys[i]);
                SaveSystem.SaveArchive(aux2,aux2.saveString);
            }
        }
        
    }
    public void SetRtpcNewValue(int index)
    {
        AkSoundEngine.SetRTPCValue(rtpcName[index],slider[index].value);
        PlayerPrefs.SetFloat(soundKeys[index],slider[index].value);
    }
}
