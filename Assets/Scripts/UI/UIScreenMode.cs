using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenMode : MonoBehaviour
{
    [SerializeField] ScreenModeController screenMode;
    [SerializeField] GameObject UIOn;
    [SerializeField] GameObject UIOff;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScreenModeUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScreenModeUI()
    {
        if(!screenMode.GetFullScreen())
        {
            UIOn.SetActive(true);
            UIOff.SetActive(false);
        }
        else
        {
            UIOn.SetActive(false);
            UIOff.SetActive(true);
        }
    }
}
