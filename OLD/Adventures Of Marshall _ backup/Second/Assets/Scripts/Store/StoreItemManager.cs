using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemManager : MonoBehaviour
{

    private enum ItemType
    {
        GlucoseSyrup,
        HotChocoMug,
        ChocoArmor,
        DarkChocoArmor,
        //SprinkledArmor,
        //RainbowArmor
    }

    [SerializeField] ItemType itemType = ItemType.GlucoseSyrup;
    
    void Start()
    {
        int i=0;

        switch (itemType)
        {
            case ItemType.GlucoseSyrup:
                i = 0;
                //BuildItem(StoreData.items[0], 0);
                break;

            case ItemType.HotChocoMug:
                i = 1;
                //BuildItem(StoreData.items[1], 1);
                break;

            case ItemType.ChocoArmor:
                i = 2;
                //BuildItem(StoreData.items[2], 2);
                break;

            case ItemType.DarkChocoArmor:
                i = 3;
                //BuildItem(StoreData.items[3], 3);
                break;
        }

        BuildItem(StoreData.items[i], i);
    }

    void Update()
    {
        
    }

    private void BuildItem(StoreData.Item item, int index)
    {
        Debug.Log("Store data item nema is: " + StoreData.items[index].GetName());
        transform.Find("Padding").transform.Find("Name").GetComponent<TextMeshProUGUI>().text = StoreData.items[index].GetName();
        transform.Find("Padding").transform.Find("Description").GetComponent<TextMeshProUGUI>().text = StoreData.items[index].GetDescription();
        transform.Find("Padding").transform.Find("Price").transform.Find("SL").GetComponent<TextMeshProUGUI>().text = "SL: " + StoreData.items[index].GetPriceSL();
        transform.Find("Padding").transform.Find("Price").transform.Find("CC").GetComponent<TextMeshProUGUI>().text = "CC: " + StoreData.items[index].GetPriceCC();
    }
}