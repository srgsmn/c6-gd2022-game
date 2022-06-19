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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : HealthController
{
    [Header("UI:")]
    [SerializeField] private PlayerHealthGUI statusBars;

    //public string DeathType { get; private set; }

    void Start()
    {
        Debug.Log("PlayerHealth | Starting PlayerHealth component");
        currHealth = maxHealth;
        statusBars.gameObject.SetActive(true);
        //statusBars.SetMaxHealthValue(maxHealth);
        statusBars.SetMaxValue(PlayerHealthGUI.BarType.health, maxHealth);
        //statusBars.SetHealthValue(currHealth);
        statusBars.SetValue(PlayerHealthGUI.BarType.health, currHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DebugInputs();

        if (hasArmor && currArmor <= 0)
        {
            Debug.Log("PlayerHealth | Armor is active but its current level is <=0: deactivating armor");
            hasArmor = false;
        }

        if (!isAlive)
        {
            Player player = GetComponent<Player>();
            player.LoadPlayer();
            if (!SaveSystem.isSaved)    //FIXME to refine with file try-catch
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(player.level);
            }
            
            isAlive = true;
        }
    }

    public override void BuildArmor(float maxValue)
    {
        DEB("Building the armor ("+maxValue+")");
        base.BuildArmor(maxValue);
        DEB("Armor built to "+currArmor);

        DEB("Updating armor bar (maxValue and current Value)");
        //statusBars.SetMaxArmorValue(maxValue, maxValue);
        statusBars.SetMaxValue(PlayerHealthGUI.BarType.armor, maxValue);
        statusBars.SetValue(PlayerHealthGUI.BarType.armor, maxValue);
    }

    public override void AddHealth(float value)
    {
        DEB("Adding health");
        base.AddHealth(value);

        DEB("Updating health bar (incrementing)");
        //statusBars.SetHealthValue(currHealth);
        statusBars.SetValue(PlayerHealthGUI.BarType.health, currHealth);
    }

    public override void AddArmor(float value)
    {
        DEB("Adding armor");
        base.AddArmor(value);

        //statusBars.SetArmorValue(currArmor);
        DEB("Updating armor bar (incrementing)");
        statusBars.SetValue(PlayerHealthGUI.BarType.armor, currArmor);
    }

    public override void TakeDamage(float damage, Object instigator)
    {
        Debug.Log("PlayerHealth | Taking damage from " + instigator.name);
        base.TakeDamage(damage, instigator);
    }

    public override void TakeDamage(float damage)
    {
        DEB("Taking damage");
        base.TakeDamage(damage);

        DEB("Updating health and armor bars (decrementing)");
        //statusBars.SetHealthValue(currHealth);
        //statusBars.SetArmorValue(currArmor);
        statusBars.SetValue(PlayerHealthGUI.BarType.health, currHealth);
        statusBars.SetValue(PlayerHealthGUI.BarType.armor, currArmor);
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

// NON UTILIZZATO MA PRESENTE IN PlayerLife.cs
/*
private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter Call: elaborating the collided object");

        if(collision.gameObject.CompareTag("Enemy Body"))                       //Collision with enemy
        {
            Debug.Log("OnCollisionEnter Call: you collided with Enemy Body");
            TakeDamage(25);
            //Die(deathType.EnemyCollision, "Collision with enemy");
        }

        if(collision.gameObject.layer == 4)                                     //Collision with water layer
        {
            Debug.Log("OnCollisionEnter Call: you collided with deep water");
            Die(deathType.Drowning, "Collision with water: you CAN'T swim at the moment");
        }

    }
*/