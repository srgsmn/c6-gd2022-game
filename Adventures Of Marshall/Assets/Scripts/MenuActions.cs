//MenuActions.cs
/* Collection of actions executable from menus
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Togliere magic numbers del negozio e mettere tutti i dati in un Globals o JSON (implementare serializzazione), al momento Globals non ha funzionato
 * Ref:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    [SerializeField] private GameObject player;

    enum bakeType
    {
        SugarSyrup,
        ChocoLayer,
        SugarIcing
    }

    //MAIN MENU
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //IN GAME MENU ACTIONS
    //public void Bake(string action, int cost,  float value)
    public void Bake(GameObject title)
    {
        int availability, cost;
        float value;
        string product = title.GetComponent<Text>().text.ToLower();

        switch (product)
        {
            case "sugarsyrup":
                availability = player.GetComponent<ItemCollector>().GetSL();

                cost = 5;
                value = 20f;

                if (availability >= cost)
                {
                    bool done = player.GetComponent<PlayerLife>().AddHealth(value);
                    if (!done)
                        break;

                    player.GetComponent<ItemCollector>().SetSL(availability - cost);
                }
                
                break;

            case "choco layer":
                Debug.Log("From shop menu: Baking a ChocoLayer");
                availability = player.GetComponent<ItemCollector>().GetCC();
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

                    player.GetComponent<ItemCollector>().SetCC(availability - cost);
                    
                }

                break;

            case "sugaricing":
                //player.BuildIcing(100f);      TODO
                break;
        }
        
        
    }

    public void Close(GameObject window)
    {
        window.SetActive(false);
    }
}
