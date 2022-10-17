/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Globals;

public class SettingsMenu : Menu
{
    [SerializeField] private Toggle debugToggle;
    [SerializeField] private Toggle invertXAxisToggle;
    [SerializeField] private Toggle invertYAxisToggle;
    [SerializeField] private GameObject mouseSensitivity;
    [SerializeField]
    [ReadOnlyInspector] private Slider mouseSlider;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        mouseSlider = mouseSensitivity.transform.GetComponentInChildren<Slider>();

        debugToggle.onValueChanged.AddListener(delegate
        {
            DebugToggleChanged(debugToggle);
        });

        invertYAxisToggle.onValueChanged.AddListener(delegate
        {
            InvertYAxisToggleChanged(invertYAxisToggle);
        });

        invertXAxisToggle.onValueChanged.AddListener(delegate
        {
            InvertXAxisToggleChanged(invertXAxisToggle);
        });

        mouseSlider.onValueChanged.AddListener(delegate
        {
            MouseSliderChanged(mouseSlider);
        });
    }

    private void Start()
    {
        Deb("Start(): Debug mode is " + GameManager.Instance.isDebugMode + ". Changing toggle value to same value.");
        debugToggle.isOn = GameManager.Instance.isDebugMode;
        Deb("Start(): Toggle value is now " + debugToggle.isOn);

        invertXAxisToggle.isOn = CameraManager.Instance.settingsData.invertXAxis;
        invertYAxisToggle.isOn = CameraManager.Instance.settingsData.invertYAxis;

        mouseSlider.value = CameraManager.Instance.settingsData.mouseSensitivity;
    }

    private void OnDestroy()
    {
        debugToggle.onValueChanged.RemoveListener(delegate
        {
            DebugToggleChanged(debugToggle);
        });

        invertYAxisToggle.onValueChanged.RemoveListener(delegate
        {
            InvertYAxisToggleChanged(invertYAxisToggle);
        });

        invertXAxisToggle.onValueChanged.RemoveListener(delegate
        {
            InvertXAxisToggleChanged(invertXAxisToggle);
        });

        mouseSlider.onValueChanged.RemoveListener(delegate
        {
            MouseSliderChanged(mouseSlider);
        });
    }

    public delegate void SettingsChangeEvent(SettingsOption option, object value);
    public static SettingsChangeEvent OnSettingsChanged;

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    public void TutorialBtn()
    {
        Deb("TutorialBtn(): Starting tutorial...");

        GameManager.Instance.StartTutorial();
    }

    public void CreditsBtn()
    {
        GameManager.Instance.DisplayScreen(GameScreen.CreditsMenu);
    }

    public void ResetBtn()
    {
        base.OpenAlert(AlertType.Reset);
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void DebugToggleChanged(Toggle tglValue)
    {
        GameManager.Instance.SwitchDebugMode(tglValue.isOn);
    }

    private void InvertYAxisToggleChanged(Toggle tglValue)
    {
        //DataManager.Instance.settingsData.invertYAxis = !DataManager.Instance.settingsData.invertYAxis;

        OnSettingsChanged?.Invoke(SettingsOption.invertYAxis, tglValue.isOn);
    }

    private void InvertXAxisToggleChanged(Toggle tglValue)
    {
        //DataManager.Instance.settingsData.invertXAxis = !DataManager.Instance.settingsData.invertXAxis;

        OnSettingsChanged?.Invoke(SettingsOption.invertXAxis, tglValue.isOn);
    }

    private void MouseSliderChanged(Slider slider)
    {
        //DataManager.Instance.settingsData.mouseSensitivity = slider.value;

        OnSettingsChanged?.Invoke(SettingsOption.mouseSensitivity, slider.value);
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
