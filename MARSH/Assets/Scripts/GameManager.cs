/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField][ReadOnlyInspector] private GameState _currentState;
    [SerializeField][ReadOnlyInspector] private GameScreen currentScreen;

    public GameState currentState
    {
        private set
        {
            _currentState = value;
        }
        get
        {
            return _currentState;
        }
    }

    //private Stack<GameScreen> previousScreens;

    [SerializeField][ReadOnlyInspector] private bool _isDebugMode = false;
    [SerializeField][ReadOnlyInspector] private bool _isTutorialOn = false;

    public bool isDebugMode
    {
        private set
        {
            _isDebugMode = value;
        }
        get
        {
            return _isDebugMode;
        }
    }

    public bool isTutorialOn
    {
        private set
        {
            _isTutorialOn = value;
        }
        get
        {
            return _isTutorialOn;
        }
    }

    public bool isTutorialDone = false;

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

        //previousScreens = new Stack<GameScreen>(Consts.SCREEN_HISTORY_LENGTH);

        EventSubscriber();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            currentState = GameState.Start;
            currentScreen = GameScreen.StartMenu;
        }
        else
        {
            currentState = GameState.Play;
            currentScreen = GameScreen.PlayScreen;
        }

        isDebugMode = false;
        OnDebugModeChanged?.Invoke(isDebugMode);

        OnNewState?.Invoke(currentState);
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    /// <summary>
    /// Displays the next sceen
    /// </summary>
    /// <param name="next">Which screen should be shown next</param>
    public void DisplayScreen(GameScreen next)
    {
        Deb("DisplayScreen() #####");
        Deb("DisplayScreen(): current state: " + currentState + ", current screen: " + currentScreen + ", next screen: " + next);

        bool flag = false;

        switch (currentScreen)
        {
            case GameScreen.StartMenu:

                if (next == GameScreen.PlayScreen)
                {
                    Deb("DisplayScreen(): from start screen you're going into play screen. Changing here current state and setting true the flag to manage the screen and state");

                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                }
                else if(next == GameScreen.SettingsMenu)
                {
                    Deb("DisplayScreen(): from start screen you're going into settings screen. Setting true the flag to manage the screen and state");

                    flag = true;
                }

                break;

            case GameScreen.PlayScreen:

                if (next == GameScreen.PauseMenu || next == GameScreen.StoreMenu)
                {
                    Deb("DisplayScreen(): from play screen you're going into an acceptable screen. Changing here current state and setting true the flag to manage the screen and state");

                    //currentState = GameState.Pause;
                    NewState(GameState.Pause);
                    flag = true;
                }

                if (next == GameScreen.GameOver)
                {
                    //currentState = GameState.GameOver;
                    NewState(GameState.GameOver);
                    flag = true;
                }

                break;

            case GameScreen.PauseMenu:
                if (next == GameScreen.PlayScreen)
                {
                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                }
                else if (next == GameScreen.SettingsMenu)
                {
                    flag = true;
                }

                break;

            case GameScreen.StoreMenu:

                if (next == GameScreen.PlayScreen)
                {
                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                }

                break;

            case GameScreen.SettingsMenu:
                if (next == GameScreen.PlayScreen)
                {
                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                }
                else if (next == GameScreen.StartMenu || next == GameScreen.CreditsMenu || next == GameScreen.PauseMenu)
                {
                    flag = true;
                }


                break;

            case GameScreen.CreditsMenu:

                if (next == GameScreen.PlayScreen)
                {
                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                }
                else if(next == GameScreen.StartMenu || next == GameScreen.SettingsMenu)
                {
                    flag = true;
                }

                break;

            case GameScreen.GameOver:
                if (next == GameScreen.PlayScreen)
                {
                    Deb("DisplayScreen(): from game over screen you're going into play screen. Changing here current state and setting true the flag to manage the screen and state");

                    //currentState = GameState.Play;
                    NewState(GameState.Play);
                    flag = true;
                } else if (next == GameScreen.StartMenu)
                {
                    Deb("DisplayScreen(): from game over screen you're going into start screen. Changing here current state and setting true the flag to manage the screen and state");

                    //currentState = GameState.Play;
                    NewState(GameState.Start);
                    flag = true;
                }

                break;
        }

        if (flag)
        {
            Deb("DisplayScreen(): flag is true => updating previous screens stack, current scene, and sending event on new screen and new state");

            //previousScreens.Push(currentScreen);
            currentScreen = next;

            Deb("DisplayScreen(): now current state is " + currentState + ", current screen is " + currentScreen);


            //OnNewState?.Invoke(currentState);
            OnNewScreen?.Invoke(currentScreen);
        }
    }

    /// <summary>
    /// Manages time flow in the game
    /// </summary>
    /// <param name="flag">Whether time should be stopped or not</param>
    public void Freeze(bool flag = true)
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

    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void NavigateBack()
    {
        //TODO
        // fai la pop ma non la push
    }

    /// <summary>
    /// Changes the state of the game
    /// </summary>
    /// <param name="state">Which new state we want to set</param>
    private void NewState(GameState state)
    {
        if(currentState==GameState.GameOver && state == GameState.Play)
        {
            ResetParameters();
        }

        currentState = state;

        OnNewState?.Invoke(currentState);
    }

    public void OpenStore()
    {
        Deb("OpenStore(): Display Store menu");
        DisplayScreen(GameScreen.StoreMenu);
    }

    /// <summary>
    /// Pause game function accessible from UI and more
    /// </summary>
    public void PauseGame()
    {
        DisplayScreen(GameScreen.PauseMenu);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ReloadGame()
    {
        //TODO
        if (DataManager.Instance.ReloadGame())
        {
            int level = DataManager.Instance.GetCurrentLevel();

            if (level != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(level);
            }

            currentState = GameState.Play;
            NewState(currentState);

            DisplayScreen(GameScreen.PlayScreen);
        }
        else
        {
            StartGame();
        }
        
    }

    private void ResetParameters()  //FIXME
    {
        EventSubscriber(false);
        EventSubscriber();

        OnParamsReset?.Invoke();
    }

    public void ResetGame()
    {
        if (GetSceneIndex() == 0)
        {
            DataManager.Instance.ResetGameData();
        }
        else
        {
            StartGame();
        }
    }

    /// <summary>
    /// Resume game function accessible from UI and more
    /// </summary>
    public void ResumeGame()
    {
        Deb("ResumeGame(): Display PlayScreen");
        DisplayScreen(GameScreen.PlayScreen);
    }

    /// <summary>
    /// Start game operations: resetting existing data and loading first scene.
    /// </summary>
    public void StartGame()
    {
        DataManager.Instance.ResetGameData();

        isTutorialOn = true;

        SceneManager.LoadScene(1);

        Deb("######");
        //NewState(GameState.Play);
        //DisplayScreen(GameScreen.PlayScreen);
    }

    public void StartTutorial()
    {
        isTutorialDone = false;
        isTutorialOn = true;

        if(currentState == GameState.Pause)
        {
            ResumeGame();
        }
    }

    public void SaveGame()
    {
        Deb("SaveGame(): saving game (TODO)");

        DataManager.Instance.SaveGameData();
    }

    public void ShowStartMenu()
    {
        SceneManager.LoadScene(0);

        NewState(GameState.Start);
        DisplayScreen(GameScreen.StartMenu);
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
    public delegate void DebugModeSwitchEvent(bool flag);
    public static DebugModeSwitchEvent OnDebugModeChanged;

    public delegate void UnpauseEvent();
    public static UnpauseEvent BeforeUnpause;
    public delegate void StartTutoriaEvent();
    public static StartTutoriaEvent OnStartTutorial;

    public delegate void ParamsResetEvent();
    public static ParamsResetEvent OnParamsReset;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // UI
            InputManager.OnDebugModeSwitch += SwitchDebugMode;

            InputManager.OnPause += OnPauseInput;
            InputManager.OnBack += OnBackInput;

            OnNewState += OnStateChanged;

            MCHealthController.OnDeath += OnGameOver;

            Tutorial.OnTutorialEnd += OnTutorialEnd;

            SceneManager.sceneLoaded += OnNewScene;
        }
        else
        {
            // UI
            InputManager.OnDebugModeSwitch -= SwitchDebugMode;

            InputManager.OnPause -= OnPauseInput;
            InputManager.OnBack -= OnBackInput;

            OnNewState -= OnStateChanged;

            MCHealthController.OnDeath -= OnGameOver;

            Tutorial.OnTutorialEnd -= OnTutorialEnd;

            SceneManager.sceneLoaded -= OnNewScene;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnTutorialEnd()
    {
        isTutorialOn = false;
        Freeze(false);
    }

    private void OnNewScene(Scene scene, LoadSceneMode mode)
    {
        Deb("OnNewScene(): Loaded a new scene\n{Scene name: " + scene.name + "; Scene index: " + scene.buildIndex + "; Load mode: " + mode + "}");

        if (scene.buildIndex != 0)
        {
            DisplayScreen(GameScreen.PlayScreen);

            DataManager.Instance.LoadSettingsData();
        }
    }

    private void OnPauseInput()
    {
        Deb("OnPause(): #####");

        switch (currentScreen)
        {
            case GameScreen.PlayScreen:
                if(!isTutorialOn)
                    DisplayScreen(GameScreen.PauseMenu);

                break;

            case GameScreen.PauseMenu:
            case GameScreen.StoreMenu:
                BeforeUnpause?.Invoke();

                DisplayScreen(GameScreen.PlayScreen);

                break;

            case GameScreen.SettingsMenu:
            case GameScreen.CreditsMenu:
                BeforeUnpause?.Invoke();

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

    public void OnBackInput()
    {
        switch (currentScreen)
        {
            case GameScreen.PauseMenu:
                DisplayScreen(GameScreen.PlayScreen);
                NewState(GameState.Play);
                //previousScreens.Pop();

                break;

            case GameScreen.StoreMenu:
                DisplayScreen(GameScreen.PlayScreen);
                NewState(GameState.Play);
                //previousScreens.Pop();

                break;

            case GameScreen.SettingsMenu:
                if (currentState == GameState.Pause)
                {
                    DisplayScreen(GameScreen.PauseMenu);
                }
                else if (currentState == GameState.Start)
                {
                    DisplayScreen(GameScreen.StartMenu);
                }
                
                break;

            case GameScreen.CreditsMenu:
                DisplayScreen(GameScreen.SettingsMenu);

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

                break;

            case GameState.GameOver:
                Deb("OnStateChanged(): cursor should be visible after all gameover option are shown.");

                StartCoroutine(ShowingGameOver(Consts.TIME_TO_CURSOR));

                break;

            default:
                Deb("OnStateChanged(): cursor should be visible now");

                Freeze();

                break;
        }
    }

    private void OnGameOver()
    {
        currentState = GameState.GameOver;
        NewState(currentState);

        DisplayScreen(GameScreen.GameOver);
    }

    private void SwitchDebugMode()
    {
        isDebugMode = !isDebugMode;

        OnDebugModeChanged?.Invoke(isDebugMode);
    }

    public void SwitchDebugMode(bool flag)
    {
        isDebugMode = flag;

        OnDebugModeChanged?.Invoke(isDebugMode);
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
