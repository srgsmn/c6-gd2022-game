/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class PopUps : MonoBehaviour
{
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private GameObject store;
    [SerializeField] private GameObject ladder;
    [SerializeField] private GameObject spawner;

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
            Deb("EventSubscriber(): subscribing...");
            MCCollisionManager.OnProximity += OnProximity;

        }
        else
        {
            Deb("EventSubscriber(): unsubscribing...");
            MCCollisionManager.OnProximity -= OnProximity;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS
    private void OnProximity(ProximityObject obj, ProximityInfo info)
    {
        Deb("###" + obj + " " + info);

        if (info == ProximityInfo.Memo)
        {
            switch (obj)
            {
                case ProximityObject.Checkpoint:
                    checkpoint.SetActive(true);

                    break;

                case ProximityObject.Store:
                    store.SetActive(true);

                    break;

                case ProximityObject.Ladder:
                    ladder.SetActive(true);

                    break;

                case ProximityObject.Spawner:
                    spawner.SetActive(true);

                    break;
            }
        }
        else if (info == ProximityInfo.None || info == ProximityInfo.Tutorial)
        {
            checkpoint.SetActive(false);
            store.SetActive(false);
            ladder.SetActive(false);
            spawner.SetActive(false);
        }
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
