using System;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;   //Instance
    public static SaveData current      //Related singleton
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public GameData gameData;

}

