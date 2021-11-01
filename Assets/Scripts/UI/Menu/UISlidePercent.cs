using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISlidePercent : MonoBehaviour
{
    [SerializeField] Slider mySlide;
    [SerializeField] TMP_Text text;
    void Start()
    {
        Change();
    }
    public void Change()
    {
         text.text = mySlide.value.ToString() + "%";
    }
}
