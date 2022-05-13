//PlayerLife.cs
/* Manages player's life and event that can affect its health.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour, IDamageable
{
    [SerializeField] AudioSource fallingSound;
    [SerializeField] AudioSource enemyCollisionSound;
    [SerializeField] AudioSource drowningSound;
    [SerializeField] AudioSource restartSound;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;   //TODO: must hide then
    [SerializeField] private HealthBarManager healthBar;
    [SerializeField] private float maxArmor = 0f;
    [SerializeField] private float currentArmor;    //TODO; must hide then
    [SerializeField] private ArmorBarManager armorBar;

    private bool dead = false;
    private bool hasArmor = false;
    //private CharacterController controller;

    enum deathType
    {
        Generic,
        CliffFall,
        EnemyCollision,
        Drowning
    }

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        hasArmor = false;
    }

    void Update()
    {
        //DEBUG TODO
        if (Input.GetKey(KeyCode.B))
        {
            BuildArmor(80f);
        }

        if (transform.position.y <= -3.5f && !dead)
        {
            Die(deathType.CliffFall, "Falling from the ground");
            fallingSound.Play();
        }

        if (hasArmor && currentArmor <= 0f)
            hasArmor = false;
    }

    void LateUpdate()
    {
        if (currentHealth <= 0f)
        {
            Die(deathType.Generic, "You have run out of your health bar");
        }
    }

    public void BuildArmor(float maxValue)
    {
        hasArmor = true;
        currentArmor = maxValue;
        maxArmor = maxValue;
        armorBar.SetMaxValue(maxValue);
        armorBar.SetValue(maxValue);
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetValue(currentHealth);
    }

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

    void Die(deathType dType, string msg)
    {
        Debug.Log("Die method execution: disabling model mesh");
        GetComponentInChildren<MeshRenderer>().enabled = false;                           //TODO: far sparire gli occhietti (non così importante se già nel modello finale
        Debug.Log("Die method execution: disabling player controller");
        GetComponent<PlayerController>().enabled = false;

        Debug.Log("Player is dead: " + msg + "");
        dead = true;

        switch (dType)
        {
            case deathType.CliffFall:
                Debug.Log("Die method execution: switch case cliff fall");
                fallingSound.Play();
                break;

            case deathType.EnemyCollision:
                Debug.Log("Die method execution: switch case enemy collision");
                enemyCollisionSound.Play();
                break;

            case deathType.Drowning:
                Debug.Log("Die method execution: switch case drowning");
                drowningSound.Play();
                break;

            default:
                break;
        }

        Debug.Log("Die method execution: level reload invokation");
        Invoke(nameof(ReloadLevel), 1.5f);
    }

    void ReloadLevel()
    {
        Debug.Log("ReloadLevel execution");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartSound.Play();
    }
    
    public void TakeDamage(int damage, Object instigator)
    {
        currentHealth -= damage;

        healthBar.SetValue(currentHealth);
    }
    
    public void TakeDamage(int damage)
    {
        if (!hasArmor)
        {
            currentHealth -= (float)damage;

            healthBar.SetValue(currentHealth);
        }
        else
        {
            currentArmor -= damage / 2.5f;
            armorBar.SetValue(currentArmor);

            if (currentHealth >= currentArmor)
            {
                currentHealth -= damage / 5f;
                healthBar.SetValue(currentHealth);
            }
        }
    }
}
