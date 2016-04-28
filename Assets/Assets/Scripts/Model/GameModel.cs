using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour 
{
    private GameSettings settings = new GameSettings();

    public GameSettings Settings
    {
        get { return settings ?? (settings = new GameSettings()); }
    }

    public int WinCondition 
    {
        get { return settings.WinCount; }
    }
}
