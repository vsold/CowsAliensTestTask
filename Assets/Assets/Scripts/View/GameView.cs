using UnityEngine;
using System.Collections.Generic;

public class GameView : MonoBehaviour
{
    [SerializeField]public Collider2D gameField;
    [SerializeField] private ObjectPool cowsPool;
    [SerializeField] private ObjectPool dogsPool;
    [SerializeField] private ObjectPool bonusesPool;
    [SerializeField] private Transform paddlock;

    private List<Transform> dogs;

    public Transform Paddlock 
    {
        get { return paddlock;}
    }

    void Start()
    {
        BuildLevelView();
    }

    private void BuildLevelView()
    {
        CreateDogs();
        CreateCows();
    }

    public Vector2 GetRandowmPosition()
    {
        Vector2 position = new Vector2();
        position.x = Random.Range(gameField.bounds.min.x + 50, gameField.bounds.max.x - 50);
        position.y = Random.Range(gameField.bounds.min.y + 50, gameField.bounds.max.y - 50);
        return position;
    }

    private void CreateDogs()
    {
        DogsView dogsBuilder = new DogsView();
        dogs = dogsBuilder.Build(this, dogsPool);
    }
    
    private void CreateCows()
    {
        CowsView cowsView = new CowsView();
        cowsView.Build(this, cowsPool, dogs);
    }

    public Bonus CreateBonus()
    {
        GameObject go = bonusesPool.GetObject();
        go.name = "Bonus";
        Vector2 newPos = GetRandowmPosition();
        go.transform.position = new Vector3(newPos.x, newPos.y, go.transform.position.z);
        var bonusComp = go.GetComponent<Bonus>();
        bonusComp.Collected += CollectedBonus;
        bonusComp.Destroyed += DestroyedBonus;
        go.SetActive(true);
        bonusComp.Start();
        return bonusComp;
    }

    private void CollectedBonus(Bonus bonus)
    {
        bonus.Collected -= CollectedBonus;
        bonus.Destroyed -= DestroyedBonus;
        bonusesPool.ReturnObject(bonus.gameObject);
    }

    private void DestroyedBonus(Bonus bonus)
    {
        bonus.Collected -= CollectedBonus;
        bonus.Destroyed -= DestroyedBonus;
        bonusesPool.ReturnObject(bonus.gameObject);
    }
}
