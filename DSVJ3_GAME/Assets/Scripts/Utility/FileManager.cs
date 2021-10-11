using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public static class FileManager<T>
{
    public static void SaveDataToFile(T objectToSave, string dataPath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);
        bf.Serialize(file, objectToSave);
        file.Close();
    }
    public static T LoadDataFromFile(string dataPath)
    {
        if (!File.Exists(dataPath)) { return default; }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        T objectToLoad = (T)bf.Deserialize(file);
        file.Close();

        return objectToLoad;
    }
    public static void DeleteFile(string dataPath)
    {
        if (!File.Exists(dataPath)) { return; }

        File.Delete(dataPath);
    }
}