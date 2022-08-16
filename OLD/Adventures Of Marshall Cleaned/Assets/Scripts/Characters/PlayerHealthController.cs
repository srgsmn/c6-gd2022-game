/*  PlayerHealth.cs
 * Manages the player's life
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Implementare SFX e Level Reload (in script diversi)
 *  
 * REF:
 *  - https://answers.unity.com/questions/225213/c-countdown-timer.html per modificare la morte dopo la caduta da un luogo
 */

using UnityEngine;

public class PlayerHealthController : HealthController
{
    //EVENTS
    public delegate void HealthUpdateEvent(float value);
    public static event HealthUpdateEvent OnHealthUpdate;

    public delegate void MaxHealthUpdateEvent(float value);
    public static event MaxHealthUpdateEvent OnMaxHealthUpdate;

    public delegate void ArmorUpdateEvent(float value);
    public static event ArmorUpdateEvent OnArmorUpdate;

    public delegate void MaxArmorUpdateEvent(float value);
    public static event MaxArmorUpdateEvent OnMaxArmorUpdate;


    private void Start()
    {
        DEB("Starting PlayerHealth component");

        currHealth = maxHealth;

        OnMaxHealthUpdate?.Invoke(maxHealth);
        OnHealthUpdate?.Invoke(currHealth);
    }

    private void Update()
    {
        DebugInputs();

        if (hasArmor && currArmor <= 0)
        {
            Debug.Log("PlayerHealth | Armor is active but its current level is <=0: deactivating armor");
            hasArmor = false;
        }
    }

    public override bool BuildArmor(float maxValue)
    {
        DEB("Building the armor ("+maxValue+")");
        base.BuildArmor(maxValue);
        DEB("Armor built to "+currArmor);

        DEB("Updating armor bar (maxValue and current Value)");
        OnMaxArmorUpdate?.Invoke(maxArmor);
        OnArmorUpdate?.Invoke(currArmor);

        return true;
    }

    public override bool AddHealth(float value)
    {
        DEB("Adding health");
        base.AddHealth(value);

        DEB("Updating health bar (incrementing)");
        OnHealthUpdate?.Invoke(currHealth);

        return true;
    }

    public override void AddArmor(float value)
    {
        DEB("Adding armor");
        base.AddArmor(value);

        DEB("Updating armor bar (incrementing)");
        OnArmorUpdate?.Invoke(currArmor);
    }

    public override void TakeDamage(float damage, Object instigator)
    {
        Debug.Log("PlayerHealth | Taking damage from " + instigator.name);
        base.TakeDamage(damage, instigator);

        OnArmorUpdate?.Invoke(currArmor);
        OnHealthUpdate?.Invoke(currHealth);
    }

    public override void TakeDamage(float damage)
    {
        DEB("Taking damage");
        base.TakeDamage(damage);

        DEB("Updating health and armor bars (decrementing)");
        OnHealthUpdate?.Invoke(currHealth);

        OnArmorUpdate?.Invoke(currArmor);
    }

    public override void Die()
    {
        Debug.Log("PlayerHealth | Dying...");
        base.Die();
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DEB("Building Armor");
            BuildArmor(100f);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DEB("Taking damage");
            TakeDamage(25f);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DEB("Adding health");
            AddHealth(25f);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DEB("Adding armor");
            AddArmor(25f);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DEB("OH NO! DEATH IS COMING");
            Die();
        }
    }

    public void LoadHealth(float health, float armor)
    {
        SetHeatlth(health);
        SetArmor(armor);
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}