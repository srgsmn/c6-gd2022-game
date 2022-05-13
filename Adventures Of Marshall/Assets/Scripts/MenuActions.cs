//MenuActions.cs
/* Collection of actions executable from menus
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
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
                cost = Globals.GetCostSL(product);
                value = Globals.GetValue(product);

                if (availability >= Globals.GetCostSL(product))
                {
                    player.GetComponent<PlayerLife>().AddHealth(value);
                    player.GetComponent<ItemCollector>().SetSL(availability - cost);
                }
                
                break;

            case "choco layer":
                Debug.Log("From shop menu: Baking a ChocoLayer");
                availability = player.GetComponent<ItemCollector>().GetCC();
                cost = Globals.GetCostCC(product);
                value = Globals.GetValue(product);

                if (availability >= Globals.GetCostCC(product))
                {
                    player.GetComponent<PlayerLife>().BuildArmor(value);
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
