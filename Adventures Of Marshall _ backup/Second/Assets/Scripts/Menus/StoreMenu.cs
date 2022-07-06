using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MenuActions
{
    enum bakeType
    {
        SugarSyrup,
        ChocoLayer,
        SugarIcing
    }

    [SerializeField] private GameObject player;

    //public void Bake(string action, int cost,  float value)
    public void Bake(GameObject title)
    {
        int availability, cost;
        float value;
        string product = title.GetComponent<Text>().text.ToLower();

        switch (product)
        {
            case "sugarsyrup":
                availability = player.GetComponent<CollectablesManager>().GetSL();

                cost = 5;
                value = 20f;

                if (availability >= cost)
                {
                    bool done = player.GetComponent<PlayerLife>().AddHealth(value);
                    if (!done)
                        break;

                    player.GetComponent<CollectablesManager>().SetSL(availability - cost);
                }

                break;

            case "choco layer":
                Debug.Log("From shop menu: Baking a ChocoLayer");
                availability = player.GetComponent<CollectablesManager>().GetCC();
                Debug.Log("Checking availability: " + availability + " CCs");
                cost = 5;
                Debug.Log("Retriving cost: " + cost + " CCs");
                value = 80f;
                Debug.Log("Retriving how much it'll power up: " + value + " points");

                if (availability >= cost)
                {
                    Debug.Log("You collected enough CCs, now going to build the armor");
                    bool done = player.GetComponent<PlayerLife>().BuildArmor(value);
                    if (!done)
                        break;

                    player.GetComponent<CollectablesManager>().SetCC(availability - cost);

                }

                break;

            case "sugaricing":
                //player.BuildIcing(100f);      TODO
                break;
        }


    }
}
