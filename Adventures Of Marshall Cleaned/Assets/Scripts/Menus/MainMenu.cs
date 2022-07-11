//MainMenu.cs
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
using UnityEngine.UI;
using TMPro;

public class MainMenu : MenuActions
{
    private string DebIntro = "StartMenu.cs | ";

    [Header("Appearance")]
    [SerializeField][Range(0f,1f)]
    private float lerpTime;
    [SerializeField]
    private Color[] colors;

    //private float t = 0f;
    //private int index = 0;

    [Header("Buttons")]
    [SerializeField]
    private GameObject btnsContainer;

    private GameObject startBtn, loadBtn, restartBtn, settingsBtn, creditsBtn, exitBtn;

    #region DEBUG FIELDS
    [SerializeField]
    [Header("DEBUG VALUES")]
    [Tooltip("Flag for existing saved games")]
    [ReadOnlyInspector] private bool isSaved = false;
    [SerializeField][ReadOnlyInspector] private float t=0f;
    [SerializeField][ReadOnlyInspector] private int currIndex = 0;
    [SerializeField][ReadOnlyInspector] private Color currColor = Color.white;
    #endregion

    private void Awake()
    {
        Debug.Log(DebIntro + "Awaking component");
        if (btnsContainer == null)
        {
            btnsContainer = transform.Find("Buttons").gameObject;
        }

        startBtn = btnsContainer.transform.Find("TopGroup").transform.Find("StartBtn").gameObject;
        loadBtn = btnsContainer.transform.Find("TopGroup").transform.Find("LoadBtn").gameObject;
        restartBtn = btnsContainer.transform.Find("TopGroup").transform.Find("RestartBtn").gameObject;
        settingsBtn = btnsContainer.transform.Find("BtmGroup").transform.Find("SettingsBtn").gameObject;
        creditsBtn = btnsContainer.transform.Find("BtmGroup").transform.Find("CreditsBtn").gameObject;
        exitBtn = btnsContainer.transform.Find("BtmGroup").transform.Find("ExitBtn").gameObject;
    }

    private void Start()
    {
        Debug.Log(DebIntro + "Starting component");
        //Check if there is a saved game
        isSaved = CheckSaved();

        startBtn.SetActive(!isSaved);
        loadBtn.SetActive(isSaved);
        restartBtn.SetActive(isSaved);
        GetComponent<Image>().color = colors[UnityEngine.Random.Range(0, colors.Length-1)];
        currColor = GetComponent<Image>().color;
    }

    private void Update()
    {
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, colors[currIndex], lerpTime*Time.deltaTime);
        currColor = GetComponent<Image>().color;

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        int prevIndex;

        if (t > .99f)
        {
            t = 0f;
            prevIndex = currIndex;

            do
            {
                currIndex = UnityEngine.Random.Range(0, colors.Length);
            } while (currIndex == prevIndex);
            
        }
    }

    private bool CheckSaved()
    {
        return GameManager.CheckSaved();
    }

    public void StartGame()
    {
        Debug.Log(DebIntro + "Starting Game");
        GameManager.StartGame(false);
        //TODO Start game
    }

    public void RestartGame()
    {
        Debug.Log(DebIntro + "Restarting game");
        GameManager.StartGame(false);
        //TODO
    }

    public void LoadGame()
    {
        Debug.Log(DebIntro + "Loading game");
        GameManager.StartGame(true);
        //TODO
    }
}
