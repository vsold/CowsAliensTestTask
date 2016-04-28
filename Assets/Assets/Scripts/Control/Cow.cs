using UnityEngine;

public class Cow : MonoBehaviour
{
    private MovementComponent followMovementComponent;

    public bool InPaddock {get; set;}

    public MovementComponent MoveComp
    {
        get; 
        private set;
    }

    private GameSettings Settings
    {
        get
        {
            return ApplicationManager.Instance.Settings;
        }
    }

    private void Start()
    {
        MoveComp = gameObject.AddComponent<MovementComponent>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (InPaddock)
            return;

        switch (other.tag)
        {
            case "Paddlock":
                GetInPaddlock(other);
                break;
            case "Dog":
                SetLeader(other.gameObject);
                break;
        }
    }
     
    private void SetLeader(GameObject go)
    {
        if (followMovementComponent != null)
            return;

        followMovementComponent = go.GetComponent<MovementComponent>();

        if (followMovementComponent == null)
            return;

        followMovementComponent.NewMove += OnNewDirection;
        MoveComp.Move(followMovementComponent.Target - followMovementComponent.transform.position, Settings.UnitMoveSpeed);
    }

    private void GetInPaddlock(Collider2D _paddlock)
    {
        if (InPaddock)
            return;

        InPaddock = true;
        followMovementComponent.NewMove -= OnNewDirection;
        followMovementComponent = null;

        Vector3 position = new Vector2();
        Vector2 correction = new Vector2(_paddlock.bounds.size.x * 0.3f, _paddlock.bounds.size.y * 0.3f);
        position.x = Random.Range(_paddlock.bounds.min.x + correction.x, _paddlock.bounds.max.x - correction.x);
        position.y = Random.Range(_paddlock.bounds.min.y + correction.y, _paddlock.bounds.max.y - correction.y);
        position.z = transform.position.z;

        MoveComp.Move(position - transform.position, Settings.UnitMoveSpeed);

        ApplicationManager.Instance.GameControl.NewCowCollected();
    }

    private void OnNewDirection(Vector3 direction)
    {
        MoveComp.Move(direction, Settings.UnitMoveSpeed);
    }
}
