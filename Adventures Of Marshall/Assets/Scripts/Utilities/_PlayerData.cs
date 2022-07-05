using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _PlayerData     //No MonoBehaviour perché non si comporterà come un componente
{
    public int level;
    public float health;
    public float armor;
    public int SLCount;
    public int CCCount;
    public float[] position = new float[3];

    public _PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        armor = player.armor;
        SLCount = player.SLCount;
        CCCount = player.CCCount;
        position[0] = player.position.x;
        position[1] = player.position.y;
        position[2] = player.position.z;
    }

    public override string ToString()
    {
        string result = "Saving following data:";
        result += "\nLV: " + level;
        result += "\t| HL: " + health;
        result += "\t| AL: " + armor;
        result += "\t| SL: " + SLCount;
        result += "\t| CC: " + CCCount;
        result += "\t| LP: [" + position[0];
        result += " ; " + position[1];
        result += " ; " + position[2] + "]";

        return result;
    }

    /*
    private GameMaster gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    public float health;
    public float armor;
    public int SLCount;
    public int CCCount;
    public float[] position;

    public PlayerData (Player player)
    {
        health = player.health;
        armor = player.armor;
        SLCount = player.SLCount;
        CCCount = player.CCCount;
        position = gameMaster.lastPositionToFloats();


    }
    */
}
