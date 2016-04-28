using UnityEngine;
using System.Collections;

public class CowIdleMovementControl : MonoBehaviour
{
    private MovementComponent moveComp;
    private const float updateInterval = 5f;
    float curTime = 0f;

    private GameSettings Settings
    {
        get
        {
            return ApplicationManager.Instance.Settings;
        }
    }

	void Start ()
	{
	    moveComp = gameObject.GetComponent<MovementComponent>();
	    GetInterval();
	}

    private void GetInterval()
    {
        curTime = Random.Range(updateInterval * 0.5f, 2f * updateInterval);
    }

    private void Update()
    {
        if (moveComp == null)
            return;

        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            CheckForIdle();
            GetInterval();
        }
    }

    private void CheckForIdle()
    {
        if (!moveComp.IsMoving)
        {
            Vector2 position = Random.insideUnitCircle * 30f;
            Vector3 pos = new Vector3(position.x, position.y, transform.position.z);
            moveComp.Move(pos, Settings.UnitMoveSpeed*0.2f);
        }
    }
}
