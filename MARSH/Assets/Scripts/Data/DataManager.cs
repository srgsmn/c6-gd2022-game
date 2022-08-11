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
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS


    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCMovementController.OnTransformChanged += OnCurrentParamsChanged;
        }
        else
        {
            MCMovementController.OnTransformChanged -= OnCurrentParamsChanged;

        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnCurrentParamsChanged(CharParam param, object value)
    {
        Deb("OnCurrentParamsChanged(): change detected (" + param + ", " + value + ")");

        switch (param)
        {
            case CharParam.Pos:
                currentPlayerData.position = (Vector3)value;

                break;

            case CharParam.Rot:
                currentPlayerData.rotation = (Quaternion)value;

                break;
        }
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebugMsgType type = DebugMsgType.log)
    {
        switch (type)
        {
            case DebugMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);

                break;

            case DebugMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);

                break;

            case DebugMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
