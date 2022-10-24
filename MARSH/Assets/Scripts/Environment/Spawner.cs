/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int totalQuantity;
    [SerializeField][Tooltip("If true, any value of quantity below will be ignored, if false, totalQuantity will be ignored")] private bool isRandom = false;
    [SerializeField] private Reward[] rewards;
    [SerializeField] private float radius = 1.5f;

    [SerializeField]
    [ReadOnlyInspector] private bool isNearby = false;

    [Header("Key:")]
    [SerializeField][ReadOnlyInspector] private bool hasKey;
    [SerializeField][ReadOnlyInspector] private GameObject keyObject;
    [SerializeField][ReadOnlyInspector] private string keyID;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    /*
    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearby = true;
            EventSubscriber(isNearby);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearby = false;
            EventSubscriber(isNearby);
        }
    }

    public void SetKey(GameObject obj)
    {
        hasKey = true;
        keyObject = obj;
        keyID = keyObject.GetComponent<Collectable>().GetID();
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCCollisionManager.OnSpawn += OnSpawn;
        }
        else
        {
            MCCollisionManager.OnSpawn -= OnSpawn;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnSpawn(GameObject target)
    {
        if (isNearby)
        {
            if (hasKey)
            {
                Generate(keyObject, target);
            }

            int go = 0;

            if (isRandom)
            {
                int i = Random.Range(0, totalQuantity);
                float angle = 360 / totalQuantity;

                Deb("### Random value is " + i + " => " + i + " of " + rewards[0].prefab.name + " and " + (totalQuantity - i) + " of " + rewards[1].prefab.name);

                for (int count = 0; count < totalQuantity; count++)
                {
                    if (count == i) go++;

                    Deb("OnSpawn(): Instantiation of a " + rewards[go].prefab.name + " (" + count + ")");

                    Generate(rewards[go].prefab, target, new Vector3(radius*Mathf.Cos(count*angle), 0, radius*Mathf.Sin(count*angle)));
                }
            }
            else
            {
                do
                {
                    for (int a = 0; a < rewards[go].quantity; a++)
                    {
                        Deb("OnSpawn(): Instantiation of a " + rewards[go].prefab.name + " (" + a + ")");

                        Generate(rewards[go].prefab, target);
                    }

                    go++;

                } while (go < rewards.Length);
            }

            Deb("OnSpawn(): Spawn completed, destroying spawner...");

            isNearby = false;
            Destroy(gameObject);
        }
    }

    private void Generate(GameObject prefab, GameObject target, object offset = null)
    {
        //Deb("Generating object " + prefab.name);
        GameObject instance = null;

        if (offset == null)
        {
            instance = Instantiate(prefab, target.transform.position + new Vector3(Random.Range(0f, .5f), 5f, Random.Range(0f, .5f)), Quaternion.identity);
        }
        else
        {
            instance = Instantiate(prefab, target.transform.position + (Vector3)offset, Quaternion.identity);

        }

        if (instance != null)
        {
            instance.GetComponent<Collectable>().NeedsID(false);
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
