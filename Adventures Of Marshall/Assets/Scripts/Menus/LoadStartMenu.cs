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

public class LoadStartMenu : StartMenu
{
    [SerializeField]
    [Tooltip("(DEBUG ONLY) Checkbox for enabling one screen or another")]
    private bool isFirstStart = true;

    [Header("Menu elements")]
    [SerializeField]
    [Tooltip("Menu that appears on first start")]
    private GameObject firstStartMenu;

    [SerializeField]
    [Tooltip("Menu that appears on NON-first starts")]
    private GameObject subsequentStartMenu;

    private void Awake()
    {
        //EnableFirst();

        if(SaveSystem.isSaved)
        {
            EnableSubsequents();
            isFirstStart = false;
        }
        else
        {
            EnableFirst();
            isFirstStart = true;
        }
    }

    private void Update()
    {
        if (isFirstStart)   EnableFirst();
        else                EnableSubsequents();
    }

    private void EnableFirst()
    {
        firstStartMenu.SetActive(true);
        subsequentStartMenu.SetActive(false);
    }

    private void EnableSubsequents()
    {
        firstStartMenu.SetActive(false);
        subsequentStartMenu.SetActive(true);
    }
}
