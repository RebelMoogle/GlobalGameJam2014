using UnityEngine;
using System.Collections;

// handles player input. 
// add listener to events and handle.
public class InputHandler : MonoBehaviour {

	public delegate void EventHandler();
	public delegate void KillEventHandler(GameObject e);
	public delegate void DirectionEventHandler(Vector3 direction);
	public delegate void FacingEventHandler(float angle);

	public event EventHandler MoveLeft;
	public event EventHandler MoveRight;
	public event EventHandler MoveUp;
	public event EventHandler MoveDown;
	public event DirectionEventHandler MoveDirection;
	public event FacingEventHandler FacingDirection;
	public event EventHandler NoMove;
	public event KillEventHandler KillAction;

	bool isMoving = false;

		// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool dirInputRecieved = false;
		Vector3 direction = Vector3.zero;

		float axisVal = 0.0f;//Input.GetAxis("Horizontal");
		if ( Input.GetKey(KeyCode.D) )
		{
			dirInputRecieved = true;
			direction.x += 1.0f;
		}
		if ( Input.GetKey(KeyCode.A) )
		{
			dirInputRecieved = true;
            direction.x += -1.0f;
        }
        if( axisVal <= -0.2 ) 
		{
			dirInputRecieved = true;
			direction.x += -axisVal;
		}
		else if ( axisVal >= 0.2 )
		{
			dirInputRecieved = true;
            direction.x += axisVal;
		}

		axisVal = 0.0f;//Input.GetAxis("Vertical");
		if ( Input.GetKey(KeyCode.S) )
		{
			dirInputRecieved = true;
			direction.z += -1.0f;
		}
		if( Input.GetKey(KeyCode.W) )
		{
			dirInputRecieved = true;
            direction.z += 1.0f;
        }
		if( axisVal <= -0.2 ) 
		{
			dirInputRecieved = true;
			direction.z += -axisVal;
		}
		else if( axisVal >= 0.2 ) 
		{
			dirInputRecieved = true;
			direction.z += axisVal;
		}

		if(Input.GetButtonDown("KillAction"))
			OnKillAction();

		if ( dirInputRecieved )
		{
			direction.Normalize();
			OnMoveDirection(direction);
		}
		else
		{
			OnNoMove();
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

	void OnFacingDirection ( float angle )
	{
		if(FacingDirection != null)
			FacingDirection( angle );
	}

	void OnMoveDirection (Vector3 direction)
	{
		if(MoveDirection != null)
			MoveDirection(direction);
	}


	void OnNoMove ()
	{
		if(NoMove != null)
			NoMove();
	}

	void OnKillAction()
	{
		if(KillAction != null)
			KillAction(this.gameObject);
	}

	public static Vector3 PlaneRayIntersection (Plane plane, Ray ray)
	{
		float dist;
		if (plane.Raycast(ray, out dist))
		{
			Debug.DrawRay (ray.origin, ray.direction * dist, Color.green);
			return ray.GetPoint (dist);
		}
		else
		{
			return Vector3.zero;

		}
	}
	
	public static Vector3 ScreenPointToWorldPointOnPlane (Vector3 screenPoint, Plane plane, Camera camera)
	{
		// Set up a ray corresponding to the screen position
		Ray ray  = camera.ScreenPointToRay (screenPoint);
		
		// Find out where the ray intersects with the plane
		return PlaneRayIntersection (plane, ray);
	}
}
