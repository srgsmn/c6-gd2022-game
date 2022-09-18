/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
    // CONSTS __________________________________________________________________ CONSTS
    public static class Consts
    {
        public const int COLLECTABLE_MAX = 1000;
        public const float SHOW_TIME = 3f;
        public const float TIME_TO_CURSOR = 3.5f;
        public const float TIME_TO_TUTORIAL = 2.5f;
        public const int SCREEN_HISTORY_LENGTH = 5;

        public static Color unarmored = Color.white;
        //public static Color armored = Color.black;
        //public static Color unarmored = new Color(241, 248, 255);
        public static Color armored = new Color(106, 34, 0);
    }

    // CONSTS __________________________________________________________________ CONSTS

    /// <summary>
    /// Class with useful dictionaries made with functions
    /// </summary>
    public static class Dicts
    {
        public static ButtonFeature ButtonFeatures(ButtonActionType type)
        {
            ButtonFeature buff = new ButtonFeature();

            switch (type)
            {
                case ButtonActionType.Normal:
                    buff.bgColor = new Color(0f, 0f, 0f, 1f);

                    break;

                case ButtonActionType.Critic:
                    buff.bgColor = new Color(1f, .25f, .2f, 1f);
                    buff.defTxtColor = new Color(0.7f, 0f, 0f, 1f);

                    break;

                case ButtonActionType.Gameover:
                    buff.bgColor = new Color(1f, 1f, 1f, 1f);
                    buff.defTxtColor = new Color(1f, 1f, 1f, 1f);
                    buff.hovTxtColor = new Color(0f, 0f, 0f, 1f);

                    break;
            }

            return buff;
        }

        public static AlertObject AlertProperties(AlertType type)
        {
            AlertObject buff = new AlertObject();

            switch (type)
            {
                case AlertType.None:
                    buff.title = "{TITLE}";
                    buff.message = "{message}";
                    buff.cancelText = "{CANCEL}";
                    buff.confirmText = "{CONFIRM}";

                    buff.background = new Color(1f, 0, 0, .5f);

                    break;

                case AlertType.Quit:
                    buff.title = "QUIT";
                    buff.message = "Are you sure you really want to quit the game?\nEvery progress after the last checkpoint will be lost";
                    buff.cancelText = "CANCEL";
                    buff.confirmText = "QUIT";

                    buff.background = new Color(1f, 0, 0, .5f);

                    break;

                case AlertType.Reset:
                    buff.title = "RESET";
                    buff.message = "Are you sure you really want to reset the game?\nThis operation will erase all your saved data from the disk";
                    buff.cancelText = "CANCEL";
                    buff.confirmText = "RESET";

                    buff.background = new Color(1f, 0, 0, .5f);

                    break;

                case AlertType.StartMenu:
                    buff.title = "RETURNING TO START MENU";
                    buff.message = "Are you sure you really want to return to the start menu?\nEvery progress after the last checkpoint will be lost";
                    buff.cancelText = "CANCEL";
                    buff.confirmText = "START MENU";

                    buff.background = new Color(1f, 0, 0, .5f);

                    break;
            }

            return buff;
        }
    }

    // ENUMS ___________________________________________________________________ ENUMS
    public enum AlertType
    {
        None, Quit, Reset, StartMenu
    }

    public enum ButtonActionType
    {
        Normal, Critic, Gameover
    }

    public enum DebAction
    {
        Inc, Dec, Max, Rst
    }

    public enum DebValue
    {
        H, A, SL, CC
    }

    public enum DebMsgType
    {
        log, warn, err
    }

    /// <summary>
    /// Parameter types of a character
    /// </summary>
    public enum ChParam
    {
        Pos, Rot, Health, MaxHealth, DefHFact, Armor, MaxArmor, DefAFact,
        SL, CC, Key, Wheel
    }

    public enum CollectableType
    {
        SL, CC, Key, Wheel
    }

    public enum SettingsOption
    {
        invertXAxis, invertYAxis, mouseSensitivity
    }

    /// <summary>
    /// State of the game (NOT the screen to display!)
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// The game isn't playable yet (pre-play state)
        /// </summary>
        Start,
        /// <summary>
        /// The game is playable (in-play state)
        /// </summary>
        Play,
        /// <summary>
        /// The game is stopped but still playable (in-play state)
        /// </summary>
        Pause,
        /// <summary>
        /// The game is no longer playable because player died (post-play state)
        /// </summary>
        GameOver
    }

    /// <summary>
    /// Screen to display (NOT the state of the game!)
    /// </summary>
    public enum GameScreen
    {
        StartMenu,
        PlayScreen,
        PauseMenu,
        SettingsMenu,
        CreditsMenu,
        StoreMenu,
        GameOver
    }

    public enum TutorialPhase
    {
        Welcome, View, Movement, Jump, Sprint, Action, Collectables, Places, Pause, Final, None
    }

    public enum ProximityObject
    {
        Checkpoint, Store, Ladder, Spawner, None
    }

    public enum ProximityInfo
    {
        Memo, Tutorial, None
    }

    public enum PlayerAudioType
    {
        Jump, Landing, SL_Collection, CC_Collection, Death, Damage
    }   

    // STRUCTS _________________________________________________________________ STRUCTS
    [Serializable]
    public struct AudioEffect
    {
        public PlayerAudioType type;
        public AudioSource source;
    }

    [Serializable]
    public struct Reward
    {
        public GameObject prefab;
        public int quantity;
    }

    // CLASSES _________________________________________________________________ CLASSES

    public class AlertObject
    {
        public string title;
        public string message;
        public string cancelText, confirmText;

        public Color background;

        public AlertObject()
        {
            this.background = new Color(1f, 1f, 1f, 0f);
        }
    }

    public class ButtonFeature
    {
        public Color bgColor;
        public Color defTxtColor;
        public Color hovTxtColor;

        public ButtonFeature(Color bgColor, Color defTxtColor, Color hovTxtColor)
        {
            this.bgColor = bgColor;
            this.defTxtColor = defTxtColor;
            this.hovTxtColor = hovTxtColor;
        }

        public ButtonFeature()
        {
            bgColor = Color.white;
            defTxtColor = new Color(.2f, .2f, .2f, 1);
            hovTxtColor = Color.white;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int level;

        public Vector3 camPos;
        public Quaternion camRot;

        public Vector3 position;
        public Quaternion rotation;

        public float health, maxHealth, armor, maxArmor;
        public float defHFactor, defAFactor;

        public int sl, cc;

        public PlayerData()
        {
            level = 1;

            camPos = Vector3.zero;
            camRot = Quaternion.identity;

            position = Vector3.zero;
            rotation = Quaternion.identity;

            health = 0;
            maxHealth = 0;
            armor = 0;
            maxArmor = 0;
            defHFactor = 0;
            defAFactor = 0;

            sl = 0;
            cc = 0;
        }

        public PlayerData(PlayerData data)
        {
            Debug.Log("CREATING PLAYER DATA FROM PLAYER DATA");

            level = data.level;

            camPos = data.camPos;
            camRot = data.camRot;

            position = data.position;
            rotation = data.rotation;

            health = data.health;
            maxHealth = data.maxHealth;
            armor = data.armor;
            maxArmor = data.maxArmor;
            defHFactor = data.defHFactor;
            defAFactor = data.defAFactor;

            sl = data.sl;
            cc = data.cc;

            Debug.Log("END OF CREATING PLAYER DATA FROM PLAYER DATA");
        }
    }

    [Serializable]
    public class EnvironmentData
    {
        public List<string> collectablesIDs;
        public string lastCheckpointID;

        public EnvironmentData()
        {
            collectablesIDs = new List<string>();
            lastCheckpointID = null;
        }

        public EnvironmentData(EnvironmentData data)
        {
            Debug.Log("CREATING ENVIRONMENTAL DATA FROM ENVIRONMENTAL DATA");

            collectablesIDs = new List<string>(data.collectablesIDs);
            lastCheckpointID = data.lastCheckpointID;

            Debug.Log("END OF CREATING ENVIRONMENTAL DATA FROM ENVIRONMENTAL DATA");
        }
    }

    [Serializable]
    public class GameData
    {
        public PlayerData player;
        public EnvironmentData environment;
        public SettingsData settings;

        public GameData()
        {
            player = new PlayerData();
            environment = new EnvironmentData();
        }

        public GameData(PlayerData player, EnvironmentData environment)
        {
            this.player = player;
            this.environment = environment;
        }

        /*
        public GameData(GameData data)
        {
            player = new PlayerData();
            player.level = data.player.level;

            player.camPos = data.player.camPos;
            player.camRot = data.player.camRot;

            player.position = data.player.position;
            player.rotation = data.player.rotation;
            player.health = data.player.health;
            player.maxHealth = data.player.maxHealth;
            player.armor = data.player.armor;
            player.maxArmor = data.player.maxArmor;
            player.defHFactor = data.player.defHFactor;
            player.defAFactor = data.player.defAFactor;
            player.sl = data.player.sl;
            player.cc = data.player.cc;

            environment = new EnvironmentData();
            environment.collectablesIDs = new List<string>(data.environment.collectablesIDs);
            environment.lastCheckpointID = data.environment.lastCheckpointID;
        }*/

        public GameData(GameData data)
        {
            player = new PlayerData(data.player);
            environment = new EnvironmentData(data.environment);
            settings = new SettingsData(data.settings);
        }
    }

    [Serializable]
    public class SettingsData
    {
        public bool invertYAxis;
        public bool invertXAxis;
        public float mouseSensitivity;

        public SettingsData()
        {
            invertYAxis = false;
            invertXAxis = false;
            mouseSensitivity = 0f;
        }

        public SettingsData(SettingsData data)
        {
            invertXAxis = data.invertXAxis;
            invertYAxis = data.invertYAxis;
            mouseSensitivity = data.mouseSensitivity;
        }
    }

    public class StoreTransaction
    {
        public int SL, CC;
        public bool isIncremental;
        public float health, armor;
        public float defHFactor;
        public float defAFactor;

        public StoreTransaction(int SL, int CC, bool isIncremental, float health, float armor, float defHFactor, float defAFactor)
        {
            this.SL = SL;
            this.CC = CC;
            this.isIncremental = isIncremental;
            this.health = health;
            this.armor = armor;
            this.defHFactor = defHFactor;
            this.defAFactor = defAFactor;
        }
    }

    [Serializable]
    public class Timer
    {
        private float amount;
        public float time;
        public bool isRunning;

        public Timer()
        {
            amount = Consts.SHOW_TIME;
            time = amount;
        }

        public Timer(float amount)
        {
            this.amount = amount;
            time = this.amount;
        }

        public void Reset()
        {
            time = amount;
            Pause();
        }

        public void Pause()
        {
            if (isRunning) isRunning = false;
        }

        public void Start()
        {
            if (!isRunning && time == amount) isRunning = true;
            else if(!isRunning && time != amount)
            {
                Reset();
                Start();
            }
        }

        public void Stop()
        {
            Pause();
            Reset();
        }
    }
}
