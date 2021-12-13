using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveArchive(DataSave data,string archiveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + archiveName + ".dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataSave LoadArchive(string archiveName)
    {
        string path = Application.persistentDataPath + "/"+archiveName+".dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataSave data = (DataSave)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}