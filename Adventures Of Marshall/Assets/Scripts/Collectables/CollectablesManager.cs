/*  CollectablesManager.cs
 * Manages player's collectables
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Va a sostituire ItemCollector.cs
 *  - Forse è meglio fare un CollisionManager che gestisca tutte le collisioni globali e richiami i vari moduli
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
    //EVENTS
    public delegate void SLUpdateEvent(int value);
    public static event SLUpdateEvent OnSLUpdate;

    public delegate void CCUpdateEvent(int value);
    public static event CCUpdateEvent OnCCUpdate;

    //FIELDS
    //[SerializeField] private CollectablesManagerGUI collectablesGUI;

    private Dictionary<string, int> items = new Dictionary<string, int>();


    //METHODS
    private void Start()
    {
        //if (collectablesGUI == null)
        //    Debug.LogError("You must link the collectable GUI first in the inspector");

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

                Debug.Log("CollectablesManager | Setting the event for SL Update");
                OnSLUpdate?.Invoke(items["SL"]);

                Debug.Log("CollectablesManager | Communicating with the GUI to update the SL counter");
                //collectablesGUI.UpdateSL(items["SL"]);
                //collectablesGUI.UpdateCounter(CollectablesManagerGUI.IndicatorType.SL, items["SL"]);

                DestroyCollectable(collider);
                break;

            case "ChocoChip":
                Debug.Log("CollectablesManager | Collision detected w/ CC. Incrementing the counter");
                items["CC"]++;

                Debug.Log("CollectablesManager | Setting the event for CC update");
                OnCCUpdate?.Invoke(items["CC"]);

                Debug.Log("CollectablesManager | Communicating with the GUI to update the CC counter");
                //collectablesGUI.UpdateCC(items["CC"]);
                //collectablesGUI.UpdateCounter(CollectablesManagerGUI.IndicatorType.CC, items["CC"]);

                DestroyCollectable(collider);
                break;
        }
    }

    private void DestroyCollectable(Collider collider)
    {
        Destroy(collider.transform.parent.gameObject);
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            items["SL"]++;
            //collectablesGUI.UpdateSL(items["SL"]);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            items["CC"]++;
            //collectablesGUI.UpdateCC(items["CC"]);
        }
    }

    public void SetSL(int value){ items["SL"] = value; }
    public int GetSL() { return items["SL"]; }

    public void SetCC(int value) { items["CC"] = value; }
    public int GetCC() { return items["CC"]; }

    public int Get(Globals.CollectableType cType)
    {
        switch (cType)
        {
            case Globals.CollectableType.CC:
                return GetCC();
            case Globals.CollectableType.SL:
                return GetSL();
            default:
                return 0;
        }
    }

    public void LoadCollectables(int sl, int cc)
    {
        SetSL(sl);
        SetCC(cc);
    }
}
