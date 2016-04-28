using System;
using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour
{
    public Vector3 Target { get; set; }
    private float speed = 1f;
    private Transform cashedTransform;
    private Bounds bounds;
    private Bounds selfBounds;
    private Vector2 moveMin;
    private Vector2 moveMax;

    public event Action<Vector3> NewMove = delegate { };
    public event Action Stoped = delegate { };

    private Coroutine moveCoroutine;
    public bool IsMoving { private set; get; }

    private void Awake()
    {
        cashedTransform = transform;
    }

    private void Start()
    {
        var collider = gameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            selfBounds = collider.bounds;
        }

        if (ApplicationManager.Instance == null)
            return;
        bounds = ApplicationManager.Instance.GameView.gameField.bounds;

        moveMin = new Vector2(bounds.min.x + selfBounds.size.x/2, bounds.min.y + selfBounds.size.y/2);
        moveMax = new Vector2(bounds.max.x - selfBounds.size.x/2, bounds.max.y - selfBounds.size.y/2);
    }

    public void Move(Vector3 direction, float _speed)
    {
        speed = _speed;
        Target = cashedTransform.position + new Vector3(direction.x, direction.y, 0f);

        if (moveCoroutine == null)
            moveCoroutine = StartCoroutine(Move());
        IsMoving = true;
        if (NewMove != null)
        {
            NewMove(direction);
        }
    }

    public void Stop()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        if (Stoped != null)
            Stoped();
    }

    private IEnumerator Move()
    {
        while ((cashedTransform.position - Target).magnitude > 0.1f)
        {
            float stepDelta = speed * Time.deltaTime;
            Vector3 newPos = Vector3.MoveTowards(cashedTransform.position, Target, stepDelta);
            if (moveMin.magnitude > 0 && moveMax.magnitude > 0)
            {
                float x = Mathf.Clamp(newPos.x, moveMin.x, moveMax.x);
                float y = Mathf.Clamp(newPos.y, moveMin.y, moveMax.y);
                newPos = new Vector3(x, y, newPos.z);
            }
            transform.position = newPos;
            yield return null;
        }

        cashedTransform.position = Target;
        moveCoroutine = null;
        IsMoving = false;
        Stop();
    }
}
