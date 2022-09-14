/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEndpoint : MonoBehaviour
{
    private enum EP_Position { top, bottom }

    [SerializeField] private EP_Position position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(position == EP_Position.top)
            {
                transform.parent.GetComponent<Ladder>().topFlag = true;
            }

            if (position == EP_Position.bottom)
            {
                transform.parent.GetComponent<Ladder>().bottomFlag = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (position == EP_Position.top)
            {
                transform.parent.GetComponent<Ladder>().topFlag = false;
            }

            if (position == EP_Position.bottom)
            {
                transform.parent.GetComponent<Ladder>().bottomFlag = false;
            }
        }
    }
}
