using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static Action Saving;
    public static Action Loading;
    
    
    public static void Save<T>(T objectToSave,string key)
    {
        string path = Application.persistentDataPath + "/saves/";//folder for save files
        Directory.CreateDirectory(path);//creating this folder
        BinaryFormatter formatter = new BinaryFormatter();//creating new formatter
        using (FileStream fileStream= new FileStream(path+key+".txt",FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
            fileStream.Seek(0, SeekOrigin.Begin);
        }
    }

    public static T Load<T>( string key)
    {
        string path = Application.persistentDataPath + "/saves/";//folder for save files
        BinaryFormatter formatter = new BinaryFormatter();//creating new formatter
        T returnValue = default(T);//if we dont find a file wich has data in it, giving default value for that type
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
        {
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Position = 0;
            returnValue =  (T)formatter.Deserialize(fileStream);// deserializing to generic T
        }
        return returnValue;
    }

    public static bool SaveAlreadyExists(string key)
    {
        string path = Application.persistentDataPath + "/saves/"+key+".txt";
        return File.Exists(path);
    }
    public static void DeletSaves()
    {
        string path = Application.persistentDataPath + "/saves/";

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        directoryInfo.Delete();
        directoryInfo.Create();

    }

    public static void StartSaving()
    {
        Saving?.Invoke();// invoking if null
    }
    public static void StartLoading()
    {
        Loading?.Invoke();
    }
}
