/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class SkinController : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] bodyParts;

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
            MCHealthController.OnValueChanged += SetSkinColor;
        }
        else
        {
            MCHealthController.OnValueChanged -= SetSkinColor;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void SetSkinColor(ChParam parameter, object value)
    {
        if (parameter == ChParam.Armor)
        {
            foreach(MeshRenderer part in bodyParts)
            {
                Deb("## Color lerp value is "+ Color.Lerp(Consts.unarmored, Consts.armored, (float)value / 100) + " (with t="+ (float)value / 100 + ")");
                part.material.SetColor("_Color", Color.Lerp(Consts.unarmored, Consts.armored, (float)value/100));
            }
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
