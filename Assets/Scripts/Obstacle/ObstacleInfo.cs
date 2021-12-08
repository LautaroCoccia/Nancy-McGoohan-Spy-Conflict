using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    public List<Transform> shootPosition;
    public List<bool> slotOccuped;
    private void Start()
    {
        for(int i = 0; i < slotOccuped.Count;i++)
        {
            slotOccuped[i] = false;
        }
         
    
    }
}
