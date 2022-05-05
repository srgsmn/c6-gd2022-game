using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour, IDamagable, IKillable
{
    [SerializeField] private int initialHealth = 100;
    [SerializeField] private int initialMaximumHealth = 100;
    [SerializeField] private UnityEvent onDie;

    private int health;
    private int maximumHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (onDie == null)
            onDie = new UnityEvent();

        health = initialHealth;
        maximumHealth = initialMaximumHealth;
    }

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
