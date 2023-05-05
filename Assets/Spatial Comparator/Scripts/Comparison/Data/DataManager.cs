using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class DataManager
{
    // Path to save the data
    private static string persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;

    public SessionData currentSessionData;

    public DataManager()
    {
        BuildDirectories();
    }

    public void InitializeSession()
    {
        Guid id = new Guid();
        currentSessionData = new SessionData(id.ToString());
        currentSessionData.gender = Gender.Female;
        currentSessionData.age = 18;
    }

    public void SaveSession()
    {
        SaveData(currentSessionData, persistentPath+"Sessions/session_"+currentSessionData.id);
    }

    public static void BuildDirectories()
    {
        Directory.CreateDirectory(persistentPath + "Sessions/");
    }

    public void SaveData<T>(T data, string path)
    {
        string savePath = path + ".json";

        // Create directory with given path
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));

        // Convert level data into json format
        string jsonFile = JsonUtility.ToJson(data);

        // Write data into file
        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(jsonFile);
            }
        }
    }

    private T LoadData<T>(string path)
    {
        string filePath = path + ".json";
        string jsonData = "";

        if (File.Exists(filePath))
        {
            using StreamReader reader = new StreamReader(filePath);
            jsonData = reader.ReadToEnd();
        }

        // Convert json to object
        return JsonUtility.FromJson<T>(jsonData);
    }
    
}
