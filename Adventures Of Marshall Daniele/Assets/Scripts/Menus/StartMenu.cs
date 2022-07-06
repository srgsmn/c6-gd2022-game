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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MenuActions
{
    private string DebIntro = "StartMenu.cs | ";

    [Header("Buttons")]
    [SerializeField]
    private GameObject btnsContainer;

    private GameObject startBtn, loadBtn, restartBtn, creditsBtn, exitBtn;

    #region DEBUG FIELDS
    [SerializeField]
    [Header("DEBUG VALUES")]
    [Tooltip("Flag for existing saved games")]
    [ReadOnlyInspector] private bool isSaved = false;
    #endregion

    private void Awake()
    {
        Debug.Log(DebIntro + "Awaking component");
        if (btnsContainer == null)
        {
            btnsContainer = transform.Find("Buttons").gameObject;
        }

        startBtn = btnsContainer.transform.Find("StartBtn").gameObject;
        loadBtn = btnsContainer.transform.Find("LoadBtn").gameObject;
        restartBtn = btnsContainer.transform.Find("RestartBtn").gameObject;
        creditsBtn = btnsContainer.transform.Find("CreditsBtn").gameObject;
        exitBtn = btnsContainer.transform.Find("ExitBtn").gameObject;

    }

    private void Start()
    {
        Debug.Log(DebIntro + "Starting component");
        //Check if there is a saved game
        isSaved = CheckSaved();

        startBtn.SetActive(!isSaved);
        loadBtn.SetActive(isSaved);
        restartBtn.SetActive(isSaved);
    }

    private bool CheckSaved()
    {
        return GameManager.CheckSaved();
    }

    public void StartGame()
    {
        Debug.Log(DebIntro + "Starting Game");
        //TODO Start game
    }

    public void RestartGame()
    {
        Debug.Log(DebIntro + "Restarting game");
        //TODO
    }

    public void LoadGame()
    {
        Debug.Log(DebIntro + "Loading game");
        //TODO
    }
}
