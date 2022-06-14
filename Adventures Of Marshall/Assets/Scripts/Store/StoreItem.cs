/* StoreItem.cs
 * --------------
 * Basic menu actions
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Implementare check livello (salvato in game master) e attivazione voce
 *  
 * Ref:
 *  -
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour
{
    private TextMeshProUGUI nameTxtComponent, descTxtComponent, slTxtComponent, ccTxtComponent;
    private int itemID;

    private Button selfRef;

    private void Awake()
    {

        selfRef = GetComponent<Button>();
        selfRef.onClick.AddListener(BuyItem);

        nameTxtComponent = transform.Find("FirstColumn").transform.Find("Name").GetComponent<TextMeshProUGUI>();
        descTxtComponent = transform.Find("FirstColumn").transform.Find("Description").GetComponent<TextMeshProUGUI>();
        slTxtComponent = transform.Find("SecondColumn").transform.Find("SL").GetComponent<TextMeshProUGUI>();
        ccTxtComponent = transform.Find("SecondColumn").transform.Find("CC").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {

    }

    public void SetID(int id)
    {
        itemID = id;
    }

    public void SetName(string name)
    {
        nameTxtComponent.text = name;
    }

    public void SetDescription(string description)
    {
        descTxtComponent.text = description;
    }

    public void SetSL(int sl)
    {
        slTxtComponent.text = "SL: " + sl;
    }

    public void SetCC(int cc)
    {
        ccTxtComponent.text = "CC: " + cc;
    }

    public void SetValues(string name, string description, int sl, int cc)
    {
        SetID(0);
        SetName(name);
        SetDescription(description);
        SetSL(sl);
        SetCC(cc);
    }

    public void SetValues(StoreData.Item item)
    {
        SetID(item.GetID());
        SetName(item.GetName());
        SetDescription(item.GetDescription());
        SetSL(item.GetPriceSL());
        SetCC(item.GetPriceCC());

        //if (item.GetAvailability() >= GameManager.GetCurrentLevel())
        //int currentLevel = GameManager.level;
        int currentLevel = 1;
        if (item.GetAvailability() > currentLevel)
        {
            selfRef.interactable = false;
        }
        else
        {
            selfRef.interactable = true;
        }
    }

    public void SetValues(int id)
    {
        StoreData.Item item = StoreData.items[id];
        SetID(id);
        SetName(item.GetName());
        SetDescription(item.GetDescription());
        SetSL(item.GetPriceSL());
        SetCC(item.GetPriceCC());
    }

    private void BuyItem() //TODO
    {
        DEB("Buying the item (id: " + itemID + ")");

    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
