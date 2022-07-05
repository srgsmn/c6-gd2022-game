using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int sl, cc;
    public float health, maxHealth, armor, maxArmor;
    public Vector3 position;
    public Quaternion rotation;

    // Default values the game starts with when there's no data to load
    public GameData()
    {
        level = 1;
        sl = 0;
        cc = 0;
        position = Vector3.zero;
        rotation = Quaternion.identity;
        health = 0f;
        maxHealth = 0f;
        armor = 0f;
        maxArmor = 0;
    }
}
