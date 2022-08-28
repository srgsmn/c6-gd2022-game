/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{

    CinemachineFreeLook CFL;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();

        CFL = GetComponent<CinemachineFreeLook>();
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
            SettingsMenu.OnSettingsChanged += ChangeValue;

        }
        else
        {
            SettingsMenu.OnSettingsChanged -= ChangeValue;

        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ChangeValue(SettingsValue value)
    {
        if (CFL != null)
        {
            switch (value)
            {
                case SettingsValue.invertXAxis:
                    CFL.m_XAxis.m_InvertInput = !CFL.m_XAxis.m_InvertInput;

                    break;

                case SettingsValue.invertYAxis:
                    CFL.m_YAxis.m_InvertInput = !CFL.m_YAxis.m_InvertInput;

                    break;

                case SettingsValue.mouseSensitivity:
                    float sensitivityValue = DataManager.Instance.settingsData.mouseSensitivity;

                    CFL.m_YAxis.m_MaxSpeed = sensitivityValue * 1f;
                    CFL.m_XAxis.m_MaxSpeed = sensitivityValue * 100f;

                    break;
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
