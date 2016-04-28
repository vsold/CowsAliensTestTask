using UnityEngine;
using UnityEngine.UI;

public class GameGUIController : MonoBehaviour
{
    [SerializeField] private GameObject levelGUI;
    [SerializeField] private GameObject gameOverGUI;
    [SerializeField] private Button resetButton;
    [SerializeField] private LevelGUITimer levelTimer;
    [SerializeField] private Text levelBonusesText;
    [SerializeField] private Text gameOverTime;
    [SerializeField] private Text gameOverBonuses;

    private GameControl gameControl;

	void Start()
    {
        levelGUI.SetActive(true);
        gameOverGUI.SetActive(false);
	    resetButton.onClick.AddListener(OnResetButtonClick);
        gameControl = ApplicationManager.Instance.GameControl;
        gameControl.onGameOver += OnGameOver;
    }

    public void OnResetButtonClick()
    {
        gameControl.Restart();
    }

    public void UpdateLevelBonuses(int count)
    {
        levelBonusesText.text = count.ToString();
    }

    public void OnGameOver()
    {
        levelGUI.SetActive(false);
        levelTimer.Stop();
        gameOverTime.text = levelTimer.TimeString;
        gameOverBonuses.text = levelBonusesText.text;
        gameOverGUI.SetActive(true);
        gameControl.onGameOver -= OnGameOver;
    }
}
