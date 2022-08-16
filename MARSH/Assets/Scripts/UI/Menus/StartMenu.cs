/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - ALL
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : Menu
{
    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS
    public void StartBtn()
    {
        GameManager.Instance.StartGame();
    }
}
