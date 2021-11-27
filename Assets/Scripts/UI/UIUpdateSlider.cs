using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUpdateSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image fillArea;
    // Start is called before the first frame update
    void Start()
    {
        fillArea.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fillArea.fillAmount = slider.value;
    }

    public void SetNewValue(string eventName)
    {
        //fillArea.fillAmount = value
    }
}
