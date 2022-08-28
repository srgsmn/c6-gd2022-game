using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Globals;

public class DebugCanvas : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [Header("Current:")]
    public TextMeshProUGUI currPos;
    public TextMeshProUGUI currRot, currH, currMaxH, currA, currMaxA, currSL, currCC;

    [Header("Saved:")]
    public TextMeshProUGUI savPos;
    public TextMeshProUGUI savRot, savH, savMaxH, savA, savMaxA, savSL, savCC;

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
            // Debug inputs
            DataManager.OnValueUpdate += UpdateText;

            DataManager.OnSavedData += UpdateSavedData;
        }
        else
        {
            DataManager.OnValueUpdate -= UpdateText;

            DataManager.OnSavedData -= UpdateSavedData;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void UpdateSavedData(GameData data)
    {
        UpdateText(true, ChParam.Pos, data.player.position);
        UpdateText(true, ChParam.Rot, data.player.rotation);
        UpdateText(true, ChParam.Health, data.player.health);
        UpdateText(true, ChParam.MaxHealth, data.player.maxHealth);
        UpdateText(true, ChParam.Armor, data.player.armor);
        UpdateText(true, ChParam.MaxArmor, data.player.maxArmor);
        UpdateText(true, ChParam.SL, data.player.sl);
        UpdateText(true, ChParam.CC, data.player.cc);
    }

    private void UpdateText(bool saved, ChParam param, object value)
    {
        //Deb("UpdateText() ###");

        switch (param)
        {
            case ChParam.Pos:
                if (saved)
                    savPos.text = value.ToString();
                else
                    currPos.text = value.ToString();

                break;

            case ChParam.Rot:
                if (saved)
                    savRot.text = value.ToString();
                else
                    currRot.text = value.ToString();

                break;

            case ChParam.Health:
                if (saved)
                    savH.text = value.ToString();
                else
                    currH.text = value.ToString();

                break;

            case ChParam.MaxHealth:
                if (saved)
                    savMaxH.text = value.ToString();
                else
                    currMaxH.text = value.ToString();

                break;

            case ChParam.Armor:
                if (saved)
                    savA.text = value.ToString();
                else
                    currA.text = value.ToString();

                break;

            case ChParam.MaxArmor:
                if (saved)
                    savMaxA.text = value.ToString();
                else
                    currMaxA.text = value.ToString();

                break;

            case ChParam.SL:
                if (saved)
                    savSL.text = value.ToString();
                else
                    currSL.text = value.ToString();

                break;

            case ChParam.CC:
                if (saved)
                    savCC.text = value.ToString();
                else
                    currCC.text = value.ToString();

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
