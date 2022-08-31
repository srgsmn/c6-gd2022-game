/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class MCCollisionManager : MonoBehaviour
{

    private MCHealthController MCHealthController;
    [SerializeField]
    [ReadOnlyInspector] private bool storeNearby = false, checkpointNearby = false, atStore = false, atCheckpoint = false, atLadder = false;

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Deb("OnTriggerEnter(): Player collided with object with name " + other.gameObject.name + " and tag " + other.gameObject.tag);

        switch (other.gameObject.tag)
        {
            case "StoreNearby":
                //TODO
                storeNearby = true;

                OnProximity?.Invoke(ProximityObject.Store, ProximityInfo.Tutorial);

                break;

            case "ChechpointNearby":
                //TODO
                checkpointNearby = true;

                OnProximity?.Invoke(ProximityObject.Checkpoint, ProximityInfo.Tutorial);

                break;

            case "Store":
                Deb("OnTriggerEnter(): Player near a store, waiting for action...");

                atStore = true;
                OnProximity?.Invoke(ProximityObject.Store, ProximityInfo.Memo);

                //GameManager.Instance.OpenStore();
                break;

            case "Checkpoint":
                Deb("OnTriggerEnter(): Player near a checkpoint, waiting for action...");
                //GameManager.Instance.SaveGame();

                atCheckpoint = true;
                OnProximity?.Invoke(ProximityObject.Checkpoint, ProximityInfo.Memo);

                break;

            case "SugarLump":
            case "ChocoChip":
                Deb("OnTriggerEnter(): Player collided with a collectable (" + other.tag + "). Delegating collection operation to collectable.");

                break;

            case "LadderEndpoint":
                atLadder = true;
                OnProximity?.Invoke(ProximityObject.Ladder, ProximityInfo.Memo);

                break;

            case "GameEnd":
                OnGameEnd?.Invoke();

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
            case "StoreNearby":
                //TODO
                storeNearby = false;

                OnProximity?.Invoke(ProximityObject.Store, ProximityInfo.None);

                break;

            case "ChechpointNearby":
                //TODO
                checkpointNearby = false;

                OnProximity?.Invoke(ProximityObject.Checkpoint, ProximityInfo.None);

                break;

            case "Store":
                Deb("OnTriggerExit(): Player no more near a store...");

                atStore = false;

                OnProximity?.Invoke(ProximityObject.Store, ProximityInfo.None);

                break;

            case "Checkpoint":
                Deb("OnTriggerExit(): Player no more near to a checkpoint...");

                atCheckpoint = false;

                OnProximity?.Invoke(ProximityObject.Checkpoint, ProximityInfo.None);

                break;

            case "LadderEndpoint":
                atLadder = false;

                OnProximity?.Invoke(ProximityObject.Ladder, ProximityInfo.None);

                break;

            case "Coccodrillo":
                transform.SetParent(null);
                
                break;
        }
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void ProximityEvent(ProximityObject item, ProximityInfo info);
    public static ProximityEvent OnProximity;

    public delegate void TeleportEvent(Transform position);
    public static TeleportEvent OnTeleport;

    public delegate void GameEndEvent();
    public static GameEndEvent OnGameEnd;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            InputManager.OnAttackInput += OnAction;
        }
        else
        {
            InputManager.OnAttackInput -= OnAction;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnAction(bool flag)
    {
        Deb("OnAction(): flag = " + flag);

        if (atStore && flag)
        {
            GameManager.Instance.OpenStore();

            flag = false;
        }

        if(atCheckpoint && flag)
        {
            GameManager.Instance.SaveGame();

            flag = false;
        }

        if(atLadder && flag)
        {
            OnTeleport(transform);
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
