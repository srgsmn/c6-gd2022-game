/* GameManager.cs
 * --------------
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Implementare check livello (salvato in game master) e attivazione voce
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

    /*
    private class GameData
    {
        private int level;
        private Vector3 lastPosition;

        public GameData(int level, Vector3 lastPosition)
        {
            this.level = level;
            this.lastPosition = lastPosition;
        }

        public void UpdateGameData(int level, Vector3 lastPosition)
        {
            this.level = level;
            this.lastPosition = lastPosition;
        }

        public int GetLevel() { return level; }
        public void SetLevel(int level) { this.level = level; }
        public Vector3 GetPosition() { return lastPosition; }
        public void SetPosition(Vector3 position) { lastPosition = position; }
    }
    */

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
    private GameData currentGame, savedGame;

    public static int level = 0;

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
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
        
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

    private float[] LastPositionToFloat()
    {
        float[] posF = new float[3];
        Vector3 posV = currentGame.GetPosition();

        posF[0] = posV.x;
        posF[1] = posV.y;
        posF[2] = posV.z;

        return posF;
    }

    private void DEB(string msg)    //DEBUG
    {
        Debug.Log(this.GetType().Name + " | " + msg);
    }
}
