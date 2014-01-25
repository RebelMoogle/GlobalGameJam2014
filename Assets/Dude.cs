using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dude : MonoBehaviour 
{

    // configurable parameters
    public float jitterMagnitude = 0.05f; // maximum jitter range
    public float detectPlayerRange = 1f; // range that an enemyDetects a player
    // range that two members of the same faction has to be in to swarm.
    public float swarmRange = 2f;

	private static List<Dude> _allDudes;
    public static Dude player;

	private const float _stoppingDistance = 0.2f;
	private const float _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
	private Vector3 _startingPosition;
	private Vector3 _targetPosition;
	private bool _moving = false;

	// Attacking stuff
	public float _attackDistance = 2.5f;
	public float _attackDelay = 0.3f;
	// default the time to last attack at the attack delay so the character can attack as soon as the game starts without delay.
	private float _timeSinceLastAttack = 0.3f; 
	private const float _attackDuration = 0.3f;
	private float _attackTimer = 0.0f;

    public enum faction
    {
        RED,
        BLUE
    }

    public enum action {
        IDLE,
        RUNNING_IN_CIRCLES,
        MOVE_TOWARD_PLAYER,
        SWARM
    }

	// There can only be one! (Or two?)?
	public bool isPlayer;

    // action if not a player
    public action Action = action.IDLE;
    // faction the player belongs to
    public faction Faction = faction.BLUE;

	// move speed in units per second
	public float _speed = 1.0f;
	private float _journeyLength;
	private float _journeyTime;
	private InputHandler _input;

	public GameObject _weaponPrefab;
	private Weapon _weapon;

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

		// Load the weapon!
		if ( _weaponPrefab != null )
		{
			GameObject weaponGO = (GameObject)Instantiate(_weaponPrefab);
			if ( weaponGO != null )
			{
				_weapon = weaponGO.GetComponent<Weapon>();
				if ( _weapon != null )
				{
					_weapon.transform.parent = transform;
					_weapon.transform.forward = transform.forward;
					_weapon.transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);

					_weapon.gameObject.SetActive(false);
				}
				else
				{
					Debug.LogError("[Dude] Weapon prefab does not contain a weapon script", this);
				}
			}
			else
			{
				Debug.LogError("[Dude] Failed to loadweapon prefab", this);
			}
		}
		else
		{
			Debug.Log("[Dude] Weapon prefab not set on " + this.name + " ", this);
		}

        setStyle();
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
                _input.KillAction += OnKillAction;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (!isPlayer)
        {
            AI();
        }
		UpdateAttacking();
	}

    void FixedUpdate()
    {
		UpdateMovement();
    }

	void StartAttacking()
	{
		if ( _weapon != null )
		{
			if ( !_weapon.gameObject.activeInHierarchy )
			{
				if ( _timeSinceLastAttack > _attackDelay )
				{
					_attackTimer = 0.0f;
					_weapon.gameObject.SetActive(true);
				}
			}
		}
	}
	void UpdateAttacking()
	{
		if( _weapon != null )
		{
			if ( _weapon.gameObject.activeInHierarchy )
			{
				// Attacking!
				if ( _attackTimer < _attackDuration )
				{
					_attackTimer += Time.deltaTime;
				}
				else
				{
					// Done attacking
					_weapon.gameObject.SetActive(false);
					_timeSinceLastAttack = 0.0f;

				}
			}
			else
			{
				// Done attacking!
				_timeSinceLastAttack += Time.deltaTime;
			}
		}
		//else no weapon

	}

	void Influence()
	{
		
	}

	void Stealth()
	{

	}

    void AI()
    {
        // here's my rudimentary AI
        // if the player is close, move towards him.
        // otherwise, swarm!
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < detectPlayerRange)
        {
            Action = action.MOVE_TOWARD_PLAYER;
        }
        else
        {
            Action = action.SWARM;
        }

        // Attack if close enough

        if (Dude.player != null)
        {
            if (Vector3.Distance(transform.position, Dude.player.transform.position) < _attackDistance)
            {
                StartAttacking();
            }
        }

        // actual actions enacted
        switch (Action)
        {
            case action.RUNNING_IN_CIRCLES:
                RunInCircles();
                break;
            case action.MOVE_TOWARD_PLAYER:
                MoveTowardsPlayer();
                break;
            case action.SWARM:
                SwarmToFaction();
                break;
            default: // idle 
                Idle();
                break;
        }
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

    void OnKillAction(GameObject e)
    {
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
            // add jitter if an AI
            if (!isPlayer)
            {
                addJitter();
            }
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
			if ( Dude.player != null )
			{
            	MoveTowards(Dude.player.transform.position);
			}
        }
    }

    // swarming
    void SwarmToFaction()
    {
        if (!_moving)
        {
            // basically just swarming to the closes faction
            Vector3 closestAllyPosition = Vector3.zero;
            // minimum distance before swarming
            float closestAllyDist = 3f;
            foreach (var dude in _allDudes)
            {
                if (Faction == dude.Faction && dude != this)
                {
                    var dist = Vector3.Distance(transform.position, dude.transform.position);
                    if (closestAllyDist == -1f || dist < closestAllyDist)
                    {
                        closestAllyPosition = dude.transform.position;
                    }
                }
            }
            if (closestAllyPosition != Vector3.zero)
            {
                MoveTowards(closestAllyPosition);
            }
            else
            {
                Idle();
            }
        }
    }

    // idle
    void Idle()
    {
        addJitter();
    }

    internal void OnReceivedAttack()
    {
       Destroy(gameObject);
    }

    void OnDestroy()
    {
        _allDudes.Remove(this);
    }
    // grab material applied
    // material should just use a color 
    // 

    void setStyle()
    {
        Color objectColor = Color.white;
        switch(Faction) {
            case faction.BLUE:
                objectColor = Color.blue;
                break;
            case faction.RED:
                objectColor = Color.red;
                break;
        }
        renderer.material.color = objectColor;
    }

    void addJitter()
    {
        // jitter
        Vector3 jitter = new Vector3(Random.RandomRange(-jitterMagnitude, jitterMagnitude), 0,
                                     Random.RandomRange(-jitterMagnitude, jitterMagnitude));
        transform.position += jitter;
    }
}
