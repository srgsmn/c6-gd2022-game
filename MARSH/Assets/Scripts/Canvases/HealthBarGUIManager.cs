/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarGUIManager : MonoBehaviour
{
    private enum BarType
    {
        health, armor
    }

    private class Bar
    {
        public Slider slider;
        public Image endpoint;

        public Bar(Slider slider, Image endpoint) {
            this.slider = slider;
            this.endpoint = endpoint;
        }
    }

    private Dictionary<BarType, Bar> bars = new Dictionary<BarType, Bar>();


    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [Header("Sliders references:")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderHandle;
    [SerializeField][ReadOnlyInspector] private bool hasArmor = false;
    [SerializeField] private Slider armorSlider;
    [SerializeField] private Image armorSliderHandle;

    [Header("Custom endpoints:")]
    [SerializeField] private Sprite[] endpoints;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        bars.Add(BarType.health, new Bar(healthSlider, healthSliderHandle));
        bars.Add(BarType.armor, new Bar(armorSlider, armorSliderHandle));

        EventSubscriber();
    }

    private void Start()
    {

    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS
    private void SetValue(BarType type, float value, bool isMax = false)
    {
        Slider slider = bars[type].slider;

        if (isMax)
        {
            slider.maxValue = value;

            if (hasArmor == false) hasArmor = true;
        }
        else
        {
            slider.value = value;
        }

        if(type==BarType.armor && hasArmor == true && slider.value == 0)
        {
            bars[type].slider.gameObject.SetActive(false);
            hasArmor = false;
        }
        else
        {
            slider.gameObject.SetActive(true);
        }

        CheckEndpoint(type);
    }

    private void CheckEndpoint(BarType type)
    {
        Slider slider = bars[type].slider;
        Image endpoint = bars[type].endpoint;

        if (endpoints.Length != 0)
        {
            if(slider.value>0 && slider.value < slider.maxValue)
            {
                endpoint.gameObject.SetActive(true);
                endpoint.sprite = endpoints[Random.Range(0, endpoints.Length)];
            }
            else
            {
                endpoint.gameObject.SetActive(false);
            }
        }
    }


    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            MCHealthController.OnValueChanged += UpdateValue;
        }
        else
        {
            MCHealthController.OnValueChanged -= UpdateValue;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void UpdateValue(ChParam param, object value)
    {
        BarType? type = null;
        bool isMax = false;

        switch (param)
        {
            case ChParam.Health:
                Deb("UpdateValue(): a value has been updated (health)");

                type = BarType.health;

                break;

            case ChParam.MaxHealth:
                Deb("UpdateValue(): a value has been updated (maxHealth)");

                type = BarType.health;
                isMax = true;

                break;

            case ChParam.Armor:
                Deb("UpdateValue(): a value has been updated (armor)");

                type = BarType.armor;

                break;

            case ChParam.MaxArmor:
                Deb("UpdateValue(): a value has been updated (maxArmor)");

                type = BarType.armor;
                isMax = true;

                break;
        }

        if (type != null)
        {
            SetValue((BarType)type, (float)value, isMax);
        }
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebMsgType type = DebMsgType.log)
    {
        switch (type)
        {
            case DebMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
