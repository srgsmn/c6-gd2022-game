using UnityEngine;

[System.Serializable]
public class _GameData
{
    private int sl, cc, level;
    private float health, armor, maxHealth, maxArmor;
    private float[] position;

    public _GameData()
    {
        level = 0;
        sl = 0;
        cc = 0;
        health = 0;
        armor = 0;
        maxHealth = 0;
        maxArmor = 0;
        position = new float[3];
    }
}