/*  CollectablesManager.cs
 * Manages player's collectables
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * REF:
 *  - 
 *  
 *  Debug.Log("CollectablesManager | ");
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] private CollectablesManagerGUI collectablesGUI;

    /*
    private class Item
    {
        public string Name { get; }
        public int Count { set; get; }

        public Item(string name)
        {
            this.Name = name;
            Count = 0;
        }

        public string getItem()
        {
            return "(" + Name + ", " + Count + ")";
        }
    }
    */

    //private List<Item> items = new List<Item>();
    Dictionary<string, int> items = new Dictionary<string, int>();

    private void Start()
    {
        items.Add("SugarLumps", 0);
        items.Add("ChocoChips", 0);

        Debug.Log("CollectablesManager | Collectable items:");
        foreach (var item in items)
        {
            Debug.Log("(" + item.Key + ", " + item.Value + ")");
        }
    }

    private void Update()
    {
        DebugInputs();
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "SugarLumps":
                items["SugarLumps"]++;
                collectablesGUI.UpdateSL(items["SugarLumps"]);
                break;

            case "ChocoChips":
                items["ChocoChips"]++;
                collectablesGUI.UpdateCC(items["ChocoChips"]);
                break;
        }
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            items["SugarLumps"]++;
            collectablesGUI.UpdateSL(items["SugarLumps"]);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            items["ChocoChips"]++;
            collectablesGUI.UpdateCC(items["ChocoChips"]);
        }
    }
}
