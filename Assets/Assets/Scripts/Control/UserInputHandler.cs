using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    public bool Block { get; set; }
    private Camera cam;
    private GameControl controller;

    private GameSettings Settings
    {
        get 
        {
            return ApplicationManager.Instance.Settings;
        }
    }

    public void Init(GameControl _controller)
    {
        controller = _controller;
    }

    void Awake () 
    {
        cam = Camera.main;
	}

	void Update () 
    {
	    if (Block)
	    {
	        return;
	    }

	    CheckForMouseInput();
    }

    private void CheckForMouseInput()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }

        var point = cam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(point);

        if (collider == null)
        {
            return;
        }

        string tag = collider.gameObject.tag;

        if (string.IsNullOrEmpty(tag) || !Settings.ClickableObjectTags.Contains(tag))
            return;
        controller.OnUserInput(point, collider);
    }
}
