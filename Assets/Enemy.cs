using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // movement-related components
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

    public enum action {
        IDLE,
        RUNNING_IN_CIRCLES,
        MOVE_TOWARD_PLAYER
    }

    public action InitialState = action.IDLE;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        UpdateMovement();
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

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - transform.position;
        if (Vector3.SqrMagnitude(dir) > _stoppingDistanceSqr)
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

    void UpdateMovement () {
        if (_moving)
        {
            _journeyTime += Time.deltaTime;
            float distanceCovered = _journeyTime * _speed;
            float fractionCovered = distanceCovered / _journeyLength;
            transform.position = Vector3.Lerp(_startingPosition, _targetPosition, fractionCovered);
            if (Vector3.SqrMagnitude(transform.position - _targetPosition) <= _stoppingDistanceSqr)
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
}
