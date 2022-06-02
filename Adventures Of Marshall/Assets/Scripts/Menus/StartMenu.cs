/*  StartMenuActions.cs
 * Collection of actions executable from start menu
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Implementare RestartGame() con reset dei salvataggi.
 *  - Implementare LoadGame()
 *  
 * Ref:
 *  -
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MenuActions
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetSceneByName("Level01").buildIndex);
        //SceneManager.LoadScene("Level01");
    }

    public void RestartGame()   //TODO vedi sopra
    {
        StartGame();
    }

    public void LoadGame()
    {
        //TODO
    }

    
    public void QuitGame()
    {
        //mettere "you sure?"
        Quit();
    }
}
