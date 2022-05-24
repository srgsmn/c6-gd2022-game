/*  CollectablesManagerGUI.cs
 * GUI manager for collectables
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * REF:
 *  - 
 *  
 *  Debug.Log("CollectablesManagerGUI | ");
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManagerGUI : MonoBehaviour
{
    [SerializeField] private Text SLText;
    [SerializeField] private Text CCText;

    private void Start()
    {
        if (SLText == null)
        {
            Debug.Log("CollectablesManagerGUI | No SL Text associated: retrieving it from children");
        }

        if (CCText == null)
        {
            Debug.Log("CollectablesManagerGUI | No CC Text slider associated: retrieving it from children");
            transform.GetChild(1);
        }
    }

    public void UpdateSL(int value)
    {
        SLText.text = "SL: " + value;
        Debug.Log("CollectablesManagerGUI | Updating SLText in " + SLText.text);
    }

    public void UpdateCC(int value)
    {
        CCText.text = "CC: " + value;
        Debug.Log("CollectablesManagerGUI | Updating CCText in " + CCText.text);
    }
}
