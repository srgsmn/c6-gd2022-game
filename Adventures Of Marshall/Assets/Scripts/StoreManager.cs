/* StoreManager.cs
 * --------------
 * Loads store data and manages purchases
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * Ref:
 *  -
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Button itemUI;
    [SerializeField] private GameObject itemsTargetArea;

    private List<Button> itemsList = new List<Button>();

    void Start()
    {
        DEB(StoreData.ItemsToString());
        //DisplayList();

        for(int i=0; i<=3; i++)
        {
            GameObject buff = itemsList.Add(gameObject.AddComponent<Button>()).Get;

            buff.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = StoreData.items[i].GetName();
            buff.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = StoreData.items[i].GetDescription();
            buff.transform.Find("SL").GetComponent<TextMeshProUGUI>().text = "SL: "+ StoreData.items[i].GetPriceSL();
            buff.transform.Find("CC").GetComponent<TextMeshProUGUI>().text = "CC: " + StoreData.items[i].GetPriceCC();

            itemsList.Add(buff);
        }
    }

    void Update()
    {
        
    }

    /*
    private void DisplayList()
    {
        DEB("Fetching data to display the item list");
        Button obj;
        GameObject subElements;

        foreach(StoreData.Item item in StoreData.items)
        {
            DEB("Got item from items: " + item.ToString());
            DEB("Instantiating new button...");
            obj = Instantiate(itemUI);

            obj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.GetName();
            obj.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.GetDescription();
            obj.transform.Find("SL").GetComponent<TextMeshProUGUI>().text = "SL: "+item.GetPriceSL();
            obj.transform.Find("CC").GetComponent<TextMeshProUGUI>().text = "CC: " + item.GetPriceCC();

            /*
            DEB("");
            subElements = obj.transform.GetChild(0).gameObject;

            subElements.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetName();
            subElements.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.GetDescription();

            subElements=subElements.transform.GetChild(2).gameObject;

            subElements.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SL: "+item.GetPriceSL();
            subElements.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "CC: "+item.GetPriceCC();
            *//*

            obj.transform.parent = itemsTargetArea.transform;
            itemsList.Add(obj);
        }
    }
    */

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
