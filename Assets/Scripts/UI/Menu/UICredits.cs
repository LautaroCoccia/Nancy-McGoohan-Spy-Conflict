using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICredits : MonoBehaviour
{
    public GameObject[] layers;
    int index = 0;
    public Button previousButton;
    public Button nextButton;
    void OnEnable()
    {
        index = 0;
        layers[0].SetActive(true);
        for (int i = 1; i < layers.Length; i++)
        {
            layers[i].SetActive(false);
        }

    }
    public void Previous()
    {
        if (index > 0)
        {
            layers[index].SetActive(false);
            index--;
            layers[index].SetActive(true);
            nextButton.interactable = true;
            if(index <= 0)
            {
                previousButton.interactable = false;
            }
        }

    }
    public void Next()
    {
        if(index<layers.Length)
        {
            layers[index].SetActive(false);
            index++;
            layers[index].SetActive(true);
            previousButton.interactable = true;
            if (index >= layers.Length)
            {
                nextButton.interactable = false;
            }
        }
    }

}
