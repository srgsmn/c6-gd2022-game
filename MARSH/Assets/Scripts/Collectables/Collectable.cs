/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [SerializeField]
    [ReadOnlyInspector] private string id = null;
    [SerializeField] private CollectableType type;

    [Header("Model movement parameters:")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Vector3 spinSpeed = Vector3.one;
    [SerializeField] private Vector3 translationSpeed = Vector3.zero;

    private Vector3 positionOnStart;

    // CONTEXT MENU FUNCTIONS __________________________________________________ CONTEXT MENU FUNCTIONS

    [ContextMenu("Generate guid for ID")]
    private void GenerateGUID()
    {
        id = Guid.NewGuid().ToString().Substring(0,8);
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
        if (other.CompareTag("Player"))
        {
            OnCollection(type, id);
            Destroy(gameObject)
;        }
    }

    private void Start()
    {
        positionOnStart = targetObject.transform.localPosition;

        if (id == null || id == "")
        {
            Deb("Start(): COLLECTABLE NAMED " + gameObject.name + " DOESN'T HAVE AN ID! THIS MAY BE THE CAUSE OF FUTURE ISSUES!" +
                "\nTo give an ID to the GO all you need to do is to go over the script that manages the go, open the context menu right-clicking on the component name and select the relative voice (\"Generate guid for ID\")", DebMsgType.warn);
        }
        //transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-90f, +90f), Random.Range(-90f, +90f), Random.Range(-90f, +90f)));
    }

    private void Update()
    {
        targetObject.transform.Rotate(360 * spinSpeed.x * Time.deltaTime, 360 * spinSpeed.y * Time.deltaTime, 360 * spinSpeed.z * Time.deltaTime);
        /*transform.position = new Vector3(positionOnStart.x + .125f * Mathf.Sin(translationSpeed.x * Time.time),
                                         positionOnStart.y + .125f * Mathf.Sin(translationSpeed.y * Time.time),
                                         positionOnStart.z + .125f * Mathf.Sin(translationSpeed.z * Time.time));
        */
        targetObject.transform.localPosition = new Vector3(positionOnStart.x + .125f * Mathf.Sin(translationSpeed.x * Time.time),
                                         positionOnStart.y + .125f * Mathf.Sin(translationSpeed.y * Time.time),
                                         positionOnStart.z + .125f * Mathf.Sin(translationSpeed.z * Time.time));
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void CollectionEvent(CollectableType type, string id);
    public static CollectionEvent OnCollection;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            DataManager.OnCollectionLoad += CollectionCheck;
        }
        else
        {
            DataManager.OnCollectionLoad += CollectionCheck;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void CollectionCheck(List<string> ids)
    {
        Deb("CollectionCheck(): detected a new checkpoint. Comparing the IDs");

        if (ids.Contains(id))
            Destroy(gameObject);
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
