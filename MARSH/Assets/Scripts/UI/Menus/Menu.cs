/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 *      - Alert implementation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class Menu : MonoBehaviour
{
    //[SerializeField] private GameObject AlertPrefab;

    // BUTTONS
    public void CloseBtn()
    {
        ResumeGame();
    }

    public void QuitBtn()
    {
        GameManager.Instance.QuitGame();
        //OpenAlert();
    }

    public void ResumeBtn()
    {
        ResumeGame();
    }

    public void StartMenuBtn()
    {
        StartMenu();
    }

    // ACTIONS
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void StartMenu()
    {
        GameManager.Instance.ShowStartMenu();
    }

    /*
    private void OpenAlert()
    {
        Instantiate(AlertPrefab, transform.parent);
    }
    */
}
