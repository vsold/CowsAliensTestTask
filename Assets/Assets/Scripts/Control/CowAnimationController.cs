using UnityEngine;
using System.Collections;

public class CowAnimationController : MonoBehaviour
{
    private MovementComponent moveComp;
    [SerializeField] private Animator animator;

    private bool isMoving;
    private string direction;

	void Start ()
	{
	    moveComp = gameObject.GetComponent<MovementComponent>();
        if (moveComp == null)
            return;
	    
	    moveComp.Stoped += Stop;
	    moveComp.NewMove += Move;
	}

    private void Move(Vector3 _direction)
    {
        Vector2 dir = (Vector2)_direction;
        dir = dir.normalized;
        int dirX = Mathf.RoundToInt(dir.x);
        int dirY = Mathf.RoundToInt(dir.y);

        if (Mathf.Abs(dirX) > 0)
            dirY = 0;

        isMoving = true;
        animator.SetBool("isMoving", true);
        animator.SetInteger("dirX", dirX);
        animator.SetInteger("dirY", dirY);
    }

    private void Stop()
    {
        isMoving = false;
        animator.SetBool("isMoving", false);
    }
}
