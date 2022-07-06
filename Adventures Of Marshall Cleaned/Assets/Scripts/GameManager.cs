/* GameManager.cs
 * --------------
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Implementare check livello (salvato in game master) e attivazione voce
 *  - Implementare evento che attivi il menu di pausa e il freeze in base allo stato del gioco.
 *  
 * Ref:
 *  - https://www.youtube.com/watch?v=4I0vonyqMi8
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    private string DebIntro = "GameManager.cs | ";

    private static string[] sceneNames =
    {
        "00_StartMenu", "Scenes/Tests/SaveLoadScene"
    };

    public enum GameState
    {
        MainMenu,
        Play,
        Pause,
        MenuOpen
    }

    public static GameManager Instance;

    public static GameData currentGame, loadedGame;

    public static event Action<GameState> onGameStateChanged;
    // I can subscribe to this event from other classe by adding a subscriber in the following way:
    //      GameManager.onGameStateChanged += <method name that has to be subscribed>
    // Then the method will have a signature like:
    //      private void <method name>(GameState obj){}
    // Also good practice to unsubscribe when done like:
    //      void OnDestroy(){
    //          GameManager.onGameStateChanged -= <method name that has to be subscribed>
    //      }

    private GameState currentState;

    public static int level = 0;

    private static Dictionary<int, Scene> levels;

    //EVENTS
    public delegate void IntDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, int value);
    public static event IntDataChangedEvent OnIntDataChanged;

    public delegate void FloatDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, float value);
    public static event FloatDataChangedEvent OnFloatDataChanged;

    public delegate void Vector3DataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, Vector3 value);
    public static event Vector3DataChangedEvent OnVector3DataChanged;

    public delegate void NewSavingEvent(GameData savedGame);
    public static event NewSavingEvent OnSave;

    private void EventSubscriber()
    {
        CollectablesManager.OnSLUpdate += UpdatePlayerSL;
        CollectablesManager.OnCCUpdate += UpdatePlayerCC;
        PlayerHealthController.OnArmorUpdate += UpdatePlayerA;
        PlayerHealthController.OnHealthUpdate += UpdatePlayerH;
        PlayerHealthController.OnMaxArmorUpdate += UpdatePlayerMaxA;
        PlayerHealthController.OnMaxHealthUpdate += UpdatePlayerMaxH;
        PlayerController.OnPositionUpdate += UpdatePlayerPosition;
        PlayerController.OnRotationUpdate += UpdatePlayerRotation;
    }

    private void EventUnsubscriber()
    {
        CollectablesManager.OnSLUpdate -= UpdatePlayerSL;
        CollectablesManager.OnCCUpdate -= UpdatePlayerCC;
        PlayerHealthController.OnArmorUpdate -= UpdatePlayerA;
        PlayerHealthController.OnHealthUpdate -= UpdatePlayerH;
        PlayerHealthController.OnMaxArmorUpdate -= UpdatePlayerMaxA;
        PlayerHealthController.OnMaxHealthUpdate -= UpdatePlayerMaxH;
        PlayerController.OnPositionUpdate -= UpdatePlayerPosition;
        PlayerController.OnRotationUpdate -= UpdatePlayerRotation;
    }

    private void LevelDictionaryMaker()
    {
        levels = new Dictionary<int, Scene>();
        bool flag;
        int i = 0;

        foreach(string sceneName in sceneNames)
        {
            flag = levels.TryAdd(i, SceneManager.GetSceneByName(sceneName));
            Debug.Log(DebIntro + "Trying to add scene: [scene name: " + sceneName + "; Dictionary entry: [" + i + " | " + levels[i].name + "]]");
            i++;
        }
    }

    private void Awake()
    {
        //Subscribing to the event
        EventSubscriber();
        LevelDictionaryMaker();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

        if (currentGame == null)
            currentGame = new GameData();

        if (loadedGame == null)
        {
            LoadGame();
            loadedGame = new GameData();
        }
    }

    private void OnDestroy()
    {
        //Unsubscribing to the event
        EventUnsubscriber();
    }

    private void UpdatePlayerLevel(int value)
    {
        currentGame.level = value;
        //SaveData.current.gameData.sl = currentGame.sl;
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.level, currentGame.level);
    }

    private void UpdatePlayerSL(int value)
    {
        currentGame.sl = value;
        //SaveData.current.gameData.sl = currentGame.sl;
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.sl, currentGame.sl);
    }

    private void UpdatePlayerCC(int value)
    {
        currentGame.cc = value;
        //SaveData.current.gameData.cc = currentGame.cc;
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.cc, currentGame.cc);

    }

    private void UpdatePlayerH(float value)
    {
        currentGame.health = value;
        //SaveData.current.gameData.health = currentGame.health;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.health, currentGame.health);
    }

    private void UpdatePlayerMaxH(float value)
    {
        currentGame.maxHealth = value;
        //SaveData.current.gameData.maxHealth = currentGame.maxHealth;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxHealth, currentGame.maxHealth);
    }

    private void UpdatePlayerA(float value)
    {
        currentGame.armor = value;
        //SaveData.current.gameData.armor = currentGame.armor;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.armor, currentGame.armor);
    }

    private void UpdatePlayerMaxA(float value)
    {
        currentGame.maxArmor = value;
        //SaveData.current.gameData.maxArmor = currentGame.maxArmor;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxArmor, currentGame.maxArmor);
    }

    private void UpdatePlayerPosition(Vector3 value)
    {
        currentGame.position = value;
        //SaveData.current.gameData.position = currentGame.position;
        OnVector3DataChanged(false, SaveDebugPanelManager.InfoType.position, currentGame.position);
    }

    private void UpdatePlayerRotation(Quaternion value)
    {
        currentGame.rotation = value;
        //SaveData.current.gameData.rotation = currentGame.rotation;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);

        currentGame.level = 1;
        /*UpdatePlayerLevel(1);
        UpdatePlayerSL(0);
        UpdatePlayerCC(0);
        UpdatePlayerMaxH(0);
        UpdatePlayerH(0);
        UpdatePlayerMaxA(0);
        UpdatePlayerA(0);
        
        UpdatePlayerPosition(Vector3.zero);
        UpdatePlayerRotation(Quaternion.identity);*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DEB("ESC's been pressed");

            switch (currentState)
            {
                case GameState.Play:
                    UpdateGameState(GameState.Pause);
                    break;

                case GameState.Pause:
                    UpdateGameState(GameState.Play);
                    break;

                default:
                    DEB("Default fall here");
                    break;
            }
        }
    }

    /*
    public static int GetCurrentLevel()
    {
        int currentLevel = currentGame.GetLevel()
        return currentLevel;
    }
    */
    
    public void UpdateGameState(GameState newState)
    {
        //TODO gestione freeze (da prendere da PauseMenu)
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene(0);

                break;

            case GameState.Play:
                Freeze(false);

                break;

            case GameState.Pause:
                Freeze(true);

                break;

            case GameState.MenuOpen:
                Freeze(true);

                break;

        }

        onGameStateChanged?.Invoke(currentState);
    }

    private void Freeze(bool flag)
    {
        //FIXME
        if (flag)
        {
            Debug.Log(DebIntro + "Freezing the game");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log(DebIntro + "Unfreezing the game");
            Time.timeScale = 1f;
        }
    }

    public GameState GetState()
    {
        return currentState;
    }

    public static void SaveGame()
    {
        Debug.Log("GameManager.cs | Saving the game");
        //SerializationManager.Save(SaveData.current);

        DataPersistenceManager.instance.SaveGame();

        Debug.Log("GameManager.cs | Now trying to load");
        DataPersistenceManager.instance.LoadGame();
        //OnSave(LoadGame());
    }

    public static void LoadGame()
    {
        Debug.Log("GameManager.cs | Loading the game");
        DataPersistenceManager.instance.LoadGame();
    }

    public static bool CheckSaved()
    {
        return FileDataHandler.Check();
    }

    public static void StartGame(bool loadSaved)
    {
        if (loadSaved)
        {
            SceneManager.LoadScene(currentGame.level);
        }
        else
        {
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadScene(1);
        }
        
    }

    public void LoadData(GameData data)
    {
        currentGame.level = data.level;
        currentGame.sl = data.sl;
        currentGame.cc = data.cc;
        currentGame.health = data.health;
        currentGame.maxHealth = data.maxHealth;
        currentGame.armor = data.armor;
        currentGame.maxArmor = data.maxArmor;
        currentGame.position = data.position;
        currentGame.rotation = data.rotation;
    }

    public void SaveData(GameData data)
    {
        Debug.Log("GameManager.cs | SaveData() called");
        data.level = currentGame.level;
        Debug.Log("GameManager.cs | Sending current level (" + currentGame.level + ") to data to save (" + data.level + ")");
        loadedGame.level = data.level;
        Debug.Log("GameManager.cs | Copying saved level (" + data.level + ") into loaded game data (" + loadedGame.level + ")");
        OnIntDataChanged(true, SaveDebugPanelManager.InfoType.level, loadedGame.level);

        data.sl = currentGame.sl;
        Debug.Log("GameManager.cs | Sending current sl (" + currentGame.sl + ") to data to save (" + data.sl + ")");
        loadedGame.sl = data.sl;
        Debug.Log("GameManager.cs | Copying saved sl (" + data.sl + ") into loaded game data (" + loadedGame.sl + ")");

        OnIntDataChanged(true, SaveDebugPanelManager.InfoType.sl, loadedGame.sl);

        data.cc = currentGame.cc;
        loadedGame.cc = data.cc;
        OnIntDataChanged(true, SaveDebugPanelManager.InfoType.cc, loadedGame.cc);

        data.health = currentGame.health;
        loadedGame.health = data.health;
        OnFloatDataChanged(true, SaveDebugPanelManager.InfoType.health, loadedGame.health);

        data.maxHealth = currentGame.maxHealth;
        loadedGame.maxHealth = data.maxHealth;
        OnFloatDataChanged(true, SaveDebugPanelManager.InfoType.maxHealth, loadedGame.maxHealth);

        data.armor = currentGame.armor;
        loadedGame.armor = data.armor;
        OnFloatDataChanged(true, SaveDebugPanelManager.InfoType.armor, loadedGame.armor);

        data.maxArmor = currentGame.maxArmor;
        loadedGame.maxArmor = data.maxArmor;
        OnFloatDataChanged(true, SaveDebugPanelManager.InfoType.maxArmor, loadedGame.maxArmor);

        data.position = currentGame.position;
        loadedGame.position = data.position;
        OnVector3DataChanged(true, SaveDebugPanelManager.InfoType.position, loadedGame.position);

        data.rotation = currentGame.rotation;
        loadedGame.rotation = data.rotation;
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
