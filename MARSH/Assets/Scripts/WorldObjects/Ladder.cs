/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject top, bottom;
    public bool topFlag, bottomFlag;
    [SerializeField]
    [ReadOnlyInspector] private bool isOn;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCCollisionManager.OnTeleport += Teleport;
        }
        else
        {
            MCCollisionManager.OnTeleport -= Teleport;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS
    private void Teleport(Transform transform)
    {
        Vector3 newPosition = Vector3.zero;

        if (bottomFlag) newPosition = top.transform.position;
        if (topFlag) newPosition = bottom.transform.position;

        transform.position = newPosition;
    }
}
