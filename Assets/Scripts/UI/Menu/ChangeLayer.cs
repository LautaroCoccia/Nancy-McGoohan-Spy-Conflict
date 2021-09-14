using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeLayer : MonoBehaviour
{
    [SerializeField] GameObject CurrentLayer;
    [SerializeField] GameObject NextLayer;
    public void Change()
    {
        CurrentLayer.SetActive(false);
        NextLayer.SetActive(true);
    }
}
