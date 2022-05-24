//ItemCollector.cs
/* Manages the collection of a character.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - rimuovere [SerializedField] ai contatori
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //TODO rimuovere serializzazione di queste due variabili
    [SerializeField] private int cc_count = 0;
    [SerializeField] private int sl_count = 0;

    [Header("GUI:")]
    [SerializeField][Tooltip("GUI element for CC count")] Text chocoChipsText;
    [SerializeField][Tooltip("GUI element for SL count")] Text sl_text;

    [Header("SFX:")]
    [SerializeField] AudioSource slSound;
    [SerializeField] AudioSource ccSound;

    private enum collectableItem
    {
        SL,
        CC
    };

    private void OnTriggerEnter(Collider obj)
    {
        switch (obj.gameObject.tag)
        {
            case "ChocoChip":
                cc_count++;
                UpdateUI(collectableItem.CC, cc_count);

                Destroy(obj.transform.parent.gameObject);
                ccSound.Play();
                break;

            case "SugarLump":
                sl_count++;
                UpdateUI(collectableItem.SL, sl_count);

                //Destroy(obj.gameObject);
                Destroy(obj.transform.parent.gameObject);
                slSound.Play();
                break;
        }

        /*
        if (other.gameObject.CompareTag("ChocoChip"))
        {
            Destroy(other.gameObject);
            cc_count++;
        }

        if (other.gameObject.CompareTag("ChocoChip"))
        {
            Destroy(other.gameObject);
            cc_count++;
        }
        */
    }

    public int GetCC() { return cc_count; }
    public void SetCC(int value) {
        cc_count = value;
        UpdateUI(collectableItem.CC, cc_count);
    }
    public int GetSL() { return sl_count; }
    public void SetSL(int value) {
        sl_count = value;
        UpdateUI(collectableItem.SL, sl_count);
    }

    private void UpdateUI(collectableItem item, int quantity)
    {
        switch(item)
        {
            case collectableItem.SL:
                sl_text.text = "SugarLumps: " + quantity;
                Debug.Log("Changed UI: SL count");

                break;

            case collectableItem.CC:
                chocoChipsText.text = "ChocoChips: " + quantity;
                Debug.Log("Changed UI: CC count");

                break;

            default:
                break;
        }
    }
}
