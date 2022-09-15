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
            DataManager.OnDataSaved += LoadData;
        }
        else
        {
            SettingsMenu.OnSettingsChanged -= ChangeValue;
            DataManager.OnDataSaved -= LoadData;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ChangeValue(SettingsOption option, object value)
    {
        if (CFL != null)
        {
            switch (option)
            {
                case SettingsOption.invertXAxis:
                    CFL.m_XAxis.m_InvertInput = (bool)value;

                    break;

                case SettingsOption.invertYAxis:
                    CFL.m_YAxis.m_InvertInput = (bool)value;

                    break;

                case SettingsOption.mouseSensitivity:
                    float sensitivityValue = (float)value;

                    CFL.m_YAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 1f;
                    CFL.m_XAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 100f;

                    break;
            }
        }
    }

    private void LoadData(GameData data)
    {
        ChangeValue(SettingsOption.invertXAxis, data.settings.invertXAxis);
        ChangeValue(SettingsOption.invertYAxis, data.settings.invertYAxis);
        ChangeValue(SettingsOption.mouseSensitivity, data.settings.mouseSensitivity);
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
