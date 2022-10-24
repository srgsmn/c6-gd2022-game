/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
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
    [SerializeField][ReadOnlyInspector] private bool isActive = true;
    [SerializeField][ReadOnlyInspector] private bool inPlace = false;


    // CONTEXT MENU FUNCTIONS __________________________________________________ CONTEXT MENU FUNCTIONS
    [ContextMenu("Generate guid for ID")]
    private void GenerateGUID()
    {
        id = Guid.NewGuid().ToString().Substring(0, 8);;
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

    private void Start()
    {
        if (id == null || id == "")
        {
            Deb("Start(): CHECKPOINT NAMED " + gameObject.name + " DOESN'T HAVE AN ID! THIS MAY BE THE CAUSE OF FUTURE ISSUES!" +
                "\nTo give an ID to the GO all you need to do is to go over the script that manages the go, open the context menu right-clicking on the component name and select the relative voice (\"Generate guid for ID\")", DebMsgType.warn);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            inPlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            inPlace = false;
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
            InputManager.OnActionInput += StartAnimation;
        }
        else
        {
            OnCheckpoint -= CPCheck;
            DataManager.OnCPLoad -= CPCheck;
            InputManager.OnActionInput -= StartAnimation;
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

    private void StartAnimation(bool flag)
    {
        if(inPlace && flag)
        {
            OnCheckpoint?.Invoke(id);

            instance = Instantiate(animatedTxtPrefab, targetCanvas.transform);

            Destroy(instance, 2.75f);

            inPlace = false;
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
