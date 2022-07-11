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
        level = 0;
        sl = 0;
        cc = 0;
        position = Vector3.zero;
        rotation = Quaternion.identity;
        health = 100f;
        maxHealth = 100f;
        armor = 0f;
        maxArmor = 0;
    }

    public override string ToString()
    {
        return "Game data: {Level: "+level+";\nSL: "+sl+"; CC: "+cc+";\nPosition: "+position+";\nRotation: "+rotation+";\nHealth: "+health+"/"+maxHealth+";\nArmor: "+armor+"/"+maxArmor+";\n }";
    }
}
