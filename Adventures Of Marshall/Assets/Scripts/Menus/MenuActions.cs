/* MenuActions.cs
 * --------------
 * Basic menu actions
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 *  
 * Ref:
 *  -
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    
    [SerializeField] private Canvas canvas;

    public static bool isAlertOpen { get; set; }

    private GameObject activeWindow = null;

    public void Open(GameObject window)
    {
        activeWindow = window;
        DEB("Opening \"" + window.transform.name + "\" panel");
        window.SetActive(true);
    }

    public void Close(GameObject window)
    {
        DEB("Closing \"" + window.transform.name + "\" panel");
        window.SetActive(false);
        activeWindow = null;
    }

    public void Close()
    {
        DEB("Closing \"" + activeWindow.transform.name + "\" panel");

        activeWindow.SetActive(false);
        activeWindow = null;
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadMenu()
    {
        DEB("Trying to open the menu");
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        DEB("Quitting...");
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
