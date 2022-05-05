 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int cc_count = 0;
    [SerializeField] private int sl_count = 0;
    [SerializeField] Text cc_text;
    [SerializeField] Text sl_text;

    private void OnTriggerEnter(Collider obj)
    {
        switch (obj.gameObject.tag)
        {
            case "ChocoChip":
                cc_count++;
                cc_text.text = "ChocoChips: " + cc_count;
                Debug.Log("CC Collected");
                Destroy(obj.transform.parent.gameObject);
                break;

            case "SugarLump":
                sl_count++;
                sl_text.text = "SugarLumps: " + sl_count;
                Debug.Log("SL Collected");
                //Destroy(obj.gameObject);
                Destroy(obj.transform.parent.gameObject);
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
}
