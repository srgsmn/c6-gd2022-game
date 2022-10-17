/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public SettingsData settingsData;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        EventSubscriber();

        settingsData = new SettingsData();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void CameraSettsUpdate(SettingsOption option);
    public static CameraSettsUpdate OnCameraSettsUpdate;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            SettingsMenu.OnSettingsChanged += OnSettingsChanged;
        }
        else
        {
            SettingsMenu.OnSettingsChanged -= OnSettingsChanged;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnSettingsChanged(SettingsOption option, object value)
    {
        switch (option)
        {
            case SettingsOption.invertXAxis:
                Deb("InvertXAxis is " + settingsData.invertXAxis);
                settingsData.invertXAxis = (bool)value;
                Deb("Setting InvertXAxis to " + settingsData.invertXAxis);

                OnCameraSettsUpdate?.Invoke(SettingsOption.invertXAxis);

                //currentGameData.settings.invertXAxis = (bool)value;

                break;

            case SettingsOption.invertYAxis:
                Deb("InvertYAxis is " + settingsData.invertYAxis);
                settingsData.invertYAxis = (bool)value;
                Deb("Setting InvertYAxis to " + settingsData.invertYAxis);

                OnCameraSettsUpdate?.Invoke(SettingsOption.invertYAxis);

                //currentGameData.settings.invertYAxis = (bool)value;

                break;

            case SettingsOption.mouseSensitivity:
                Deb("MouseSensitivity is " + settingsData.mouseSensitivity);
                settingsData.mouseSensitivity = (float)value;
                Deb("Setting MouseSensitivity to " + settingsData.mouseSensitivity);

                OnCameraSettsUpdate?.Invoke(SettingsOption.mouseSensitivity);

                //currentGameData.settings.mouseSensitivity = (float)value;

                break;
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
