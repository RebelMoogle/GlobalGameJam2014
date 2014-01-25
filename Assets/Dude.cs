using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour 
{

	private const float _stoppingDistance = 0.2f;
	private const float _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
	private Vector3 _startingPosition;
	private Vector3 _targetPosition;
	private bool _moving = false;

	// move speed in units per second
	public float _speed = 1.0f;
	private float _journeyLength;
	private float _journeyTime;
	private InputHandler _input;


	// Use this for initialization
	void Start () 
	{
		_targetPosition = transform.position;
		_input = GetComponent<InputHandler>();
		if ( _input != null )
		{
			_input.MoveDown += OnMovementDown;
			_input.MoveUp += OnMovementUp;
			_input.MoveLeft += OnMovementLeft;
			_input.MoveRight += OnMovementRight;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMovement();
	}

	void Kill()
	{

	}

	void Stealth()
	{

	}

	void Influence()
	{

	}

	void OnMovementDown(GameObject e)
	{
		Vector3 target = transform.position + new Vector3(0.0f, 0.0f, -1.0f);
		MoveTowards(target);
		Debug.Log("Down InputRecieved!",this);
	}
	void OnMovementUp(GameObject e)
	{
		Vector3 target = transform.position + new Vector3(0.0f, 0.0f, 1.0f);
		MoveTowards(target);
		Debug.Log(" Up InputRecieved!",this);
	}
	void OnMovementLeft(GameObject e)
	{
		Vector3 target = transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
		MoveTowards(target);
		Debug.Log("Left InputRecieved!",this);
	}
	void OnMovementRight(GameObject e)
	{
		Vector3 target = transform.position + new Vector3(1.0f, 0.0f, 0.0f);
		MoveTowards(target);
		Debug.Log("Right InputRecieved!",this);
	}

	void MoveTowards(Vector3 targetPosition)
	{
		Vector3 dir = targetPosition - transform.position;
		if ( Vector3.SqrMagnitude(dir) > _stoppingDistanceSqr )  
		{
			_targetPosition = targetPosition;
			_journeyLength = Vector3.Magnitude(dir);
			_startingPosition = transform.position;
			_journeyTime = 0.0f;
			_moving = true;
		}
		else
		{
			_moving = false;
		}
	}

	void UpdateMovement()
	{
		if( _moving )
		{
			_journeyTime += Time.deltaTime;
			float distanceCovered = _journeyTime * _speed;
			float fractionCovered = distanceCovered / _journeyLength;
			transform.position = Vector3.Lerp(_startingPosition, _targetPosition, fractionCovered);
			if ( Vector3.SqrMagnitude( transform.position - _targetPosition ) <= _stoppingDistanceSqr )
			{
				_moving = false;
			}
		}
	}
}
