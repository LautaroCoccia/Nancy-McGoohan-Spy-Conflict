using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeLayer : MonoBehaviour
{
    [SerializeField] GameObject CurrentLayer;
    [SerializeField] GameObject NextLayer;
    public void Change()
    {
        PopDown();
        PopUp();
    }
    public void PopUp()
    {
        NextLayer.SetActive(true);
    }
    public void PopDown()
    {
        CurrentLayer.SetActive(false);
    }
}
