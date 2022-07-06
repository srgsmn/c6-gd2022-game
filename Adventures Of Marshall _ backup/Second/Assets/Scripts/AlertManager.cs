using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
//[ExecuteAlways] //se ho bisogno di usare funzioni che girino durante la play mode
public class AlertManager : MonoBehaviour
{
    [SerializeField] private string title = "<string> Title Here";
    [SerializeField] private string caption = "<string> Caption Here";
    [SerializeField] private string desc = "<string> Description Here";
    [SerializeField] private string lBtnTxt = "<string> Left button label Here";
    [SerializeField] private string rBtnTxt = "<string> Right button label Here";

    private GameObject titleObj;
    private GameObject captionObj;
    private GameObject descObj;
    private GameObject lBtn;
    private GameObject rBtn;

    private void Awake()
    {
        titleObj = transform.GetChild(0).gameObject;
        titleObj.GetComponent<TextMeshProUGUI>().text = title;

        captionObj = transform.GetChild(1).gameObject;
        captionObj.GetComponent<TextMeshProUGUI>().text = caption;

        descObj = transform.GetChild(2).gameObject;
        descObj.GetComponent<TextMeshProUGUI>().text = desc;

        lBtn = transform.GetChild(3).gameObject;
        lBtn.GetComponentInChildren<TextMeshProUGUI>().text = lBtnTxt;

        rBtn = transform.GetChild(4).gameObject;
        rBtn.GetComponentInChildren<TextMeshProUGUI>().text = rBtnTxt;
    }
}
