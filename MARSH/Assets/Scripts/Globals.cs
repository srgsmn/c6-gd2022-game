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
    }

    // ENUMS ___________________________________________________________________ ENUMS
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
        SL, CC
    }

    public enum CollectableType
    {
        SL, CC
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

    // CLASSES _________________________________________________________________ CLASSES
    [Serializable]
    public class PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;

        public float health, maxHealth, armor, maxArmor;
        public float defHFactor, defAFactor;

        public int sl, cc;

        public PlayerData()
        {
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
    }

    [Serializable]
    public class EnvironmentData
    {
        public List<string> collectablesID;
        public string lastCheckpointID;

        public EnvironmentData()
        {
            collectablesID = new List<string>();
            lastCheckpointID = null;
        }
    }

    [Serializable]
    public class GameData
    {
        public PlayerData player;
        public EnvironmentData environment;

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
    }
}
