using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveDebugPanelManager : MonoBehaviour
{
    public enum InfoType
    {
        level, position, health, armor, maxHealth, maxArmor, sl, cc
    }

    private TextMeshProUGUI uLv, uPos, uH, uA, uMaxH, uMaxA, uSL, uCC, sLv, sPos, sH, sA, sMaxH, sMaxA, sSL, sCC;

    private Dictionary<InfoType, string> messages = new Dictionary<InfoType, string>();

    // Start is called before the first frame update
    void Awake()
    {
        //retrieving the text components
        uLv = transform.Find("U_Level").GetComponent<TextMeshProUGUI>();
        uPos = transform.Find("U_Position").GetComponent<TextMeshProUGUI>();
        uH = transform.Find("U_Health").GetComponent<TextMeshProUGUI>();
        uA = transform.Find("U_Armor").GetComponent<TextMeshProUGUI>();
        uMaxH = transform.Find("U_MaxHealth").GetComponent<TextMeshProUGUI>();
        uMaxA = transform.Find("U_MaxArmor").GetComponent<TextMeshProUGUI>();
        uSL = transform.Find("U_SL").GetComponent<TextMeshProUGUI>();
        uCC = transform.Find("U_CC").GetComponent<TextMeshProUGUI>();
        sLv = transform.Find("S_Level").GetComponent<TextMeshProUGUI>();
        sPos = transform.Find("S_Position").GetComponent<TextMeshProUGUI>();
        sH = transform.Find("S_Health").GetComponent<TextMeshProUGUI>();
        sA = transform.Find("S_Armor").GetComponent<TextMeshProUGUI>();
        sMaxH = transform.Find("S_MaxHealth").GetComponent<TextMeshProUGUI>();
        sMaxA = transform.Find("S_MaxArmor").GetComponent<TextMeshProUGUI>();
        sSL = transform.Find("S_SL").GetComponent<TextMeshProUGUI>();
        sCC = transform.Find("S_CC").GetComponent<TextMeshProUGUI>();

        //subscribing
        GameManager.OnIntDataChanged += UpdateIntValue;
        GameManager.OnFloatDataChanged += UpdateFloatValue;
        GameManager.OnVector3DataChanged += UpdateVector3Value;
    }

    private void OnDestroy()
    {
        //Unsubscribing
        GameManager.OnIntDataChanged -= UpdateIntValue;
        GameManager.OnFloatDataChanged -= UpdateFloatValue;
        GameManager.OnVector3DataChanged -= UpdateVector3Value;
    }

    private void BuildDictionary()
    {
        messages.Add(InfoType.level, "LEVEL: {0}");
        messages.Add(InfoType.position, "POSITION: {0}");
        messages.Add(InfoType.health, "HEALTH: {0}");
        messages.Add(InfoType.armor, "ARMOR: {0}");
        messages.Add(InfoType.sl, "SUGAR LUMPS: {0}");
        messages.Add(InfoType.cc, "CHOCO CHIPS: {0}");
        messages.Add(InfoType.maxHealth, "MAX HEALTH: {0}");
        messages.Add(InfoType.maxArmor, "MAX ARMOR: {0}");
    }

    private void Start()
    {
        BuildDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateIntValue(bool isSaved, InfoType infoType, int value)
    {
        switch (infoType)
        {
            case InfoType.level:
                if (isSaved)
                    sLv.text = string.Format(messages[InfoType.level], value);
                else
                    uLv.text = string.Format(messages[InfoType.level], value);

                break;

            case InfoType.sl:
                if (isSaved)
                    sSL.text = string.Format(messages[InfoType.sl], value);
                else
                    uSL.text = string.Format(messages[InfoType.sl], value);

                break;

            case InfoType.cc:
                if (isSaved)
                    sCC.text = string.Format(messages[InfoType.cc], value);
                else
                    uCC.text = string.Format(messages[InfoType.cc], value);

                break;
        }

    }

    private void UpdateFloatValue(bool isSaved, InfoType infoType, float value)
    {
        switch (infoType)
        {
            case InfoType.armor:
                if (isSaved)
                    sA.text = string.Format(messages[InfoType.armor], value);
                else
                    uA.text = string.Format(messages[InfoType.armor], value);

                break;

            case InfoType.health:
                if (isSaved)
                    sH.text = string.Format(messages[InfoType.health], value);
                else
                    uH.text = string.Format(messages[InfoType.health], value);

                break;

            case InfoType.maxArmor:
                if (isSaved)
                    sMaxA.text = string.Format(messages[InfoType.maxArmor], value);
                else
                    uMaxA.text = string.Format(messages[InfoType.maxArmor], value);

                break;

            case InfoType.maxHealth:
                if (isSaved)
                    sMaxH.text = string.Format(messages[InfoType.maxHealth], value);
                else
                    uMaxH.text = string.Format(messages[InfoType.maxHealth], value);

                break;
        }

    }

    private void UpdateVector3Value(bool isSaved, InfoType infoType, Vector3 value)
    {
        switch (infoType)
        {
            case InfoType.position:
                if (isSaved)
                    sPos.text = string.Format(messages[InfoType.position], value.ToString());
                else
                    uPos.text = string.Format(messages[InfoType.position], value.ToString());

                break;
        }

    }

}
