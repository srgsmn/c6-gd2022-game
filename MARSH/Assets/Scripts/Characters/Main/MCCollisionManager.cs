/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class MCCollisionManager : MonoBehaviour
{

    private MCHealthController MCHealthController;

    private void OnTriggerEnter(Collider other)
    {
        Deb("OnTriggerEnter(): Player collided with object with name " + other.gameObject.name + " and tag " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "Store":
                Deb("OnTriggerEnter(): Player collided with a store, trying to open it...");
                GameManager.Instance.OpenStore();
                break;

            case "Checkpoint":
                Deb("OnTriggerEnter(): Player collided with a checkpoint, trying to save...");
                GameManager.Instance.SaveGame();

                break;

            case "SugarLump":
            case "ChocoChip":
                Deb("OnTriggerEnter(): Player collided with a collectable (" + other.tag + "). Delegating collection operation to collectable.");

                break;
            case "AcquaFiume":
                MCHealthController = gameObject.GetComponent<MCHealthController>();
                MCHealthController.Die();
                
                break;
            case "Coccodrillo":
                transform.SetParent(other.gameObject.transform);
                
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coccodrillo":
                transform.SetParent(null);
                
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
