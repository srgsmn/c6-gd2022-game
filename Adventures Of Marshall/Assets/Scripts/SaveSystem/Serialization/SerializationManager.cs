/* Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * Ref:
 *  - (SaveSystem) https://www.youtube.com/watch?v=5roZtuqZyuw
 */
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class SerializationManager
{
    public static string directory = Application.persistentDataPath + "/saves";
    public static string fileName = "gamedata01.save";
    public static string path = directory + "/" + fileName;

    public static bool Save(object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        FileStream file = File.Create(path);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load()
    {
        if (!File.Exists(path))
            return null;

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);

            file.Close();

            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at " + path);

            file.Close();

            return null;
        }
    }

    /* DA METTERE DA QUALCHE ALTRA PARTE, forse in game manager
    public void OnLoadGame()
    {
        SaveData.current = (SaveData)SerializationManager.Load(path);
    }
    */

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector3SerializationSurrogate v3Surrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quatSurrogate = new QuaternionSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quatSurrogate);

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
