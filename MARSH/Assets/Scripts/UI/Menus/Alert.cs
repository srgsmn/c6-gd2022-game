/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - ALL
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Globals;
using TMPro;

public class Alert : Menu, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AlertType alertType;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI cancelTxt;
    [SerializeField] private TextMeshProUGUI confirmTxt;

    private Color defaultBg;
    private AlertObject alertObject;

    private void Start()
    {
        alertObject = Dicts.AlertProperties(alertType);

        background.color = alertObject.background;
        defaultBg = background.color;

        title.text = alertObject.title;
        message.text = alertObject.message;
        cancelTxt.text = alertObject.cancelText;
        confirmTxt.text = alertObject.confirmText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CancelAction();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.color = new Color(1f, 1f, 1f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.color = defaultBg;
    }

    public void CancelAction()
    {
        Destroy(gameObject);
    }

    public void ConfirmAction()
    {
        switch (alertType)
        {
            case AlertType.Quit:
                QuitGame();

                break;
        }
    }
}
