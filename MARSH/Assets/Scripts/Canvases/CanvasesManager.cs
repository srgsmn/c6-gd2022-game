/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;

public class CanvasesManager : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject storeCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS


    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            GameManager.OnNewScreen += DisplayCanvas;
        }
        else
        {
            GameManager.OnNewScreen -= DisplayCanvas;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void DisplayCanvas(GameScreen screen)
    {
        switch (screen)
        {
            case GameScreen.PauseMenu:
                pauseCanvas.SetActive(true);

                break;

            case GameScreen.PlayScreen:
                if (pauseCanvas.activeSelf) pauseCanvas.SetActive(false);
                if (storeCanvas.activeSelf) storeCanvas.SetActive(false);
                if (!HUDCanvas.activeSelf) HUDCanvas.SetActive(true);

                break;

            case GameScreen.StoreMenu:
                if (HUDCanvas.activeSelf) HUDCanvas.SetActive(false);
                storeCanvas.SetActive(true);

                break;

            case GameScreen.GameOver:
                if (HUDCanvas.activeSelf) HUDCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);

                break;

        }
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebMsgType type = DebMsgType.log)
    {
        switch (type)
        {
            case DebMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);
                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
