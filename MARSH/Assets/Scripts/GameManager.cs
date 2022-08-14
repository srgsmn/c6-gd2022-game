/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.CullingGroup;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentState;
    private GameScreen currentScreen;

    private Stack<GameScreen> previousScreens;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        previousScreens = new Stack<GameScreen>(5);

        EventSubscriber();
    }

    private void Start()
    {
        currentState = GameState.Play;
        currentScreen = GameScreen.PlayScreen;

        OnNewState?.Invoke(currentState);
    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    /// <summary>
    /// Pause game function accessible from UI and more
    /// </summary>
    /// <param name="pause"></param>
    public void PauseGame()
    {
        DisplayScreen(GameScreen.PauseMenu);
    }

    /// <summary>
    /// Resume game function accessible from UI and more
    /// </summary>
    public void ResumeGame()
    {
        Deb("ResumeGame(): Display PlayScreen");
        DisplayScreen(GameScreen.PlayScreen);
    }

    public void OpenStore()
    {
        Deb("OpenStore(): Display Store menu");
        DisplayScreen(GameScreen.StoreMenu);
    }

    /// <summary>
    /// Manages time flow in the game
    /// </summary>
    /// <param name="flag">Whether time should be stopped or not</param>
    private void Freeze(bool flag = true)
    {
        if (flag)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// Changes the state of the game
    /// </summary>
    /// <param name="state">Which new state we want to set</param>
    private void NewState(GameState state)
    {
        currentState = state;

        OnNewState?.Invoke(currentState);
    }

    /// <summary>
    /// Displays the next sceen
    /// </summary>
    /// <param name="next">Which screen should be shown next</param>
    private void DisplayScreen(GameScreen next)
    {
        Deb("DisplayScreen() #####");
        Deb("DisplayScreen(): current state: " + currentState + ", current screen: " + currentScreen + ", next screen: " + next);

        bool flag = false;

        switch (currentScreen)
        {
            case GameScreen.StartMenu:
                //TODO
                break;

            case GameScreen.PlayScreen:

                if(next == GameScreen.PauseMenu || next == GameScreen.StoreMenu)
                {
                    Deb("DisplayScreen(): from play screen you're going into an acceptable screen. Changing here current state and setting true the flag to manage the screen and state");

                    currentState = GameState.Pause;
                    flag = true;
                }

                if(next == GameScreen.GameOver)
                {
                    currentState = GameState.GameOver;
                    flag = true;
                }

                break;

            case GameScreen.PauseMenu:
            case GameScreen.StoreMenu:

                if (next == GameScreen.PlayScreen)
                {
                    currentState = GameState.Play;
                    flag = true;
                }

                break;
        }

        if (flag)
        {
            Deb("DisplayScreen(): flag is true => updating previous screens stack, current scene, and sending event on new screen and new state");

            previousScreens.Push(currentScreen);
            currentScreen = next;

            Deb("DisplayScreen(): now current state is " + currentState + ", current screen is " + currentScreen);


            OnNewState?.Invoke(currentState);
            OnNewScreen?.Invoke(currentScreen);
        }
    }

    private void NavigateBack()
    {
        //TODO
        // fai la pop ma non la push
    }

    IEnumerator ShowingGameOver(float time)
    {
        Deb("ShowingGameOver(): Showuing game over animation");

        yield return new WaitForSeconds(time);

        Cursor.visible = true;

        Deb("ShowingGameOver(): Animation should be done and cursor should be visible");
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void NewStateEvent(GameState state);
    public static NewStateEvent OnNewState;
    public delegate void NewScreenEvent(GameScreen screen);
    public static NewScreenEvent OnNewScreen;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // UI
            InputManager.OnPause += OnPause;
            InputManager.OnBack += OnBack;

            OnNewState += OnStateChanged;

            MCHealthController.OnDeath += OnGameOver;
        }
        else
        {
            // UI
            InputManager.OnPause -= OnPause;
            InputManager.OnBack -= OnBack;

            OnNewState -= OnStateChanged;

            MCHealthController.OnDeath -= OnGameOver;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnPause()
    {
        Deb("OnPause(): #####");

        switch (currentScreen)
        {
            case GameScreen.PlayScreen:
                DisplayScreen(GameScreen.PauseMenu);

                break;

            case GameScreen.PauseMenu:
            case GameScreen.StoreMenu:
                DisplayScreen(GameScreen.PlayScreen);

                break;

            case GameScreen.SettingsMenu:
            case GameScreen.CreditsMenu:

                if (currentState == GameState.Pause)
                {
                    DisplayScreen(GameScreen.PlayScreen);
                }
                else if (currentState == GameState.Start)
                {
                    DisplayScreen(GameScreen.StartMenu);
                }

                break;
        }
    }

    private void OnBack()
    {
        switch (currentScreen)
        {
            case GameScreen.PauseMenu:
                DisplayScreen(GameScreen.PlayScreen);
                NewState(GameState.Play);
                previousScreens.Pop();

                break;

            case GameScreen.StoreMenu:
                DisplayScreen(GameScreen.PlayScreen);
                NewState(GameState.Play);
                previousScreens.Pop();

                break;
        }
    }

    private void OnStateChanged(GameState state)
    {
        Deb("OnStateChanged(): game state has changed, checking for cursor visibility");
        switch (state)
        {
            case GameState.Play:
                Deb("OnStateChanged(): cursor should NOT be visible now");

                Freeze(false);
                Cursor.visible = false;

                break;

            case GameState.GameOver:
                Deb("OnStateChanged(): cursor should be visible after all gameover option are shown.");

                StartCoroutine(ShowingGameOver(Consts.TIME_TO_CURSOR));

                break;

            default:
                Deb("OnStateChanged(): cursor should be visible now");

                Freeze();
                Cursor.visible = true;

                break;
        }
    }

    private void OnGameOver()
    {
        DisplayScreen(GameScreen.GameOver);
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
