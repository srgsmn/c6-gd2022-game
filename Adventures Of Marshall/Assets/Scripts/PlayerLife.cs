//PlayerLife.cs
/* Manages player's life and event that can affect its health.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource fallingSound;
    [SerializeField] AudioSource enemyCollisionSound;
    [SerializeField] AudioSource drowningSound;
    [SerializeField] AudioSource restartSound;

    private bool dead = false;

    enum deathType
    {
        CliffFall,
        EnemyCollision,
        Drowning
    }

    void Update()
    {
        if (transform.position.y <= -3.5f && !dead)
        {
            Die(deathType.CliffFall, "Falling from the ground");
            fallingSound.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Body"))                       //Collision with enemy
        {
            Die(deathType.EnemyCollision, "Collision with enemy");
        }

        if(collision.gameObject.layer == 4)                                     //Collision with water layer
        {
            Die(deathType.Drowning, "Collision with water: you CAN'T swim at the moment");
        }

    }

    void Die(deathType dType, string msg)
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;                           //TODO: far sparire gli occhietti (non così importante se già nel modello finale
        GetComponent<PlayerController>().enabled = false;

        Debug.Log("Player is dead: " + msg + "");
        dead = true;

        switch (dType)
        {
            case deathType.CliffFall:
                fallingSound.Play();
                break;

            case deathType.EnemyCollision:
                enemyCollisionSound.Play();
                break;

            case deathType.Drowning:
                drowningSound.Play();
                break;

            default:
                break;
        }

        Invoke(nameof(ReloadLevel), 1.5f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartSound.Play();
    }
}
