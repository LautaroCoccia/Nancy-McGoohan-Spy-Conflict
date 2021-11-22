using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonSound : MonoBehaviour
{
    public void OnButtonPressed()
    {
        AkSoundEngine.SetSwitch("button", "button_press", gameObject);
        AkSoundEngine.PostEvent("button", gameObject);
    }
    public void OnButtonStart()
    {
        AkSoundEngine.SetSwitch("button", "button_start", gameObject);
        AkSoundEngine.PostEvent("button", gameObject);
    }
}
