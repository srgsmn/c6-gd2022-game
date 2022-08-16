/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class CanvasesManager : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [Header("Canvases prefab:")]
    [SerializeField] private GameObject backgroundCanvasP;
    [SerializeField] private GameObject startMenuCanvasP;
    [SerializeField] private GameObject HUDCanvasP;
    [SerializeField] private GameObject pauseMenuCanvasP;
    [SerializeField] private GameObject storeMenuCanvasP;
    //[SerializeField] private GameObject settingsMenuCanvasP;
    //[SerializeField] private GameObject creditsCanvasP;
    [SerializeField] private GameObject gameoverCanvasP;
    [SerializeField] private GameObject debugCanvasP;

    [Header("Canvases instances:")]
    [SerializeField][ReadOnlyInspector] private GameObject backgroundInstance;
    [SerializeField][ReadOnlyInspector] private GameObject startMenuInstance;
    [SerializeField][ReadOnlyInspector] private GameObject HUDInstance;
    [SerializeField][ReadOnlyInspector] private GameObject pauseMenuInstance;
    [SerializeField][ReadOnlyInspector] private GameObject storeMenuInstance;
    [SerializeField][ReadOnlyInspector] private GameObject settingsMenuInstance;
    [SerializeField][ReadOnlyInspector] private GameObject creditsInstance;
    [SerializeField][ReadOnlyInspector] private GameObject gameoverInstance;
    [SerializeField][ReadOnlyInspector] private GameObject debugInstance;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void Start()
    {
        OnNewState(GameManager.Instance.currentState);
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            GameManager.OnDebugModeChanged += SetDebugMode;

            GameManager.OnNewScreen += DisplayCanvas;
            GameManager.OnNewState += OnNewState;
        }
        else
        {
            GameManager.OnDebugModeChanged -= SetDebugMode;

            GameManager.OnNewScreen -= DisplayCanvas;
            GameManager.OnNewState -= OnNewState;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void DisplayCanvas(GameScreen screen)
    {
        switch (screen)
        {
            case GameScreen.StartMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(false);
                if (creditsInstance.activeSelf) creditsInstance.SetActive(false);

                if (!startMenuInstance.activeSelf) startMenuInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.PauseMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(false);
                if (creditsInstance.activeSelf) creditsInstance.SetActive(false);

                if (!pauseMenuInstance.activeSelf) pauseMenuInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.PlayScreen:
                Deb("DisplayCanvas(): Displaying PlayScreen...");

                if (backgroundInstance.activeSelf) backgroundInstance.SetActive(false);

                if (pauseMenuInstance.activeSelf) pauseMenuInstance.SetActive(false);
                if (storeMenuInstance.activeSelf) storeMenuInstance.SetActive(false);

                if (!HUDInstance.activeSelf) HUDInstance.SetActive(true);

                Deb("DisplayCanvas(): Current canvas status: pause canvas = " + pauseMenuInstance.activeSelf + " (expected false), store canvas = " + storeMenuInstance.activeSelf + " (expected false), HUD canvas = " + HUDInstance.activeSelf + " (expected true)");

                Cursor.visible = false;

                break;

            case GameScreen.StoreMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (HUDInstance.activeSelf) HUDInstance.SetActive(false);

                if (!storeMenuInstance.activeSelf) storeMenuInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.SettingsMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (HUDInstance.activeSelf) HUDInstance.SetActive(false);

                if (!settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.CreditsMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (HUDInstance.activeSelf) HUDInstance.SetActive(false);

                if (!creditsInstance.activeSelf) creditsInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.GameOver:
                if (HUDInstance.activeSelf) HUDInstance.SetActive(false);
                if (backgroundInstance.activeSelf) backgroundInstance.SetActive(false);

                if (!gameoverInstance.activeSelf) gameoverInstance.SetActive(true);

                break;
        }
    }

    private void OnNewState(GameState state)
    {
        if (state == GameState.Start)
        {
            if (backgroundInstance == null) backgroundInstance = Instantiate(backgroundCanvasP, transform);
            //backgroundInstance.SetActive(true);

            if (HUDInstance != null) Destroy(HUDInstance);
            if (pauseMenuInstance != null) Destroy(pauseMenuInstance);
            if (storeMenuInstance != null) Destroy(storeMenuInstance);
            if (gameoverInstance != null) Destroy(gameoverInstance);
            if (debugInstance != null) Destroy(debugInstance);

            if (startMenuInstance == null) startMenuInstance = Instantiate(startMenuCanvasP, transform);
            //startMenuInstance.SetActive(true);

            //if (settingsMenuInstance == null) settingsMenuInstance = Instantiate(settingsMenuCanvasP, transform);
            //settingsMenuInstance.SetActive(false);

            //if (creditsInstance != null) creditsInstance = Instantiate(creditsCanvasP, transform);
            //creditsInstance.SetActive(false);
        }
        else if(state == GameState.GameOver)
        {
            if (backgroundInstance != null) Destroy(backgroundInstance);
            if (startMenuInstance != null) Destroy(startMenuInstance);
            if (HUDInstance != null) Destroy(HUDInstance);
            if (pauseMenuInstance != null) Destroy(pauseMenuInstance);
            if (storeMenuInstance != null) Destroy(storeMenuInstance);
            if (settingsMenuInstance != null) Destroy(settingsMenuInstance);
            if (creditsInstance != null) Destroy(creditsInstance);
            if (debugInstance != null) Destroy(debugInstance);


            if (gameoverInstance == null) gameoverInstance = Instantiate(gameoverCanvasP, transform);
            //gameoverInstance.SetActive(true);
        }
        else
        {
            if (HUDInstance == null) HUDInstance = Instantiate(HUDCanvasP, transform);
            HUDInstance.SetActive(true);

            if (backgroundInstance == null) backgroundInstance = Instantiate(backgroundCanvasP, transform);

            if (state == GameState.Play) backgroundInstance.SetActive(false);
            if (state == GameState.Pause) backgroundInstance.SetActive(true);

            if (pauseMenuInstance == null) pauseMenuInstance = Instantiate(pauseMenuCanvasP, transform);
            pauseMenuInstance.SetActive(false);

            if (storeMenuInstance == null) storeMenuInstance = Instantiate(storeMenuCanvasP, transform);
            storeMenuInstance.SetActive(false);

            //if (settingsMenuInstance == null) settingsMenuInstance = Instantiate(settingsMenuCanvasP, transform);
            //settingsMenuInstance.SetActive(true);

            //if (creditsInstance == null) creditsInstance = Instantiate(creditsCanvasP, transform);
            //creditsInstance.SetActive(true);

            if (debugInstance == null) debugInstance = Instantiate(debugCanvasP, transform);
            debugInstance.SetActive(GameManager.Instance.isDebugMode);

            if (startMenuInstance != null) Destroy(startMenuInstance);
            if (gameoverInstance != null) Destroy(gameoverInstance);
        }
    }

    private void SetDebugMode(bool flag)
    {
        if (debugInstance != null) debugInstance.SetActive(flag);
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
