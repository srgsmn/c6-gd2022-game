/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class KeyLocator : MonoBehaviour
{
    [SerializeField] private GameObject keyObject;
    [SerializeField] private string id;
    public Spawner[] spawnSpots;
    [SerializeField]
    [ReadOnlyInspector] private int index;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();

        keyObject.GetComponent<Collectable>().SetID(id);
    }

    private void Start()
    {
        if (spawnSpots.Length != 0)
        {
            //index = Random.Range(1, spawnSpots.Length);
            //spawnSpots[index-1].SetKey(keyObject);

            spawnSpots[1].SetKey(keyObject);
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

        }
        else
        {

        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS
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
