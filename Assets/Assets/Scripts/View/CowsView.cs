using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CowsView
{
    private GameView gameView;
    private GameSettings Settings
    {
        get
        {
            return ApplicationManager.Instance.Settings;
        }
    }

    private List<Transform> dogs;

    public void Build(GameView _gameView, ObjectPool pool, List<Transform> _dogs)
    {
        dogs = _dogs;
        gameView = _gameView;
        for (int i = 0; i < Settings.CowsCount; i++)
        {
            GameObject go = pool.GetObject();
            if (go != null)
            {
                go.name = "Cow" + i;
                Vector2 newPos = GetCowPosition();
                go.transform.position = new Vector3(newPos.x, newPos.y, go.transform.position.z);
                go.SetActive(true);
            }
            else
            {
                Debug.LogError("Not enough objects to spawn cows");
            }
        }
    }

    private bool IsCrossingPaddlock(Vector2 pos)
    {
        var allowedRad = Settings.paddlocGeneratekRadius + Settings.cowGenerateRadius;
        return (pos - (Vector2)gameView.Paddlock.position).magnitude <= allowedRad;
    }

    private bool IsCrossingDogs(Vector2 pos)
    {
        var allowedRad = Settings.dogGenerateRadius + Settings.cowGenerateRadius;
        return dogs.Any(dog => (pos - (Vector2)dog.position).magnitude <= allowedRad);
    }

    private Vector2 GetCowPosition()
    {
        Vector2 newPos = Vector2.zero;
        bool isCrossing = true;
        while (isCrossing)
        {
            newPos = gameView.GetRandowmPosition();
            isCrossing = IsCrossingPaddlock(newPos) || IsCrossingDogs(newPos);
        }
        return newPos;
    }
}
