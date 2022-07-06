using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private static string dataDirPath = Application.persistentDataPath;

    private static string dataFileName = "test.json";

    /*
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    */

    public GameData Load()
    {
        Debug.Log("FileDataHandler.cs | Load() called");

        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserialize the data from file
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        Debug.Log("FileDataHandler.cs | Save() called");

        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create the directory the file will be written to if it doesn't already exist
            Debug.Log("FileDataHandler.cs | Trying creating the directory");
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize the C# game data object into JSON
            Debug.Log("FileDataHandler.cs | Making the JSON string");
            string dataToStore = JsonUtility.ToJson(data, true);
            Debug.Log("FileDataHandler.cs | JSON string is " + dataToStore);

            //write the serialized data to the file
            Debug.Log("FileDataHandler.cs | Going deep into file streaming");
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer=new StreamWriter(stream))
                {
                    Debug.Log("FileDataHandler.cs | Writing the file");
                    writer.Write(dataToStore);
                }
            }

            Debug.Log("FileDataHandler.cs | It looks like everything's done");
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }

        Debug.Log("FileDataHandler.cs | Returning from Save()");
    }

    public static bool Check()
    {
        Debug.Log("FileDataHandler.cs | Check() called");

        string fullPath = Path.Combine(dataDirPath, dataFileName);

        bool flag = false;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                flag = true;
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return flag;
    }
}
