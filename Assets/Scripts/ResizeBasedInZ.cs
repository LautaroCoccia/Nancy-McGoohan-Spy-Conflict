using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBasedInZ : MonoBehaviour
{
    float distanceMark;
    float lastPosZ;
    float subtractToScale;
    public float diffToSum = 2;
    float startScale = 1;
    void Start()
    {
        lastPosZ = transform.position.z;
    }
    void Update()
    {
        SetNewScaleSum(subtractToScale);
    }
    public void SetDistanceMark(float distanceMark, float subtractToScale)
    {
        this.distanceMark = distanceMark;
        this.subtractToScale = subtractToScale;
    }
    void SetNewScaleSum(float changeValue)
    {
        transform.localScale = new Vector3(startScale * (changeValue * (distanceMark - transform.position.z)),
                                           startScale * (changeValue * (distanceMark - transform.position.z)),
                                           transform.localScale.z);

    }
    public void SetNewScale(float changeValue)
    {
        transform.localScale = new Vector3(changeValue, changeValue,transform.localScale.z);
    }
}
