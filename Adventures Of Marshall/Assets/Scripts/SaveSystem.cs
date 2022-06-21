using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    private static string savePath
    {
        get { return Application.persistentDataPath + "/.save"; }
    }

    public static bool isSaved
    {
        get { return File.Exists(savePath); }
    }

    public static void SaveData(GameManager.GameData gameData, GameManager.PlayerData playerData)
    {
        Debug.Log("SaveSystem.cs | Creating the binary formatter");
        BinaryFormatter formatter = new BinaryFormatter();

        Debug.Log("SaveSystem.cs | Creating the filestream (in " + savePath + ")");
        FileStream stream = new FileStream(savePath, FileMode.Create);

        Debug.Log("SaveSystem.cs | Retrieving player's data");
        //PlayerData data = new PlayerData(player); FIXME
        //Debug.Log("StartMenu.cs | Data to be saved is: " + data.ToString());  FIXME
    }

    public static void SavePlayer(Player player)
    {
        Debug.Log("SaveSystem.cs | Creating the binary formatter");
        BinaryFormatter formatter = new BinaryFormatter();

        Debug.Log("SaveSystem.cs | Creating the filestream (in "+savePath+")");
        FileStream stream = new FileStream(savePath, FileMode.Create);

        Debug.Log("SaveSystem.cs | Retrieving player's data");
        PlayerData data = new PlayerData(player);
        Debug.Log("StartMenu.cs | Data to be saved is: " + data.ToString());

        Debug.Log("SaveSystem.cs | Serializing the data");
        formatter.Serialize(stream, data);

        Debug.Log("SaveSystem.cs | Closing the stream");
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(savePath))
        {
            Debug.Log("SaveSystem.cs | The file exists, opening the formatter");
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log("SaveSystem.cs | Creating the stream");
            FileStream stream = new FileStream(savePath, FileMode.Open);

            Debug.Log("SaveSystem.cs | Deserializing data");
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            Debug.Log("SaveSystem.cs | Closing the stream");
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + savePath);
            return null;
        }
    }

    public static void DeleteSaved()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        //OR
        /*
        try { File.Delete(savePath); }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        */
    }
}
