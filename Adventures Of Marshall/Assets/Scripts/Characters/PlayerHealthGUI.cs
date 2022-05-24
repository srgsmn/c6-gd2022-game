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
        if (hasArmor == false)
        {
            Debug.Log("PlayerHealthGUI | Starting w/ no harmor: setting its slider value to 0");
            armorSlider.value = 0;
            armorSlider.maxValue = 0;
        }
    }

    private void Update()
    {
        //TODO
    }

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
}
