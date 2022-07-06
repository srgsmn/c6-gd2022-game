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

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Play,
        Pause
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

    //EVENTS
    public delegate void IntDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, int value);
    public static event IntDataChangedEvent OnIntDataChanged;

    public delegate void FloatDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, float value);
    public static event FloatDataChangedEvent OnFloatDataChanged;

    public delegate void Vector3DataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, Vector3 value);
    public static event Vector3DataChangedEvent OnVector3DataChanged;

    public delegate void NewSavingEvent(GameData savedGame);
    public static event NewSavingEvent OnSave;

    private void Awake()
    {
        //Subscribing to the event
        CollectablesManager.OnSLUpdate += UpdatePlayerSL;
        CollectablesManager.OnCCUpdate += UpdatePlayerCC;
        PlayerHealthController.OnArmorUpdate += UpdatePlayerA;
        PlayerHealthController.OnHealthUpdate += UpdatePlayerH;
        PlayerHealthController.OnMaxArmorUpdate += UpdatePlayerMaxA;
        PlayerHealthController.OnMaxHealthUpdate += UpdatePlayerMaxH;
        PlayerController.OnPositionUpdate += UpdatePlayerPosition;
        PlayerController.OnRotationUpdate += UpdatePlayerRotation;

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
            LoadGame();
    }

    

    private void OnDestroy()
    {
        //Unsubscribing to the event
        CollectablesManager.OnSLUpdate -= UpdatePlayerSL;
        CollectablesManager.OnCCUpdate -= UpdatePlayerCC;
        PlayerHealthController.OnArmorUpdate -= UpdatePlayerA;
        PlayerHealthController.OnHealthUpdate -= UpdatePlayerH;
        PlayerHealthController.OnMaxArmorUpdate -= UpdatePlayerMaxA;
        PlayerHealthController.OnMaxHealthUpdate -= UpdatePlayerMaxH;
        PlayerController.OnPositionUpdate -= UpdatePlayerPosition;
        PlayerController.OnRotationUpdate -= UpdatePlayerRotation;
    }
    
    private void UpdatePlayerSL(int value)
    {
        currentGame.sl = value;
        SaveData.current.gameData.sl = currentGame.sl;
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.sl, currentGame.sl);
    }

    private void UpdatePlayerCC(int value)
    {
        currentGame.cc = value;
        SaveData.current.gameData.cc = currentGame.cc;
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.cc, currentGame.cc);

    }

    private void UpdatePlayerH(float value)
    {
        currentGame.health = value;
        SaveData.current.gameData.health = currentGame.health;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.health, currentGame.health);
    }

    private void UpdatePlayerMaxH(float value)
    {
        currentGame.maxHealth = value;
        SaveData.current.gameData.maxHealth = currentGame.maxHealth;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxHealth, currentGame.maxHealth);
    }

    private void UpdatePlayerA(float value)
    {
        currentGame.armor = value;
        SaveData.current.gameData.armor = currentGame.armor;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.armor, currentGame.armor);
    }

    private void UpdatePlayerMaxA(float value)
    {
        currentGame.maxArmor = value;
        SaveData.current.gameData.maxArmor = currentGame.maxArmor;
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxArmor, currentGame.maxArmor);
    }

    private void UpdatePlayerPosition(Vector3 value)
    {
        currentGame.position = value;
        SaveData.current.gameData.position = currentGame.position;
        OnVector3DataChanged(false, SaveDebugPanelManager.InfoType.position, currentGame.position);
    }

    private void UpdatePlayerRotation(Quaternion value)
    {
        currentGame.rotation = value;
        SaveData.current.gameData.rotation = currentGame.rotation;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);

        UpdatePlayerMaxH(0);
        UpdatePlayerH(0);
        UpdatePlayerMaxA(0);
        UpdatePlayerA(0);
        UpdatePlayerSL(0);
        UpdatePlayerCC(0);
        UpdatePlayerPosition(Vector3.zero);
        UpdatePlayerRotation(Quaternion.identity);
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
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                SceneManager.LoadScene(0);

                break;

            case GameState.Play:

                break;

            case GameState.Pause:

                break;

        }

        onGameStateChanged?.Invoke(currentState);
    }

    public GameState GetState()
    {
        return currentState;
    }

    //FIXME non serve più
    /*private float[] Vector3ToFloats(Vector3 vect)
    {
        float[] array = new float[3];

        array[0] = vect.x;
        array[1] = vect.y;
        array[2] = vect.z;

        return array;
    }

    //FIXME non serve più
    private Vector3 FloatsToVector3(float[] floats)
    {
        Vector3 vect;

        vect.x = floats[0];
        vect.y = floats[1];
        vect.z = floats[2];

        return vect;
    }
    */
    public static void SaveGame()
    {
        Debug.Log("GameManager.cs | Saving the game");
        SerializationManager.Save(SaveData.current);

        OnSave(LoadGame());
    }

    public static GameData LoadGame()
    {
        Debug.Log("GameManager.cs | Loading the game");
        return loadedGame = (GameData)SerializationManager.Load();
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
