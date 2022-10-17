/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - Trigger from here sh the gate opening
 *      - Also see Generic Canvas pop ups
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class Gate : MonoBehaviour
{

    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [Header("Key properties")]
    [SerializeField] private string keyID;
    public bool keyCollected = false;
    [Header("State:")]
    [SerializeField]
    [ReadOnlyInspector] private bool isOpen = false;
    private Animator animator;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS


    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    //public delegate void NameEvent();
    //public static NameEvent OnName;


    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            Collectable.OnCollection += OnCollection;
            MCCollisionManager.OnOpenGate += OnOpen;
        }
        else
        {
            Collectable.OnCollection -= OnCollection;
            MCCollisionManager.OnOpenGate -= OnOpen;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnCollection(CollectableType parameter, string id)
    {
        if (parameter == CollectableType.Key && keyID==id)
        {
            keyCollected = true;
        }
    }

    private void OnOpen()
    {
        isOpen = true;
        animator.SetTrigger("opencancello");
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
