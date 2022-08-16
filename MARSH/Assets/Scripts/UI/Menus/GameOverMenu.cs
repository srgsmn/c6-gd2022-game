/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - Alert implementation
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{
    public void ReloadBtn()
    {
        ReloadGame();
    }

    private void ReloadGame()
    {
        GameManager.Instance.ReloadGame();
    }
}
