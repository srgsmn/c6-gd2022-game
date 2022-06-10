/* Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * Ref:
 *  - https://www.youtube.com/watch?v=JivuXdrIHK0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MenuActions
{
    private bool isPaused = false;            //static perché vogliamo che altri script possano leggerlo senza poterne creare delle istanze, senza che possano referenziarlo

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject quitAlert;
    [SerializeField] private GameObject loadMenuAlert;

    public static bool? alertDecision = null;

    private enum AlertType
    {
        Quit, LoadMain
    }

    private Dictionary<AlertType, GameObject> alerts = new Dictionary<AlertType, GameObject>();

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        quitAlert.SetActive(false);
        loadMenuAlert.SetActive(false);

        alerts.Add(AlertType.Quit, quitAlert);
        alerts.Add(AlertType.LoadMain, loadMenuAlert);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DEB("ESC's been pressed");

            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        DEB("Resuming game");

        Close(pauseMenuUI);
        Close(quitAlert);
        Close(loadMenuAlert);
        Freeze(isPaused=false);
    }

    public void Pause()
    {
        DEB("Opening pause screen");

        Open(pauseMenuUI);
        Freeze(isPaused = true);
    }

    public void AskConfirm(GameObject alert)
    {
        DEB("Asking for confirm...");

        alertDecision = null;

        DEB("Retrieving the Key of the gameobject");

        AlertType? aType = GetKey(alert);

        if(aType != null)
        {
            switch ((AlertType)aType)
            {
                case AlertType.Quit:

                    DEB("Opening Quit Alert");

                    Open(quitAlert);

                    break;

                case AlertType.LoadMain:

                    DEB("Opening Loading Menu Alert");

                    Open(loadMenuAlert);

                    break;
            }
        }

        alertDecision = null ;
    }

    private AlertType? GetKey(GameObject value)
    {
        foreach(KeyValuePair<AlertType,GameObject> pair in alerts)
        {
            if (pair.Value == value) return pair.Key;
        }

        return null;
    }

    private void Freeze(bool flag)
    {
        if (flag)
        {
            DEB("Freezing the game");
            Time.timeScale = 0;
        }
        else
        {
            DEB("Unfreezing the game");
            Time.timeScale = 1f;
        }

        hud.SetActive(!flag);
        DEB("HUD set to "+hud.activeSelf);
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}

/* In Update()
        if (activePanel != null)
        {
            HideIfClickedOutside(activePanel);
        }
        */

/*
    private void HideIfClickedOutside(GameObject panel)
    {
        if(Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            Debug.Log("PauseMenu.cs | Clicked outside alert. Closing the panel");
            Close(panel);
        }
    }
    */