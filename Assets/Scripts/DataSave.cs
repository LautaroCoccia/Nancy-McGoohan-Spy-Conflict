using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public float saveFloat;
    public string saveString;
    public DataSave(float i)
    {
        saveFloat = i;
    }
    public DataSave(string s)
    {
        saveString = s;
    }
    public DataSave(float i, string s)
    {
        saveFloat = i;
        saveString = s;
    }
}
