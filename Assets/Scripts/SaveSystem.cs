using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public struct DataSave
{
    public float saveFloat;
    public string saveString;
}
public class SaveSystem : MonoBehaviour
{
    public static void SaveArchive(float i, string str)
    {
        DataSave data;
        data.saveFloat = i;
        data.saveString = str;
        BinaryFormatter fm = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + str + ".dat";
        FileStream s = new FileStream(path, FileMode.Create);
        s.SetLength(0);
        fm.Serialize(s, data);
        s.Close();
    }
    public static DataSave LoadArchive(string str)
    {
        string path = Application.persistentDataPath + "/" + str + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter fm = new BinaryFormatter();
            FileStream s = new FileStream(path, FileMode.Open);
            if (s.Length != 0)
            {

                DataSave data = (DataSave)fm.Deserialize(s);
                s.Close();
                return data;
            }
            s.Close();
        }
        DataSave aux;
        aux.saveString = "NULL";
        aux.saveFloat = 0;
        return aux;
    }
}