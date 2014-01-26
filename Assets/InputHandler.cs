using UnityEngine;
using System.Collections;

// handles player input. 
// add listener to events and handle.
public class InputHandler : MonoBehaviour {

	public delegate void EventHandler();
	public delegate void DirectionEventHandler(Vector3 direction);

	public event EventHandler MoveLeft;
	public event EventHandler MoveRight;
	public event EventHandler MoveUp;
	public event EventHandler MoveDown;
	public event DirectionEventHandler MoveDirection;
	public event EventHandler KillAction;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool inputRecieved = false;
		Vector3 direction = Vector3.zero;

		float axisVal = Input.GetAxis("Horizontal");
		if( axisVal <= -0.1 || Input.GetKeyDown(KeyCode.A) )
		{
			inputRecieved = true;
			direction.x += -1.0f;
		}
		else if ( axisVal >= 0.1 || Input.GetKeyDown(KeyCode.D) )
		{
			inputRecieved = true;
			direction.x += 1.0f;
		}

		axisVal = Input.GetAxis("Vertical");

		if( axisVal <= -0.1 || Input.GetKeyDown(KeyCode.S) )
		{
			inputRecieved = true;
			direction.z += -1.0f;
		}
		else if( axisVal >= 0.1 || Input.GetKeyDown(KeyCode.W) )
		{
			inputRecieved = true;
			direction.z += 1.0f;
		}

		if(Input.GetButtonDown("KillAction"))
			OnKillAction();

		if ( inputRecieved )
		{
			MoveDirection(direction);
		}
	}

	void OnMoveLeft ()
	{
		if(MoveLeft != null)
			MoveLeft();
	}

	void OnMoveRight ()
	{
		if(MoveRight != null)
			MoveRight();
	}

	void OnMoveDown ()
	{
		if(MoveDown != null)
			MoveDown();
	}

	void OnMoveUp ()
	{
		if(MoveUp != null)
			MoveUp();
	}

	void OnMoveDirection (Vector3 direction)
	{
		if(MoveDirection != null)
			MoveDirection(direction);
	}

	void OnKillAction()
	{
		if(KillAction != null)
			KillAction();
	}
}
