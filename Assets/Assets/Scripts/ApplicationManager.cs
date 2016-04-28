using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField]
    private GameModel gameModel;
    [SerializeField]
    private GameView gameView;
    [SerializeField]
    private GameControl gameCotrol;
    [SerializeField] 
    private Camera mainCamera;

    private GameSettings settings;

    public static ApplicationManager Instance
    {
        get;
        private set;
    }

    public GameSettings Settings
    {
        get { return settings ?? (settings = new GameSettings()); }
    }

    public GameModel GameModel 
    {
        get { return gameModel;}
    }

    public GameView GameView 
    {
        get { return gameView;}
    }

    public GameControl GameControl 
    {
        get { return gameCotrol;}
    }

    public Camera MainCamera 
    {
        get { return mainCamera; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

}
