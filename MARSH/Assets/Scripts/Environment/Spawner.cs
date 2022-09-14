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

    [SerializeField]
    [ReadOnlyInspector] private bool isNearby = false;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

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
        if (other.CompareTag("Player"))
        {
            isNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearby = false;
        }
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
        int go = 0;

        if (isRandom)
        {
            int i = Random.Range(1, totalQuantity);

            for(int a=0; a<totalQuantity; a++)
            {
                Deb("Instantiation of a " + rewards[go].prefab.name + " ("+ a +")");

                Generate(rewards[go].prefab, target);

                if (a == i - 1) go++;
            }
        }
        else
        {
            do
            {
                for (int a = 0; a < rewards[go].quantity; a++)
                    Deb("Instantiation of a " + rewards[go].prefab.name + " (" + a + ")");

                Generate(rewards[go].prefab, target);

                go++;
            } while (go < rewards.Length);
        }

        Destroy(gameObject);
    }

    private void Generate(GameObject prefab, GameObject target)
    {
        Deb("Generating object " + prefab.name);

        GameObject instance = Instantiate(prefab, target.transform.position + new Vector3(0f,5f,0f), transform.rotation);

        instance.GetComponent<Collectable>().NeedsID(false);
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
