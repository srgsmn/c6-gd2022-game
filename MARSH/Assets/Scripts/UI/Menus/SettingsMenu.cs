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

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Start()
    {
        Deb("Start(): Debug mode is " + GameManager.Instance.isDebugMode + ". Changing toggle value to same value.");
        debugToggle.isOn = GameManager.Instance.isDebugMode;
        Deb("Start(): Toggle value is now " + debugToggle.isOn);

        debugToggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChangedOccured(debugToggle);
        });
    }

    private void OnDestroy()
    {
        debugToggle.onValueChanged.RemoveListener(delegate
        {
            ToggleValueChangedOccured(debugToggle);
        });
    }

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

    private void ToggleValueChangedOccured(Toggle tglValue)
    {
        GameManager.Instance.SwitchDebugMode(tglValue.isOn);
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
