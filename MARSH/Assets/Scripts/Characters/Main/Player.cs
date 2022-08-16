/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

[RequireComponent(typeof(MCMovementController))]
[RequireComponent(typeof(MCHealthController))]
public class Player: MonoBehaviour
{
    [SerializeField] private MCMovementController movement;
    [SerializeField] private MCHealthController health;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS
    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            DataManager.OnGameLoading += LoadData;
        }
        else
        {
            DataManager.OnGameLoading -= LoadData;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void LoadData(PlayerData data)
    {
        Deb("LoadData(): Loading transform data from Player.cs component\n{ position: " + data.position + ", rotation: " + data.rotation + " }");

        transform.position = data.position;
        transform.rotation = data.rotation;

        Deb("LoadData(): Loaded data is { position: " + transform.position + ", rotation: " + transform.rotation + " }");

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
