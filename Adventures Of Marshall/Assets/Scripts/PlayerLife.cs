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
    [SerializeField] private float currentHealth;
    [SerializeField] private HealthBarManager healthBar;

    private bool dead = false;
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
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (transform.position.y <= -3.5f && !dead)
        {
            Die(deathType.CliffFall, "Falling from the ground");
            fallingSound.Play();
        }
    }

    void LateUpdate()
    {
        if (currentHealth <= 0f)
        {
            Die(deathType.Generic, "You have run out of your health bar");
        }
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

        healthBar.SetHealth(currentHealth);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= (float) damage;

        healthBar.SetHealth(currentHealth);
    }
}
