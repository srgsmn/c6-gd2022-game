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

        if (activePanel != null)
        {
            HideIfClickedOutside(activePanel);
        }
    }

    private void HideIfClickedOutside(GameObject panel)
    {
        if(Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            Close(panel);
        }
    }

    public void Resume()
    {
        Close(pauseMenuUI);
        Close(quitAlertUI);
        Close(loadMenuAlertUI);
        Freeze(isPaused=false);
    }

    public void Pause()
    {
        Open(pauseMenuUI);
        Freeze(isPaused = true);
    }

    public void LoadMenu()
    {
        loadMenuAlertUI.SetActive(true);
        activePanel = loadMenuAlertUI;
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("00_StartMenu");
    }

    public void QuitGame()
    {
        quitAlertUI.SetActive(true);
        activePanel = quitAlertUI;
    }

    private void Freeze(bool flag)
    {
        if (flag)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }

        hud.SetActive(!flag);
    }


}
