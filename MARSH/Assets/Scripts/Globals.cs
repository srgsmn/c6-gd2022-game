/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{

    // ENUMS ___________________________________________________________________ ENUMS
    public enum DebugAction
    {
        Inc, Dec, Max, Rst
    }

    public enum DebugValue
    {
        H, A, SL, CC
    }

    public enum DebugMsgType
    {
        log, warn, err
    }

    /// <summary>
    /// Parameter types of a character
    /// </summary>
    public enum CharParam
    {
        Pos, Rot
    }

    // CLASSES _________________________________________________________________ CLASSES
    [Serializable]
    public class PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;

        public PlayerData()
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
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
