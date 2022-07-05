/*  HealthManager.cs
 * Manages a character's health. Should be put in an empty grouping bars
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Rivedere se ha senso la variabile non obbligatoria di Set**Value()
 *  - Cambiare nome classe PlayerHealthGUI -> StatusBarsManager ?
 *  
 * REF:
 *  - 
 *  
 *  Debug.Log("PlayerHealthGUI | ");
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthGUI : MonoBehaviour    //FIXME
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private bool hasArmor = false;
    [SerializeField] private Slider armorSlider;
    [SerializeField] private Sprite[] endpoints;

    public enum BarType
    {
        health, armor
    }

    private class Bar
    {
        public Slider slider;
        public Image endpoint;

        public Bar(Slider slider, Image endpoint)
        {
            this.slider = slider;
            this.endpoint = endpoint;
        }
    }

    private Image biteImg;

    private Dictionary<BarType, Bar> bars = new Dictionary<BarType, Bar>();

    private void Awake()
    {
        DEB("Awaking: adding sliders to dictionary");
        bars.Add(BarType.health,
                new Bar(healthSlider, healthSlider.transform.Find("Handle Slide Area").transform.Find("Handle").GetComponent<Image>()));
        bars.Add(BarType.armor,
            new Bar(armorSlider, armorSlider.transform.Find("Handle Slide Area").transform.Find("Handle").GetComponent<Image>()));

        PlayerHealthController.OnHealthUpdate += UpdateHealth;
        PlayerHealthController.OnArmorUpdate += UpdateArmor;

        PlayerHealthController.OnMaxHealthUpdate += UpdateMaxHealth;
        PlayerHealthController.OnMaxArmorUpdate += UpdateMaxArmor;
    }

    private void OnDestroy()
    {
        PlayerHealthController.OnHealthUpdate -= UpdateHealth;
        PlayerHealthController.OnArmorUpdate -= UpdateArmor;

        PlayerHealthController.OnMaxHealthUpdate -= UpdateMaxHealth;
        PlayerHealthController.OnMaxArmorUpdate -= UpdateMaxArmor;
    }

    private void UpdateHealth(float value)
    {
        SetValue(BarType.health, value);
    }

    private void UpdateArmor(float value)
    {
        SetValue(BarType.armor, value);
    }

    private void UpdateMaxHealth(float value)
    {
        SetMaxValue(BarType.health, value);
    }

    private void UpdateMaxArmor(float value)
    {
        SetMaxValue(BarType.armor, value);
    }

    private void Start()
    {
        /*
        if (healthSlider == null)
        {
            Debug.Log("PlayerHealthGUI | No health slider associated: retrieving it from children");
            healthSlider = transform.GetChild(0);
        }
            
        if (armorSlider == null)
        {
            Debug.Log("PlayerHealthGUI | No armor slider associated: retrieving it from children");
            armorSlider = transform.GetChild(1);
        }
        */
        DEB("Setting hasArmor flag to FALSE");
        if (hasArmor == false)
        {
            Debug.Log("PlayerHealthGUI | Starting w/ no harmor: setting its slider value to 0");
            armorSlider.value = 0;
            armorSlider.maxValue = 0;
        }

        DEB("Checking both sliders");
        CheckEndpoint(BarType.health);
        CheckEndpoint(BarType.armor);
    }

    public bool SetMaxValue(BarType type, float maxValue)
    {
        DEB("SETTING MAX VALUE ("+type.ToString()+")");

        bars[type].slider.maxValue = maxValue;


        if (type == BarType.armor && hasArmor == false)
        {
            DEB("Is type armor: setting hasArmor flag to TRUE");
            hasArmor = true;
        }

        DEB("Checking endpoint");
        CheckEndpoint(type);

        if (bars[type].slider.maxValue < 0)
        {
            return false;
        }

        return true;
    }

    public bool SetValue(BarType type, float value)
    {
        bars[type].slider.value = value;

        if (type == BarType.armor && hasArmor == true && bars[type].slider.value == 0)
        {
            DEB("Is type armor: hasArmor = TRUE and value = 0 => disabling slider gameobject");
            bars[type].slider.gameObject.SetActive(false);
            hasArmor = false;
        }
        else if (type == BarType.armor && hasArmor == true && bars[type].slider.value != 0 && !bars[type].slider.gameObject.activeSelf)
        {
            DEB("Is type armor: hasArmor = TRUE and value > 0 ad activeSelf = false => activating ");
            bars[type].slider.gameObject.SetActive(true);
        }

        CheckEndpoint(type);

        if (bars[type].slider.value < 0)
        {
            return false;
        }

        return true;
    }

    /*
    public bool SetMaxHealthValue(float maxValue, float value)
    {
        Debug.Log("PlayerHealthGUI | Setting max health value ("+maxValue+") and current value ("+value+")");
        healthSlider.maxValue = maxValue;
        healthSlider.value = value;

        if (healthSlider.maxValue < 0)
            return false;

        return true;
    }

    public bool SetMaxHealthValue(float maxValue)
    {
        Debug.Log("PlayerHealthGUI | Setting max health value ("+maxValue+")");
        healthSlider.maxValue = maxValue;
        healthSlider.value = maxValue;

        if (healthSlider.maxValue < 0)
            return false;

        return true;
    }

    public bool SetMaxArmorValue(float maxValue, float value)
    {
        Debug.Log("PlayerHealthGUI | Setting max armor value ("+maxValue+") and current value ("+value+")");
        armorSlider.maxValue = maxValue;
        armorSlider.value = (float)value;

        if (hasArmor == false)
            hasArmor = true;

        if (hasArmor == true && armorSlider.value == 0)
            armorSlider.gameObject.SetActive(false);

        if (armorSlider.maxValue < 0)
            return false;

        return true;
    }

    public bool SetMaxArmorValue(float maxValue)
    {
        Debug.Log("PlayerHealthGUI | Setting max armor value ("+maxValue+")");
        armorSlider.maxValue = maxValue;
        armorSlider.value = maxValue;

        if (hasArmor == false)
            hasArmor = true;

        if (hasArmor == true && armorSlider.value == 0)    //WARN non necessario
            armorSlider.gameObject.SetActive(false);

        if (armorSlider.maxValue < 0)
            return false;

        return true;
    }

    public bool SetHealthValue(float value)
    {
        Debug.Log("PlayerHealthGUI | Setting health value to "+ value);
        healthSlider.value = value;
        UpdateBite();

        if (healthSlider.value < 0)
            return false;

        return true;
    }

    public bool SetArmorValue(float value)
    {
        Debug.Log("PlayerHealthGUI | Setting armor value to "+value);
        armorSlider.value = value;

        if(hasArmor==true && armorSlider.value==0)
            armorSlider.transform.parent.gameObject.SetActive(false);

        if (armorSlider.value < 0)
            return false;

        return true;
    }

    */

    private void CheckEndpoint(BarType type) {

        Debug.Log("Value: "+ bars[type].slider.value+" | Max Value: "+ bars[type].slider.maxValue);

        if (type == BarType.health && bars[type].slider.value == 0)
            return;

        if (bars[type].slider.value == bars[type].slider.maxValue ||
            (type == BarType.armor && bars[type].slider.value == 0))
        {
            Debug.Log("Max reached, bite should disappear");
            bars[type].endpoint.gameObject.SetActive(false);
        }
        else
        {
            bars[type].endpoint.gameObject.SetActive(true);
            bars[type].endpoint.sprite = endpoints[Random.Range(0, endpoints.Length)];
        }

        //if (type == BarType.armor && hasArmor == true && armorSlider.value == 0)
        //    armorSlider.gameObject.SetActive(false);
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
