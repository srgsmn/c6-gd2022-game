//EnemyBehaviour.cs
/* Manages health and behaviours of enemy-like objects.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private int initialHealth = 100;
    [SerializeField] private int initialMaximumHealth = 100;
    [SerializeField] private UnityEvent onDie;

    private int health;
    private int maximumHealth;

    void Start()
    {
        if (onDie == null)
            onDie = new UnityEvent();

        health = initialHealth;
        maximumHealth = initialMaximumHealth;
    }

    // Evaluating and managing damage
    public void TakeDamage(int damage, Object instigator)
    {
        int preDamage = health;
        health -= damage;

        Debug.Log("DAMAGE INFLICTED: -" + damage + " [From " + preDamage + " to " + health + "]");

        if (health <= 0)
        {
            Debug.Log(transform.name + "'s health below 0: DEATH TIME");
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        int preDamage = health;
        health -= damage;

        Debug.Log("DAMAGE INFLICTED: -" + damage + " [From " + preDamage + " to " + health + "]");

        if (health <= 0)
        {
            Debug.Log(transform.name + "'s health below 0: DEATH TIME");
            Die();
        }
    }

    public void Kill()
    {
        Debug.Log("KILLING " + transform.name);
        Die();
    }

    private void Die()
    {
        Debug.Log("DIE PROCESS BEGUN for " + transform.name);
        if (onDie != null)
        {
            Debug.Log("INVOKE CALL (Should spawn coins)");
            onDie.Invoke();

            Debug.Log("DESTROYING GAME OBJECT (" + transform.name + ")");
            Destroy(this.gameObject);
        }
    }
}
