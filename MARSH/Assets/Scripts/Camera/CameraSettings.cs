/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{

    private CinemachineFreeLook CFL;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();

        CFL = GetComponent<CinemachineFreeLook>();
    }

    private void Start()
    {
        CFL.m_XAxis.m_InvertInput = CameraManager.Instance.settingsData.invertXAxis;
        CFL.m_YAxis.m_InvertInput = CameraManager.Instance.settingsData.invertYAxis;

        float sensitivityValue = CameraManager.Instance.settingsData.mouseSensitivity;

        CFL.m_YAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 1f;
        CFL.m_XAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 100f;

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
            CameraManager.OnCameraSettsUpdate += UpdateValues;
        }
        else
        {
            CameraManager.OnCameraSettsUpdate -= UpdateValues;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void UpdateValues(SettingsOption option)
    {
        if (CFL != null)
        {
            switch (option)
            {
                case SettingsOption.invertXAxis:
                    CFL.m_XAxis.m_InvertInput = CameraManager.Instance.settingsData.invertXAxis;

                    break;

                case SettingsOption.invertYAxis:
                    CFL.m_YAxis.m_InvertInput = CameraManager.Instance.settingsData.invertYAxis;

                    break;

                case SettingsOption.mouseSensitivity:
                    float sensitivityValue = CameraManager.Instance.settingsData.mouseSensitivity;

                    CFL.m_YAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 1f;
                    CFL.m_XAxis.m_MaxSpeed = Mathf.Pow(2f, sensitivityValue) * 100f;

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
