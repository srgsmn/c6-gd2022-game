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

using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    //INSPECTOR'S VALUES
    [Header("Health values:")]
    [SerializeField] private protected float maxHealth = 100f;
    [SerializeField] [ReadOnlyInspector] private protected float currHealth;
    [SerializeField] private protected float maxArmor = 0f;
    [SerializeField] [ReadOnlyInspector] private protected float currArmor;

    private protected bool _isAlive;
    private protected bool hasArmor = false;

    private float damageFactor = 1f;

    public void ResetDamageFactor()
    {
        damageFactor = 0;
    }

    public void SetDamageFactor(float factor)
    {
        damageFactor = factor;
    }

    public bool isAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }
    public bool HasArmor() { return hasArmor; }
    public float GetHealth() { return currHealth; }
    public float GetArmor() { return currArmor; }
    public void SetHeatlth(float health) { currHealth = health; }
    public void SetArmor(float armor) { currArmor = armor; }
    public virtual void SetMaxHeatlth(float maxHealth) { this.maxArmor = maxHealth; }
    public virtual void SetMaxArmor(float maxArmor) { this.maxArmor = maxArmor; }


    private void Awake()
    {
        isAlive = true;
    }

    public virtual bool BuildArmor(float maxValue)
    {
        hasArmor = true;
        maxArmor = maxValue;
        AddArmor(maxArmor);

        return true;
    }

    public virtual bool AddHealth(float value)
    {
        currHealth += value;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        return true;
    }

    public virtual void AddArmor(float value)
    {
        if (currArmor >= 0 && hasArmor)
        {
            currArmor += value;
            if (currArmor > maxArmor)
            {
                currArmor = maxArmor;
            }
        }
    }

    public virtual void TakeDamage(float damage, Object instigator)
    {
        TakeDamage(damage * damageFactor);
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
        if (currArmor < 0)
        {
            currArmor = 0;
            hasArmor = false;
        }
    }

    public virtual void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }
}
