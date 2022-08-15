/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - Check if active or not: GameData should send an event call on start with the saved id, every cp should read it and decide if they're on or off
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [SerializeField]
    [ReadOnlyInspector] private string id = null;

    [Header("Text instantiation parameter:")]
    [SerializeField] private GameObject animatedTxtPrefab;
    [SerializeField] private Canvas targetCanvas;

    private GameObject instance;
    private bool isActive = false;

    // CONTEXT MENU FUNCTIONS __________________________________________________ CONTEXT MENU FUNCTIONS
    [ContextMenu("Generate guid for ID")]
    private void GenerateGUID()
    {
        id = Guid.NewGuid().ToString();
    }

    [ContextMenu("Reset ID")]
    private void ResetID()
    {
        id = null; ;
    }

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isActive)
        {
            OnCheckpoint(id);

            instance = Instantiate(animatedTxtPrefab, targetCanvas.transform);

            Destroy(instance, 2.75f);
        }
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void CheckpointEvent(string id);
    public static CheckpointEvent OnCheckpoint;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            OnCheckpoint += CPCheck;
            DataManager.OnCPLoad += CPCheck;
        }
        else
        {
            OnCheckpoint -= CPCheck;
            DataManager.OnCPLoad -= CPCheck;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void CPCheck(string id)
    {
        Deb("CPCheck(): detected a new checkpoint. Comparing the IDs");
        if (this.id == id && !isActive)
            isActive = true;
        else
            isActive = false;
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
