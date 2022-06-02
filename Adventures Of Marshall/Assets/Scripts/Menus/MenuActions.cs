//MenuActions.cs
/* Collection of actions executable from menus
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

public class MenuActions : MonoBehaviour
{
    
    [SerializeField] private Canvas canvas;

    public void Open(GameObject window)
    {
        Debug.Log("Opening \"" + window.transform.name + "\" panel");
        window.SetActive(true);
    }

    public void Close(GameObject window)
    {
        Debug.Log("Closing \"" + window.transform.name + "\" panel");
        window.SetActive(false);
    }
    
    public void Close()
    {
        Debug.Log("Closing " + transform.name + " panel");
        transform.parent.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quitting...");
    }
}
