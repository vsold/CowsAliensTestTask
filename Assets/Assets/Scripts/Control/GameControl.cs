using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameGUIController guiController;
    private UserInputHandler gameInputHandler;
    private ApplicationManager applicationManager;
    private GameModel model;

    private Dog currentDog;
    private int cowsCount = 0;
    private int cowsPlacedCount = 0;
    private int bonusesCount = 0;

    public Action onGameOver;

    private void Awake()
    {
        gameInputHandler = gameObject.AddComponent<UserInputHandler>();
        gameInputHandler.Init(this);
    }

    private void Start()
    {
        if (ApplicationManager.Instance != null)
        {
            applicationManager = ApplicationManager.Instance;
            model = applicationManager.GameModel;
        }
    }

    public void OnUserInput(Vector2 point, Collider2D _collider2D)
    {
        switch (_collider2D.tag)
        {
            case "Dog" :
                //if (currentDog != null)
                //    currentDog.GetComponent<SpriteRenderer>().color = Color.blue;
                currentDog = _collider2D.gameObject.GetComponent<Dog>();
                //currentDog.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "InputArea" :
            case "Cow" :
            case "Paddlock":
                if (currentDog != null)
                {
                    currentDog.SetNewMoveTarget(point);
                }
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void NewCowCollected()
    {
        cowsCount++;
        cowsPlacedCount++;

        if (cowsCount >= model.WinCondition)
        {
            gameInputHandler.Block = true;

            if (onGameOver != null)
                onGameOver();

            return;
        }

        if (cowsPlacedCount >= applicationManager.Settings.BonusCowsCount)
        {
            cowsPlacedCount = 0;
            Bonus bonus = applicationManager.GameView.CreateBonus();
            bonus.Collected += CollectedBonus;
        }
    }
    private void CollectedBonus(Bonus bonus)
    {
        bonusesCount++;
        guiController.UpdateLevelBonuses(bonusesCount);
        bonus.Collected -= CollectedBonus;
    }
}
