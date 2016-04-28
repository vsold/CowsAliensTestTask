using UnityEngine;
using System.Collections.Generic;

public class DogsView
{
    private GameView gameView;
    private GameSettings Settings
    {
        get
        {
            return ApplicationManager.Instance.Settings;
        }
    }

    public List<Transform> Build(GameView _gameView, ObjectPool dogsPool)
    {
        gameView = _gameView;
        List<Transform> dogs = new List<Transform>();

        for (int i = 0; i < Settings.DogsCount; i++)
        {
            GameObject go = dogsPool.GetObject();
            if (go != null)
            {
                go.name = "Dog " + i;
                Vector2 newPos = GetDogPosition();
                go.transform.position = new Vector3(newPos.x, newPos.y, go.transform.position.z);
                go.SetActive(true);
                dogs.Add(go.transform);
            }
            else
            {
                Debug.LogError("Not enough objects to spawn dogs");
            }
        }
        return dogs;
    }

    private Vector2 GetDogPosition()
    {
        Vector2 newPos = Vector2.zero;
        bool isCrossing = true;
        var allowedRad = Settings.paddlocGeneratekRadius + Settings.dogGenerateRadius;
        while (isCrossing)
        {
            newPos = gameView.GetRandowmPosition();
            isCrossing = (newPos - (Vector2)gameView.Paddlock.position).magnitude <= allowedRad;
        }
        return newPos;
    }
}
