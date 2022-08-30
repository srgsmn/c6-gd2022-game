/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class StoreTutorial : MonoBehaviour
{
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
            MCCollisionManager.OnProximity += OnProximity;

        }
        else
        {
            MCCollisionManager.OnProximity -= OnProximity;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS
    private void OnProximity(ProximityObject obj, ProximityInfo info)
    {
        Deb("###"+obj+" "+info);

        if (obj==ProximityObject.Store && info == ProximityInfo.Tutorial)
        {
            gameObject.SetActive(true);
        }

        if (obj == ProximityObject.Store && (info == ProximityInfo.None || info == ProximityInfo.Memo))
        {
            gameObject.SetActive(false);
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
