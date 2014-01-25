using UnityEngine;
using System.Collections;

// handles player input. 
// add listener to events and handle.
public class InputHandler : MonoBehaviour {

	public delegate void EventHandler(GameObject e);

	public event EventHandler MoveLeft;
	public event EventHandler MoveRight;
	public event EventHandler MoveUp;
	public event EventHandler MoveDown;
	public event EventHandler KillAction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float axisVal = Input.GetAxis("Horizontal");
		if(axisVal <= -0.1)
			OnMoveLeft();
		else if (axisVal >= 0.1)
			OnMoveRight();

		axisVal = Input.GetAxis("Vertical");

		if(axisVal <= -0.1)
			OnMoveDown();
		else if(axisVal >= 0.1)
			OnMoveUp();

		if(Input.GetButtonDown("KillAction"))
			OnKillAction();
	}

	void OnMoveLeft ()
	{
		if(MoveLeft != null)
			MoveLeft(this.gameObject);
	}

	void OnMoveRight ()
	{
		if(MoveRight != null)
			MoveRight(this.gameObject);
	}

	void OnMoveDown ()
	{
		if(MoveDown != null)
			MoveDown(this.gameObject);
	}

	void OnMoveUp ()
	{
		if(MoveUp != null)
			MoveUp(this.gameObject);
	}

	void OnKillAction()
	{
		if(KillAction != null)
			KillAction(this.gameObject);
	}
}
