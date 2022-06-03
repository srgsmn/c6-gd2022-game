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
using UnityEngine.SceneManagement;

public class PauseMenu : MenuActions
{
    public static bool isPaused = false;            //static perché vogliamo che altri script possano leggerlo senza poterne creare delle istanze, senza che possano referenziarlo

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject quitAlertUI;
    [SerializeField] private GameObject loadMenuAlertUI;

    private GameObject activePanel;

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        activePanel = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        /*
        if (activePanel != null)
        {
            HideIfClickedOutside(activePanel);
        }
        */
    }

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

    public void Resume()
    {
        Debug.Log("PauseMenu.cs | Resuming game");
        Close(pauseMenuUI);
        Close(quitAlertUI);
        Close(loadMenuAlertUI);
        Freeze(isPaused=false);
    }

    public void Pause()
    {
        Debug.Log("PauseMenu.cs | Opening pause screen");
        Open(pauseMenuUI);
        Freeze(isPaused = true);
    }

    public void LoadMenu()
    {
        Debug.Log("PauseMenu.cs | Trying to load menu");
        loadMenuAlertUI.SetActive(true);
        Debug.Log("PauseMenu.cs | Opening alert for loading menu");
        activePanel = loadMenuAlertUI;
    }

    public void OpenMenu()
    {
        Debug.Log("PauseMenu.cs | Trying to open the menu");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("PauseMenu.cs | Trying to quit the game");
        quitAlertUI.SetActive(true);
        Debug.Log("PauseMenu.cs | Opening alert for quitting");
        activePanel = quitAlertUI;
    }

    private void Freeze(bool flag)
    {
        if (flag)
        {
            Debug.Log("PauseMenu.cs | Freezing the game");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("PauseMenu.cs | Unfreezing the game");
            Time.timeScale = 1f;
        }

        hud.SetActive(!flag);
        Debug.Log("PauseMenu.cs | HUD set to "+hud.activeSelf);
    }


}
