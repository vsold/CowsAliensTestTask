using UnityEngine;

public class Dog : MonoBehaviour 
{
    private Transform cashedTransform;

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
        cashedTransform = transform;
    }

    public void SetNewMoveTarget(Vector3 target)
    {
        Vector3 direction = target - cashedTransform.position;
        MoveComp.Move(direction, Settings.UnitMoveSpeed);
    }
}
