/*  CollectablesManager.cs
 * Manages player's collectables
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Va a sostituire ItemCollector.cs
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
    //FIELDS
    [SerializeField] private CollectablesManagerGUI collectablesGUI;

    private Dictionary<string, int> items = new Dictionary<string, int>();


    //METHODS
    private void Start()
    {
        items.Add("SL", 0);
        items.Add("CC", 0);

        Debug.Log("CollectablesManager | Collectable items:");
        foreach (var item in items)
        {
            Debug.Log("(" + item.Key + ", " + item.Value + ")");
        }
    }

    private void Update()   //FIXME Debugging method
    {
        DebugInputs();
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "SugarLump":
                Debug.Log("CollectablesManager | Collision detected w/ SL. Incrementing the counter");
                items["SL"]++;
                Debug.Log("CollectablesManager | Communicating with the GUI to update the SL counter");
                //collectablesGUI.UpdateSL(items["SL"]);
                collectablesGUI.UpdateCounter(CollectablesManagerGUI.IndicatorType.SL, items["SL"]);
                break;

            case "ChocoChip":
                Debug.Log("CollectablesManager | Collision detected w/ CC. Incrementing the counter");
                items["CC"]++;
                Debug.Log("CollectablesManager | Communicating with the GUI to update the CC counter");
                //collectablesGUI.UpdateCC(items["CC"]);
                collectablesGUI.UpdateCounter(CollectablesManagerGUI.IndicatorType.CC, items["CC"]);
                break;
        }

        Destroy(collider.transform.parent.gameObject);
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            items["SL"]++;
            collectablesGUI.UpdateSL(items["SL"]);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            items["CC"]++;
            collectablesGUI.UpdateCC(items["CC"]);
        }
    }

    public void SetSL(int value){ items["SL"] = value; }
    public int GetSL() { return items["SL"]; }

    public void SetCC(int value) { items["CC"] = value; }
    public int GetCC() { return items["CC"]; }
}
