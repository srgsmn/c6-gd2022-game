/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject AlertPrefab;
    private GameObject alertInstance;

    private void Awake()
    {
        EventSubscriber();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // BUTTONS
    public void BackBtn()
    {
        GameManager.Instance.OnBack();
    }

    public void CloseBtn()
    {
        ResumeGame();
    }

    public void QuitBtn()
    {
        //GameManager.Instance.QuitGame();
        OpenAlert(AlertType.Quit);
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

    public void OpenAlert(AlertType type)
    {
        alertInstance = Instantiate(AlertPrefab, transform.parent);

        alertInstance.GetComponent<Alert>().SetAlertType(type);
    }

    public void CloseAlert()
    {
        if(alertInstance!=null)
            Destroy(alertInstance.gameObject);
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            GameManager.BeforeUnpause += CloseAlert;
        }
        else
        {
            GameManager.BeforeUnpause -= CloseAlert;
        }
    }
}
