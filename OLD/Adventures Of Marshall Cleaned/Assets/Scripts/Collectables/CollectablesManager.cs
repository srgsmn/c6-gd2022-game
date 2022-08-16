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
 */

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

    private Dictionary<Globals.CollectableType, int> items = new Dictionary<Globals.CollectableType, int>();

    //METHODS
    private void Awake()
    {
        items.Add(Globals.CollectableType.SL, 0);
        items.Add(Globals.CollectableType.CC, 0);

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

    public void IncrementItem(Globals.CollectableType key, Collider collider)
    {
        items[key]++;

        switch (key)
        {
            case Globals.CollectableType.SL:
                Debug.Log("CollectablesManager | Setting the event for SL Update");

                OnSLUpdate?.Invoke(items[Globals.CollectableType.SL]);

                DestroyCollectable(collider);
                break;

            case Globals.CollectableType.CC:
                Debug.Log("CollectablesManager | Setting the event for CC update");

                OnCCUpdate?.Invoke(items[Globals.CollectableType.CC]);

                DestroyCollectable(collider);
                break;
        }
    }

    private void DestroyCollectable(Collider collider)
    {
        Debug.Log("CollectablesManager | Destroying the collectable [" + collider.tag + "]");

        Destroy(collider.transform.parent.gameObject);
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            items[Globals.CollectableType.SL]++;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            items[Globals.CollectableType.CC]++;
        }
    }

    public void SetSL(int value){ items[Globals.CollectableType.SL] = value; }
    public int GetSL() { return items[Globals.CollectableType.SL]; }

    public void SetCC(int value) { items[Globals.CollectableType.CC] = value; }
    public int GetCC() { return items[Globals.CollectableType.CC]; }

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
