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

public class PlayerHealthManager : HealthManager
{
    [SerializeField] private PlayerHealthGUI statusBars;

    //public string DeathType { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerHealth | Starting PlayerHealth component");
        currHealth = maxHealth;
        statusBars.gameObject.SetActive(true);
        statusBars.SetMaxHealthValue(maxHealth);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            isAlive = true;
        }
    }

    public override void BuildArmor(float maxValue)
    {
        Debug.Log("PlayerHealth | Building the armor");
        base.BuildArmor(maxValue);

        statusBars.SetMaxArmorValue(maxValue, maxValue);
    }

    public override void AddHealth(float value)
    {
        Debug.Log("PlayerHealth | Adding health");
        base.AddHealth(value);

        statusBars.SetHealthValue(currHealth);
    }

    public override void AddArmor(float value)
    {
        Debug.Log("PlayerHealth | Adding armor");
        base.AddArmor(value);

        statusBars.SetArmorValue(currArmor);
    }

    public override void TakeDamage(float damage, Object instigator)
    {
        Debug.Log("PlayerHealth | Taking damage from " + instigator.name);
        base.TakeDamage(damage, instigator);
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("PlayerHealth | Taking damage");
        base.TakeDamage(damage);
        statusBars.SetHealthValue(currHealth);
        statusBars.SetArmorValue(currArmor);
    }

    public override void Die()
    {
        Debug.Log("PlayerHealth | Dying...");
        base.Die();
    }

    private void DebugInputs()  //FIXME Debugging method
    {
        if (Input.GetKey(KeyCode.Z))
            BuildArmor(100f);
        if (Input.GetKeyDown(KeyCode.X))
            TakeDamage(25f);
        if (Input.GetKey(KeyCode.C))
            AddHealth(25f);
        if (Input.GetKey(KeyCode.V))
            AddArmor(25f);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Die();
        }
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