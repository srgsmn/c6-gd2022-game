using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
    public int level;
    public float health;
    public float armor;
    public int SLCount;
    public int CCCount;

    private PlayerHealthManager playerHealth;
    private CollectablesManager collectablesManager;

    private void Awake()
    {
        playerHealth = gameObject.GetComponent<PlayerHealthManager>();
        collectablesManager = gameObject.GetComponent<CollectablesManager>();
    }

    private void Update()
    {
        health = playerHealth.GetHealth();
        armor = playerHealth.GetArmor();
        SLCount = collectablesManager.GetSL();
        CCCount = collectablesManager.GetCC();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;
        armor = data.armor;
        SLCount = data.SLCount;
        CCCount = data.CCCount;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
    */
}
