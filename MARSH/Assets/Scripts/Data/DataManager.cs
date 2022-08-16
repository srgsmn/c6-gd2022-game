/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private PlayerData currentPlayerData;
    [SerializeField] private EnvironmentData currentEnvironmentData;
    [SerializeField] private GameData currentGameData, loadedGameData;


    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        EventSubscriber();
    }
    
    private void Start()
    {
        currentPlayerData = new PlayerData();

        currentEnvironmentData = new EnvironmentData();

        currentGameData = new GameData(currentPlayerData, currentEnvironmentData);

        if (currentEnvironmentData.lastCheckpointID != null)
        {
            OnCPLoad(currentEnvironmentData.lastCheckpointID);
        }

        if (currentEnvironmentData.collectablesIDs.Count != 0)
        {
            OnCollectionLoad(currentEnvironmentData.collectablesIDs);
        }
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    public int GetCurrentLevel()
    {
        return currentGameData.player.level;
    }

    public PlayerData GetCurrentPlayerData()
    {
        return currentGameData.player;
    }

    public void ReloadGame()
    {
        currentGameData = new GameData(loadedGameData);   //FIXME

        OnGameLoading?.Invoke(currentGameData.player);
    }

    public void ResetGameData()
    {
        currentEnvironmentData = new EnvironmentData();
        currentPlayerData = new PlayerData();

        currentGameData = new GameData(currentPlayerData, currentEnvironmentData);

        loadedGameData = null;
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void ValueUpdateEvent(bool saved, ChParam param, object value);
    public static ValueUpdateEvent OnValueUpdate;
    public delegate void CPLoadEvent(string id);
    public static CPLoadEvent OnCPLoad;
    public delegate void ColelctionLoadEvent(List<string> ids);
    public static ColelctionLoadEvent OnCollectionLoad;

    public delegate void SavedDataDebugEvent(GameData data);
    public static SavedDataDebugEvent OnSavedData;

    public delegate void GameLoadingEvent(PlayerData data);
    public static GameLoadingEvent OnGameLoading;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCMovementController.OnTransformChanged += OnValueChanged;
            MCHealthController.OnValueChanged += OnValueChanged;
            MCCollectionManager.OnValueChanged += OnValueChanged;

            Checkpoint.OnCheckpoint += SaveGameData;
            Collectable.OnCollection += OnNewCollection;
        }
        else
        {
            MCMovementController.OnTransformChanged -= OnValueChanged;
            MCHealthController.OnValueChanged -= OnValueChanged;
            MCCollectionManager.OnValueChanged -= OnValueChanged;

            Checkpoint.OnCheckpoint -= SaveGameData;
            Collectable.OnCollection -= OnNewCollection;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnNewCollection(CollectableType type, string id)
    {
        currentEnvironmentData.collectablesIDs.Add(id);
    }

    private void OnValueChanged(ChParam param, object value)
    {
        //Deb("OnTransformParamsChanged(): change detected (" + param + ", " + value + ")");

        switch (param)
        {
            case ChParam.Pos:
                currentPlayerData.position = (Vector3)value;

                break;

            case ChParam.Rot:
                currentPlayerData.rotation = (Quaternion)value;

                break;

            case ChParam.Health:
                currentPlayerData.health = (float)value;

                break;

            case ChParam.MaxHealth:
                currentPlayerData.maxHealth = (float)value;

                break;

            case ChParam.DefHFact:
                currentPlayerData.defHFactor = (float)value;

                break;

            case ChParam.Armor:
                currentPlayerData.armor = (float)value;

                break;

            case ChParam.MaxArmor:
                currentPlayerData.maxArmor = (float)value;

                break;

            case ChParam.DefAFact:
                currentPlayerData.defAFactor = (float)value;

                break;

            case ChParam.SL:
                currentPlayerData.sl = (int)value;

                break;

            case ChParam.CC:
                currentPlayerData.cc = (int)value;

                break;
        }

        OnValueUpdate?.Invoke(false, param, value);
    }

    private void SaveGameData(string id)
    {
        
        currentGameData.environment.lastCheckpointID = id;
        //TODO
        loadedGameData = new GameData(currentGameData); //FIXME

        OnSavedData?.Invoke(loadedGameData);
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebMsgType type = DebMsgType.log)
    {
        switch (type)
        {
            case DebMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);

                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);

                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
