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
    [SerializeField]
    [ReadOnlyInspector] private bool keyCollected = false;
    [Header("State:")]
    [SerializeField]
    [ReadOnlyInspector] private bool _isOpen = false;
    [Header("GUI:")]
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private GameObject collectedPanel;
    [SerializeField] private GameObject uncollectedPanel;
    [SerializeField]
    [ReadOnlyInspector] private GameObject panelInstance;


    public bool isOpen
    {
        private set
        {
            if(!value && !_isOpen)
            {
                Destroy(panelInstance);
            }
            _isOpen = value;
        }
        get
        {
            return _isOpen;
        }
    }

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if(collectedPanel==null || targetCanvas==null || uncollectedPanel == null || keyID == "")
        {
            Deb("Awake(): component parameters missing! Check the inspector and see what's missing", DebMsgType.err);
        }

        EventSubscriber();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            if (!keyCollected)
            {
                panelInstance = Instantiate(uncollectedPanel, targetCanvas.transform);
            }
            else
            {
                panelInstance = Instantiate(uncollectedPanel, targetCanvas.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            if (!keyCollected)
            {
                Destroy(panelInstance);
            }
            else
            {
                Destroy(panelInstance);
            }
        }
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
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
        }
        else
        {
            Collectable.OnCollection -= OnCollection;
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
