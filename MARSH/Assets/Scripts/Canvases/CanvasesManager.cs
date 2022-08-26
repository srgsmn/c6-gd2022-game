/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 *  TODO:
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using Unity.VisualScripting;
using UnityEngine;
//using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class CanvasesManager : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [Header("Canvases prefab:")]
    [SerializeField] private GameObject backgroundCanvasP;
    [SerializeField] private GameObject startMenuCanvasP;
    [SerializeField] private GameObject HUDCanvasP;
    [SerializeField] private GameObject pauseMenuCanvasP;
    [SerializeField] private GameObject storeMenuCanvasP;
    [SerializeField] private GameObject settingsMenuCanvasP;
    [SerializeField] private GameObject creditsCanvasP;
    [SerializeField] private GameObject gameoverCanvasP;
    [SerializeField] private GameObject debugCanvasP;
    [SerializeField] private GameObject tutorialCanvasP;

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
    [SerializeField][ReadOnlyInspector] private GameObject tutorialInstance;

    [Header("SFX:")]
    [SerializeField] private AudioSource selectSound;

    [Header("Current values:")]
    [SerializeField][ReadOnlyInspector] private GameState currentState;
    [SerializeField][ReadOnlyInspector] private GameScreen currentScreen;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        gameObject.name += "_" + GameManager.Instance.GetSceneIndex();

        EventSubscriber();
    }

    private void Start()
    {
        OnNewState(GameManager.Instance.currentState);
    }

    private void Update()
    {
        //Deb("Update(): current state: " + currentState + "; current screen: " + currentScreen);
        if(currentScreen==GameScreen.PlayScreen && !HUDInstance.activeSelf)
        {
            // FIXME: I've used this method even if is not the best because I can't understand why without it and while prepearing the scene it doesn't show the HUD properly
            HUDInstance.SetActive(true);
        }
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

            GameManager.OnStartTutorial += DisplayTutorial;

            MenuButton.OnButtonClicked += OnButtonClicked;
        }
        else
        {
            GameManager.OnDebugModeChanged -= SetDebugMode;

            GameManager.OnNewScreen -= DisplayCanvas;
            GameManager.OnNewState -= OnNewState;

            GameManager.OnStartTutorial -= DisplayTutorial;

            MenuButton.OnButtonClicked -= OnButtonClicked;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void DisplayTutorial()
    {
        Deb("DISPLAYING TUTORIAL");
        tutorialInstance.SetActive(true);
    }

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
                if (settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(false);
                if (creditsInstance.activeSelf) creditsInstance.SetActive(false);

                Deb("DisplayCanvas(): Now displaying specifically HUD (instance is " + (HUDInstance.activeSelf ? "" : "NOT " ) + "active)");
                if (!HUDInstance.activeSelf) HUDInstance.SetActive(true);
                Deb("DisplayCanvas(): Now HUD instance is " + (HUDInstance.activeSelf ? "" : "NOT ") + "active");

                Deb("DisplayCanvas(): Current canvas status: pause canvas = " + pauseMenuInstance.activeSelf + " (expected false), store canvas = " + storeMenuInstance.activeSelf + " (expected false), HUD canvas = " + HUDInstance.activeSelf + " (expected true)");

                Deb("DisplayCanvas(): Basing on Game Manager tutorial is " + (GameManager.Instance.isTutorialOn ? "ON" : "OFF"));
                if (GameManager.Instance.isTutorialOn)
                {
                    Deb("DisplayCanvas(): Setting tutorial canvas to true");
                    tutorialInstance.SetActive(true);
                }
                else
                {
                    Deb("DisplayCanvas(): Setting tutorial canvas to false");
                    tutorialInstance.SetActive(false);
                }

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

                if (startMenuInstance != null && startMenuInstance.activeSelf) startMenuInstance.SetActive(false);
                if (pauseMenuInstance != null && pauseMenuInstance.activeSelf) pauseMenuInstance.SetActive(false);
                if (HUDInstance != null && HUDInstance.activeSelf) HUDInstance.SetActive(false);
                if (creditsInstance != null && creditsInstance.activeSelf) creditsInstance.SetActive(false);

                if (!settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.CreditsMenu:
                if (!backgroundInstance.activeSelf) backgroundInstance.SetActive(true);

                if (startMenuInstance != null && startMenuInstance.activeSelf) startMenuInstance.SetActive(false);
                if (HUDInstance != null && HUDInstance.activeSelf) HUDInstance.SetActive(false);
                if (settingsMenuInstance != null && settingsMenuInstance.activeSelf) settingsMenuInstance.SetActive(false);

                if (!creditsInstance.activeSelf) creditsInstance.SetActive(true);

                Cursor.visible = true;

                break;

            case GameScreen.GameOver:
                if (HUDInstance.activeSelf) HUDInstance.SetActive(false);
                if (backgroundInstance.activeSelf) backgroundInstance.SetActive(false);

                if (!gameoverInstance.activeSelf) gameoverInstance.SetActive(true);

                break;
        }

        Deb("DisplayCanvas(): changing current screen to " + screen);
        currentScreen = screen;
        Deb("DisplayCanvas(): now current screen is " + currentScreen);
    }

    private void OnButtonClicked()
    {
        if (selectSound != null) selectSound.Play();
    }

    private void OnNewState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                if (backgroundInstance == null) backgroundInstance = Instantiate(backgroundCanvasP, transform);
                //backgroundInstance.SetActive(true);

                if (HUDInstance != null) Destroy(HUDInstance);
                if (pauseMenuInstance != null) Destroy(pauseMenuInstance);
                if (storeMenuInstance != null) Destroy(storeMenuInstance);
                if (gameoverInstance != null) Destroy(gameoverInstance);
                if (debugInstance != null) Destroy(debugInstance);

                if (startMenuInstance == null) startMenuInstance = Instantiate(startMenuCanvasP, transform);
                //startMenuInstance.SetActive(true);

                if (settingsMenuInstance == null) settingsMenuInstance = Instantiate(settingsMenuCanvasP, transform);
                settingsMenuInstance.SetActive(false);

                if (creditsInstance == null) creditsInstance = Instantiate(creditsCanvasP, transform);
                creditsInstance.SetActive(false);

                break;

            case GameState.Play:
            case GameState.Pause:
                if (HUDInstance == null) HUDInstance = Instantiate(HUDCanvasP, transform);
                HUDInstance.SetActive(false);

                if (debugInstance == null) debugInstance = Instantiate(debugCanvasP, transform);
                debugInstance.SetActive(GameManager.Instance.isDebugMode);

                if (tutorialInstance == null) tutorialInstance = Instantiate(tutorialCanvasP, transform);
                tutorialInstance.SetActive(GameManager.Instance.isTutorialOn);

                if (backgroundInstance == null) backgroundInstance = Instantiate(backgroundCanvasP, transform);
                backgroundInstance.SetActive(false);

                if (pauseMenuInstance == null) pauseMenuInstance = Instantiate(pauseMenuCanvasP, transform);
                pauseMenuInstance.SetActive(false);

                if (storeMenuInstance == null) storeMenuInstance = Instantiate(storeMenuCanvasP, transform);
                storeMenuInstance.SetActive(false);

                if (settingsMenuInstance == null) settingsMenuInstance = Instantiate(settingsMenuCanvasP, transform);
                settingsMenuInstance.SetActive(false);

                if (creditsInstance == null) creditsInstance = Instantiate(creditsCanvasP, transform);
                creditsInstance.SetActive(false);

                if (startMenuInstance != null) Destroy(startMenuInstance);
                if (gameoverInstance != null) Destroy(gameoverInstance);

                break;

            case GameState.GameOver:
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

                break;
        }

        currentState = state;
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
                Debug.Log(this.GetType().Name + " > " + msg + "\n{GameObject info: Name: " + gameObject.name + ", tag: " + gameObject.tag + "}");

                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg + "\n{GameObject info: Name: " + gameObject.name + ", tag: " + gameObject.tag + "}");

                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg + "\n{GameObject info: Name: " + gameObject.name + ", tag: " + gameObject.tag + "}");


                break;
        }
    }
}
