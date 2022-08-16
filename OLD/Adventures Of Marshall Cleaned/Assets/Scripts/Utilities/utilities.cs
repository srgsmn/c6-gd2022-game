//utilities.cs
/* This file collects the interfaces used in the project
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
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

public class Currency
{
    int sl; //SugarLumps
    int cc; //ChocoChips

    public void SetSL(int value) { sl = value; }
    public int GetSL() { return sl; }
    public void SetCC(int value) { cc = value; }
    public int GetCC() { return cc; }
}

public enum DeathType
    {
        Generic,
        CliffFall,
        EnemyCollision,
        Drowning
    }