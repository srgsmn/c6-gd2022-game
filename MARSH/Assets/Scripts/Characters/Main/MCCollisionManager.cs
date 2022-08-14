/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class MCCollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Deb("OnTriggerEnter(): Player collided with object with name " + other.gameObject.name + " and tag " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "Store":
                Deb("OnTriggerEnter(): Player collided with a store, trying to open it...");
                GameManager.Instance.OpenStore();
                break;
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
