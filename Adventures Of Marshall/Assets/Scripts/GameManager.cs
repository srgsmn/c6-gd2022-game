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

    [Serializable]
    public class GameData
    {
        private int sl, cc, level;
        private float health, armor, maxHealth, maxArmor;
        private Vector3 position;

        public GameData()
        {
            level = 0;
            sl = 0;
            cc = 0;
            health = 0;
            armor = 0;
            maxHealth = 0;
            maxArmor = 0;
            position = Vector3.zero;
        }

        public void SetLv(int lv) { level = lv; }
        public void SetSL(int sl){ this.sl = sl; }
        public void SetCC(int cc) { this.cc = cc; }
        public void SetH(float health) { this.health = health; }
        public void SetA(float armor) { this.armor = armor; }
        public void SetMaxH(float maxHealth) { this.maxHealth = maxHealth; }
        public void SetMaxA(float maxArmor) { this.maxArmor = maxArmor; }
        public void SetPos(Vector3 pos) { this.position = pos; }

        public int GetLv() { return level; }
        public int GetSL() { return sl; }
        public int GetCC() { return cc; }
        public float GetH() { return health; }
        public float GetA() { return armor; }
        public float GetMaxH() { return maxHealth; }
        public float GetMaxA() { return maxArmor; }
        public Vector3 GetPos() { return position; }

        public float[] GetFloatPos()
        {
            float[] posF = new float[3];
            posF[0] = position.x;
            posF[1] = position.y;
            posF[2] = position.z;

            return posF;
        }
    }

    public static GameManager Instance;

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
    public static GameData currentGame, savedGame;

    public static int level = 0;

    //EVENTS
    public delegate void IntDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, int value);
    public static event IntDataChangedEvent OnIntDataChanged;

    public delegate void FloatDataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, float value);
    public static event FloatDataChangedEvent OnFloatDataChanged;

    public delegate void Vector3DataChangedEvent(bool saved, SaveDebugPanelManager.InfoType type, Vector3 value);
    public static event Vector3DataChangedEvent OnVector3DataChanged;

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

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
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
    }

    private void UpdatePlayerSL(int value)
    {
        currentGame.SetSL(value);
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.sl, currentGame.GetSL());
    }

    private void UpdatePlayerCC(int value)
    {
        currentGame.SetCC(value);
        OnIntDataChanged(false, SaveDebugPanelManager.InfoType.cc, currentGame.GetCC());

    }

    private void UpdatePlayerH(float value)
    {
        currentGame.SetH(value);
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.health, currentGame.GetH());
    }

    private void UpdatePlayerMaxH(float value)
    {
        currentGame.SetMaxH(value);
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxHealth, currentGame.GetMaxH());
    }

    private void UpdatePlayerA(float value)
    {
        currentGame.SetA(value);
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.armor, currentGame.GetA());
    }

    private void UpdatePlayerMaxA(float value)
    {
        currentGame.SetMaxA(value);
        OnFloatDataChanged(false, SaveDebugPanelManager.InfoType.maxArmor, currentGame.GetMaxA());
    }

    private void UpdatePlayerPosition(Vector3 value)
    {
        currentGame.SetPos(value);
        OnVector3DataChanged(false, SaveDebugPanelManager.InfoType.position, currentGame.GetPos());
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
        currentGame = new GameData();
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

    //FIXME forse non serve più
    private float[] LastPositionToFloat()
    {
        float[] posF = new float[3];
        Vector3 posV = currentGame.GetPos();

        posF[0] = posV.x;
        posF[1] = posV.y;
        posF[2] = posV.z;

        return posF;
    }

    public static void SaveData()
    {
        SaveSystem.SaveData(currentGame);
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
