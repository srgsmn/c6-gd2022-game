/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{

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
        Pos, Rot, Health, MaxHealth, DefHFact, Armor, MaxArmor, DefAFact
    }

    // CLASSES _________________________________________________________________ CLASSES
    [Serializable]
    public class PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;

        public float health, maxHealth, armor, maxArmor;
        public float defHFactor, defAFactor;

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
