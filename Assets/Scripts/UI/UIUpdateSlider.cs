using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUpdateSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    public void SetNewValue(string eventName)
    {
        AkSoundEngine.SetRTPCValue(eventName,slider.value);
    }
}
