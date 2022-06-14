using UnityEngine;

[System.Serializable]
public class GameData
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

    public override string ToString()
    {
        string result = "GameData object:";
        result += "\nLevel: " + level;
        result += "\nLast Position: " + lastPosition;

        return result;
    }
}