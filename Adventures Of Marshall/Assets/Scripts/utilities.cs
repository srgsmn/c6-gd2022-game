//utilities.cs
/* This file collects the interfaces used in the project
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INTERFACES
public interface IDamageable
{
    void TakeDamage(int damage, Object instigator);
}

public interface IKillable
{
    void Kill();
}

// CLASSES
abstract class LifeManager : MonoBehaviour, IDamageable, IKillable
{
    private float health;
    private float healthMax;
    private float initialHealthMax;
    private float initialHealth;

    public void TakeDamage(int damage, Object instigator)
    {
        health -= damage;

        if (health <= 0f)
        {
            Kill();
        }
        //throw new System.NotImplementedException();
    }

    public void Kill()
    {
        //throw new System.NotImplementedException();
    }
}

// Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam