/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
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

    public static class Functions
    {
        public static void Deb(string msg, DebugMsgType type = DebugMsgType.log)
        {
            switch (type)
            {
                case DebugMsgType.log:
                    Debug.Log(msg);

                    break;

                case DebugMsgType.warn:
                    Debug.LogWarning(msg);

                    break;

                case DebugMsgType.err:
                    Debug.LogError(msg);

                    break;
            }
        }
    }
}
