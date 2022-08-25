/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - ALL
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class PauseMenu : Menu
{
    public void SettingsBtn()
    {
        GameManager.Instance.DisplayScreen(GameScreen.SettingsMenu);
    }
}
