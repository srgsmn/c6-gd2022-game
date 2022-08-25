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
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool changeWithBg = true;
    [SerializeField][ReadOnlyInspector] private Color bgColor;

    private ButtonFeature features;
    
    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        if (buttonImage == null) buttonImage = GetComponent<Image>();

        EventSubscriber();
    }

    private void Update()
    {
        if (features != null && changeWithBg) features.hovTxtColor = bgColor;
    }

    private void Start()
    {
        features = Dicts.ButtonFeatures(buttonActionType);

        text.color = features.defTxtColor;

        buttonImage.color = features.bgColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.IsInteractable() && changeWithBg)
        {
            text.color = features.hovTxtColor;
        }
        if (!changeWithBg)
        {
            text.color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.IsInteractable())
        {
            text.color = features.defTxtColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("EventSystem")
            .GetComponent<UnityEngine.EventSystems.EventSystem>()
            .SetSelectedGameObject(null);

        text.color = features.defTxtColor;
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MenuBackground.OnNewBGColor += ChangeHovTxtColor;
        }
        else
        {
            MenuBackground.OnNewBGColor -= ChangeHovTxtColor;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void ChangeHovTxtColor(Color color)
    {
        bgColor = color;
    }
}
