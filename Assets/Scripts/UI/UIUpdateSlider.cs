using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUpdateSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] string soundKey;
    [SerializeField] string rtpcName;

    private void Start() 
    {
        if(PlayerPrefs.HasKey(soundKey))
        {
            slider.value  = PlayerPrefs.GetFloat(soundKey);
            SetRtpcValue(slider.value);
        }
        else
        {
            SaveVolumeValue();
        }
    }
    public void SetNewValue()
    {
        SetRtpcValue(slider.value);
    }

    void SetRtpcValue(float value)
    {
        AkSoundEngine.SetRTPCValue(rtpcName,value);
        SaveVolumeValue();
    }
    void SaveVolumeValue() 
    {
        PlayerPrefs.SetFloat(soundKey,slider.value);
    }
}
