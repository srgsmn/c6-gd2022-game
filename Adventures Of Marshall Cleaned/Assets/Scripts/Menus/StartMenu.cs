//LoadStartMenu.cs
/* Checks if there are already saved games and basing on that it shows a starting menù intead of another
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  -
 *  
 * Ref:
 *  - 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MenuActions
{
    #region DEBUG FIELDS
    [SerializeField]
    [Tooltip("(DEBUG ONLY) Checkbox for enabling one screen or another")]
    private bool isFirstStart = true;
    #endregion

    [Header("Menu elements")]
    [SerializeField]
    [Tooltip("Menu that appears on first start, when there isn't any game saved")]
    private GameObject firstGamePanel;

    [SerializeField]
    [Tooltip("Menu that appears when there is alreary a game saved")]
    private GameObject existingGamePanel;

    [SerializeField]
    [Tooltip("Alert that appears when clicking on quit button")]
    private GameObject quitAlertPanel;

    private GameObject player;


    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (GameManager.loadedGame != null) //FIXME
        {
            EnableExistingGamePanel();
            //isFirstStart = false;
        }
        else
        {
            EnableFirstGamePanel();
            //isFirstStart = true;
        }
    }

    private void Update()
    {
        //if (isFirstStart)   EnableFirstGamePanel();
        //else                EnableExistingGamePanel();
    }

    private void EnableFirstGamePanel()
    {
        firstGamePanel.SetActive(true);
        existingGamePanel.SetActive(false);
    }

    private void EnableExistingGamePanel()
    {
        firstGamePanel.SetActive(false);
        existingGamePanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        //SaveSystem.DeleteSaved();
        StartGame();
    }

    public void LoadGame()
    {
        Debug.Log("StartMenu.cs | Loading the last game");
        //SaveSystem.LoadPlayer();
        Debug.Log("StartMenu.cs | Loading the last game scene");
        //SceneManager.LoadScene(player.GetComponent<Player>().level);
        GameManager.LoadGame();
    }
}
