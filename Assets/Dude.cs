using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dude : MonoBehaviour 
{

	private static List<Dude> _allDudes;
    public static Dude player;

	private const float _stoppingDistance = 0.2f;
	private const float _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
	private Vector3 _startingPosition;
	private Vector3 _targetPosition;
	private bool _moving = false;

	// Attacking stuff
	private const float _attackDistance = 1.0f;

    public enum action {
        IDLE,
        RUNNING_IN_CIRCLES,
        MOVE_TOWARD_PLAYER
    }

	// There can only be one! (Or two?)?
	public bool isPlayer;

    // action if not a player
    public action InitialState = action.IDLE;

	// move speed in units per second
	public float _speed = 1.0f;
	private float _journeyLength;
	private float _journeyTime;
	private InputHandler _input;

	void Awake()
	{
		if ( _allDudes == null )
		{
			_allDudes = new List<Dude>();
		}
		_allDudes.Add (this);
        if (this.isPlayer)
        {
            player = this;
        }
	}


	// Use this for initialization
	void Start () 
	{
		_targetPosition = transform.position;

		// Only the player needs to hook up the input controller
		if ( isPlayer )
		{
			_input = GetComponent<InputHandler>();
			if ( _input != null )
			{

				_input.MoveDown += OnMovementDown;
				_input.MoveUp += OnMovementUp;
				_input.MoveLeft += OnMovementLeft;
				_input.MoveRight += OnMovementRight;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMovement();
        if (!isPlayer)
        {
            switch (InitialState)
            {
                case action.RUNNING_IN_CIRCLES:
                    RunInCircles();
                    break;
                case action.MOVE_TOWARD_PLAYER:
                    MoveTowardsPlayer();
                    break;
            }
        }
	}

	void Attack()
	{
		//transform.forward 
	}

	void Influence()
	{
		
	}

	void Stealth()
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
			transform.forward = _targetPosition - transform.position;
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

    // RUNNING_IN_CIRCLES-related data
    private const float _squareSize = 5.0f;
    private static Vector3[] _runRoute = { 
        new Vector3(0.0f, 0.0f, -1 * _squareSize), 
        new Vector3(-1 * _squareSize, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f,  _squareSize), 
        new Vector3(1 * _squareSize, 0.0f, 0.0f)
    };

    // the current run direction
    private int _runRouteDirection = 0;

    void RunInCircles()
    {
        if (!_moving)
        {
            _runRouteDirection += 1;
            if (_runRouteDirection == _runRoute.Length)
            {
                _runRouteDirection = 0;
            }
            Vector3 target = transform.position + _runRoute[_runRouteDirection];
            MoveTowards(target);
        }
    }

    void MoveTowardsPlayer()
    {
        if (!_moving)
        {
            MoveTowards(Dude.player.transform.position);
        }
    }

    internal void OnReceivedAttack()
    {
        Destroy(gameObject);
    }
}
