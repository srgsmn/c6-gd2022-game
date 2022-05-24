//utilities.cs
/* This file collects the interfaces used in the project
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INTERFACES
public interface IDamageable
{
    void TakeDamage(float damage, Object instigator);
    void TakeDamage(float damage);
}

public interface IKillable
{
    void Kill();
}

public interface IBarManageable
{
    void SetValue(float value);
    void SetMaxValue(float maxValue);
}

// ENUM
enum DeathType
{
    Generic,
    CliffFall,
    EnemyCollision,
    Drowning
}