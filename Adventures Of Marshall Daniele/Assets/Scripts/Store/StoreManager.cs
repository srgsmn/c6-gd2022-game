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
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject itemsTargetArea;

    private GameObject[] items = new GameObject[StoreData.items.Length];
    private GameObject scrollObj;
    private Transform parent;

    private void Awake()
    {
        DEB("Awaking the component");

        DEB("Setting the parent from Awake");
        parent = transform.Find("Scrollable").transform.Find("Panel").transform;
    }

    private void Start()
    {
        DEB("Starting looping on data");
        for(int i=0; i<StoreData.items.Length; i++)
        {
            DEB("Instantiation #" + i);

            items[i] = Instantiate(itemUI, parent);

            items[i].transform.Find("Button").GetComponent<StoreItem>().SetValues(StoreData.items[i]);
        }

        DEB("You should see " + StoreData.items.Length + " elements in store list");

        DEB(StoreData.ItemsToString());
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
