/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;
using Cinemachine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private PlayerData currentPlayerData;
    [SerializeField] private EnvironmentData currentEnvironmentData;
    [SerializeField] private GameData currentGameData, loadedGameData;

    public SettingsData settingsData;

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

        if (GameManager.Instance.GetSceneIndex() != 0)
        {
            LoadSettingsData();
        }

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

    public void LoadSettingsData()
    {
        settingsData = new SettingsData();

        CinemachineFreeLook cfl = GameObject.Find("ThirdPersonCamera").GetComponent<CinemachineFreeLook>();

        if (cfl != null)
        {
            cfl.m_YAxis.m_InvertInput = false;
            cfl.m_XAxis.m_InvertInput = false;

            settingsData.invertYAxis = cfl.m_YAxis.m_InvertInput;
            settingsData.invertXAxis = cfl.m_XAxis.m_InvertInput;

            cfl.m_YAxis.m_MaxSpeed = 1f;
            cfl.m_XAxis.m_MaxSpeed = 100f;

            settingsData.mouseSensitivity = 1f;
        }
    }

    public int GetCurrentLevel()
    {
        return currentGameData.player.level;
    }

    public PlayerData GetCurrentPlayerData()
    {
        return currentGameData.player;
    }

    public bool ReloadGame()
    {
        if (loadedGameData == null)
        {
            return false;
        }

        currentGameData = new GameData(loadedGameData);

        Camera.main.transform.SetPositionAndRotation(currentGameData.player.camPos, currentGameData.player.camRot);

        OnGameLoading?.Invoke(currentGameData.player);

        return true;
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

            GameManager.OnParamsReset += OnParamsReset;
        }
        else
        {
            MCMovementController.OnTransformChanged -= OnValueChanged;
            MCHealthController.OnValueChanged -= OnValueChanged;
            MCCollectionManager.OnValueChanged -= OnValueChanged;

            Checkpoint.OnCheckpoint -= SaveGameData;
            Collectable.OnCollection -= OnNewCollection;

            GameManager.OnParamsReset -= OnParamsReset;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnParamsReset()
    {
        EventSubscriber(false);
        EventSubscriber(true);
    }

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
                currentPlayerData.camPos = Camera.main.transform.position;
                currentPlayerData.camRot = Camera.main.transform.rotation;

                break;

            case ChParam.Rot:
                currentPlayerData.rotation = (Quaternion)value;
                currentPlayerData.camPos = Camera.main.transform.position;
                currentPlayerData.camRot = Camera.main.transform.rotation;

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
