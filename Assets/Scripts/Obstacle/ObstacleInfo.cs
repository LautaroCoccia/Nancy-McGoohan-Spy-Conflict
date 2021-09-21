using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    public Transform coverPosition;
    public List<Transform> shootPosition;
    private void Start()
    {
        coverPosition = GetComponent<Transform>();
    }
}
