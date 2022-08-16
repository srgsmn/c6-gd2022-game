/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Globals;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ButtonActionType buttonActionType;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI text;

    private ButtonFeature features;

    private void Start()
    {
        features = Dicts.ButtonFeatures(buttonActionType);

        text.color = features.defTxtColor;

        buttonImage.color = features.bgColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = features.hovTxtColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = features.defTxtColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("EventSystem")
            .GetComponent<UnityEngine.EventSystems.EventSystem>()
            .SetSelectedGameObject(null);
    }
}
