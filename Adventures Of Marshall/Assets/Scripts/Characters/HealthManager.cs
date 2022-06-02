/*  HealthManager.cs
 * Manages a character's life
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Hide health and armor level values, now visible only for debug
 *  - Change reduced damage factor
 *  
 * REF:
 *  - https://answers.unity.com/questions/225213/c-countdown-timer.html per modificare la morte dopo la caduta da un luogo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    //INSPECTOR'S VALUES
    [Header("Health values:")]
    [SerializeField] private protected float maxHealth = 100f;
    [SerializeField] private protected float currHealth;   //FIXME: must hide then
    [SerializeField] private protected float maxArmor = 0f;
    [SerializeField] private protected float currArmor;    //FIXME; must hide then

    private protected bool _isAlive;
    private protected bool hasArmor = false;

    public bool isAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }
    public bool HasArmor() { return hasArmor; }
    public float GetHealth() { return currHealth; }
    public float GetArmor() { return currArmor; }

    private void Awake()
    {
        isAlive = true;
    }

    public virtual void BuildArmor(float maxValue)
    {
        hasArmor = true;
        maxArmor = maxValue;
        AddArmor(maxArmor);
    }

    public virtual void AddHealth(float value)
    {
        currHealth += value;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    public virtual void AddArmor(float value)
    {
        currArmor += value;
        if (currArmor > maxArmor)
        {
            currArmor = maxArmor;
        }
    }

    public virtual void TakeDamage(float damage, Object instigator)
    {
        TakeDamage(damage);
        Debug.Log(instigator.name + " damaged you of " + damage + " points");
    }

    public virtual void TakeDamage(float damage)
    {
        if (!hasArmor)
            currHealth -= damage;
        else
        {
            currArmor -= damage / 2.5f;

            if (currHealth >= currArmor)
                currHealth -= damage / 5f;   
        }

        if (currHealth < 0) currHealth = 0;
        if (currArmor < 0) currArmor = 0;
    }

    public virtual void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }
}
