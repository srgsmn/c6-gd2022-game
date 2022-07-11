/* DataPersistenceManager.cs
 * 
 * HOW TO:
 *  - save: DataPersistenceManager.instance.NewGame(); etc --> devo chiamare l'istanza e il suo metodo pubblico
 *  - load: vedi sopra
 * 
 * REF:
 *  - 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static DataPersistenceManager instance { get; private set; }

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        /*
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }

        instance = this;
        */

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");

            Destroy(gameObject);
        }

        //this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataHandler = new FileDataHandler();
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void Start()
    {
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        Debug.Log("DataPersistenceManager.cs | LoadGame() called");
        // Load any saved data from file using data handler
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // Push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        Debug.Log("DataPersistenceManager.cs | SaveGame() called");

        // Pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // Save that data to a file using the data handler
        Debug.Log("DataPersistenceManager.cs | Passing data to handler...");
        dataHandler.Save(gameData);
    }

    public bool IsSaved()
    {
        Debug.Log("DataPersistenceManager.cs | IsSaved() called");

        if (dataHandler.Load() == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            return false;
        }
        else
        {
            return true;
        }
    }

    public GameData GetGameData()
    {
        return gameData;
    }
}
