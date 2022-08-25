/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class StartMenu : Menu
{
    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS
    public void StartBtn()
    {
        GameManager.Instance.StartGame();
    }

    public void SettingsBtn()
    {
        GameManager.Instance.DisplayScreen(GameScreen.SettingsMenu);
    }
}
