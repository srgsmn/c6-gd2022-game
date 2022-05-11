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

// Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam