using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dude : MonoBehaviour 
{

    // configurable parameters
    public float jitterMagnitude = 0.05f; // maximum jitter range
    public float detectPlayerRange = 1f; // range that an enemyDetects a player
    public float detectEnemyRange = 2f;
    public float playerInfluenceFactor = 5f;
    // range that two members of the same faction has to be in to swarm.
    public float swarmRange = 2f;

    // events
    public static event EventEngine.Event playerDies;
    public delegate void DeathCallback(Dude dude);
    public static event DeathCallback dudeDies;

    public static int totalDudes = 0;
	public static List<Dude> allDudes;
    public static Dude player;
    public static bool playerDied = false;

	private const float _stoppingDistance = 0.2f;
	private const float _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
	private Vector3 _startingPosition;
	private Vector3 _targetPosition;
	private bool _movingTowardsTarget = false;
	private bool _movingInDirection = false;
	private Vector3 _moveDirection;

	// Attacking stuff
	public float _attackDistance = 2.5f;
	public float _attackDelay = 0.3f;
	// default the time to last attack at the attack delay so the character can attack as soon as the game starts without delay.
	private float _timeSinceLastAttack = 0.3f; 
	private const float _attackDuration = 0.3f;
	private float _attackTimer = 0.0f;

	public float _influenceDelay = 0.3f;
	// default the time to last attack at the attack delay so the character can attack
	private float _timeSinceLastInfluence = 0.3f; 
	private const float _influenceDuration = 0.3f;
	private float _influenceTimer = 0.0f;



	bool _receivedInput;

    public enum action {
        IDLE,
        RUNNING_IN_CIRCLES,
        MOVE_TOWARD_PLAYER,
        MOVE_TOWARD_CLOSEST_ENEMY,
        SWARM
    }

	// There can only be one! (Or two?)?
	public bool isPlayer;

	// move speed in units per second
	public float _speed = 1.0f;
	private float _journeyLength;
	private float _journeyTime;
	private InputHandler _input;

	public GameObject _weaponPrefab;
	private Weapon _weapon;
	public GameObject _influencerPrefab;
	private Weapon _influencer;

	void Awake()
	{
		_receivedInput = false;
		if ( allDudes == null )
		{
			allDudes = new List<Dude>();
		}
		allDudes.Add (this);
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
                    _weapon.owner = this;
					_weapon.transform.parent = transform;

                    if (isPlayer) {
                        _weapon.forwardMultiplier = 0.4f;
						_weapon.rightMultiplier = 0.2f;
                    }
					else
					{
						_weapon.forwardMultiplier = 0.0f;
						_weapon.rightMultiplier = 1.0f;
					}

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

	// Load the influencer!
	if ( _influencerPrefab != null )
	{
		GameObject influencerGO = (GameObject)Instantiate(_influencerPrefab);
		if ( influencerGO != null )
		{
			_influencer = influencerGO.GetComponent<Weapon>();
			if ( _influencer != null )
			{
				_influencer.owner = this;
				_influencer.transform.parent = transform;
				_influencer.transform.forward = transform.forward;
				_influencer.transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
				if (isPlayer) {
					// giant influencer
					_influencer.transform.localScale *= 3;
					_influencer.transform.localPosition = 
						_influencer.transform.localPosition += new Vector3(0.0f, 0.0f, 2.0f);
				}
				
				_influencer.gameObject.SetActive(false);
			}
			else
			{
				Debug.LogError("[Dude] Sphere prefab does not contain a weapon script", this);
			}
		}
		else
		{
				Debug.LogError("[Dude] Failed to loadsphere prefab", this);
		}
	}
	else
	{
			Debug.Log("[Dude] Sphere prefab not set on " + this.name + " ", this);
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

				_input.MoveDirection += OnMovementDirection;
				_input.FacingDirection += OnFacingDirection;
                _input.KillAction += OnKillAction;
				_input.InfluenceAction += OnInfluenceAction;
			}
		}

        totalDudes = allDudes.Count;
	}
	
	// Update is called once per frame

	void Update () 
	{
        if (!isPlayer)
        {
          AI();
        }
		UpdateAttacking();
		UpdateInfluence();

		MouseUpdate();
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
					RaycastHit hit;
					Ray ray = new Ray(transform.position + (transform.forward * 0.5f), transform.forward);

					if ( Physics.SphereCast(ray, 0.4f, out hit, 3.0f, 1<<9 ) )
				    {
						Dude dude = hit.collider.GetComponent<Dude>();
						if ( dude != null )
						{
							// don't hit self!
							if ( dude != this )
							{
								dude.OnReceivedAttack();
							}
							
						}
					}
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

	void StartInfluencing()
	{
		if ( _influencer != null )
		{
			if ( !_influencer.gameObject.activeInHierarchy )
			{
				if ( _timeSinceLastInfluence > _influenceDelay )
				{
					_influenceTimer = 0.0f;
					RaycastHit hit;
					Ray ray = new Ray(transform.position + (transform.forward * 0.5f), transform.forward);
					
					if ( Physics.SphereCast(ray, 0.4f, out hit, 3.0f, 1<<9 ) )
					{
						Dude dude = hit.collider.GetComponent<Dude>();
						if ( dude != null )
						{
							// don't hit self!
							if ( dude != this )
							{
								dude.OnReceivedInfluence();
							}
							
						}
					}
					_influencer.gameObject.SetActive(true);
				}
			}
		}
		
	}

	void UpdateInfluence()
	{
		if( _influencer != null )
		{
			if ( _influencer.gameObject.activeInHierarchy )
			{
				// Attacking!
				if ( _influenceTimer < _influenceDuration )
				{
					_influenceTimer += Time.deltaTime;
				}
				else
				{
					// Done attacking
					_influencer.gameObject.SetActive(false);
					_timeSinceLastInfluence = 0.0f;
					
				}
			}
			else
			{
				// Done attacking!
				_timeSinceLastInfluence += Time.deltaTime;
			}
		}
		//else no influencer weapon
		
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
        var faction = AILibs.getFactionType(this);
        var factionOpinion = GlobalManager.factionOpinion[AILibs.getFactionType(this)];
        Dude nearestEnemy = findNearestEnemy(detectEnemyRange);
        bool playerWithinRange = Dude.player != null && Vector3.Distance(transform.position, Dude.player.transform.position) * Mathf.Abs(factionOpinion) < detectPlayerRange * playerInfluenceFactor;
        if (AILibs.factionDislikesPlayer(faction) && playerWithinRange) {
            MoveTowardsPlayer();
        } else if (nearestEnemy != null) {
            MoveTowardsTarget(nearestEnemy.transform.position);
        } else if (AILibs.factionLikesPlayer(faction) && playerWithinRange) {
            MoveTowardsPlayer();
        } else {
            SwarmToFaction();
        }

        // Attack if close enough
        var closestDistance = _attackDistance + 1;
        if (Dude.player != null && AILibs.factionDislikesPlayer(faction)) {
            var playerDistance = Vector3.Distance(transform.position, Dude.player.transform.position) * Mathf.Abs(GlobalManager.factionOpinion[faction]);
            if (playerDistance < closestDistance) {
                closestDistance = playerDistance;
            }
        }

        if (nearestEnemy != null) {
            var playerDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            if (playerDistance < closestDistance) {
                closestDistance = playerDistance;
            }
        }

        if (closestDistance < _attackDistance)
        {
            StartAttacking();
        }
    }

	void OnMovementDown()
	{
		Vector3 target = transform.position + new Vector3(0.0f, 0.0f, -1.0f);
		MoveTowardsTarget(target);
		Debug.Log("Down InputRecieved!",this);
	}
	void OnMovementUp()
	{
		Vector3 target = transform.position + new Vector3(0.0f, 0.0f, 1.0f);
		MoveTowardsTarget(target);
		Debug.Log(" Up InputRecieved!",this);
	}
	void OnMovementLeft()
	{
		Vector3 target = transform.position + new Vector3(-1.0f, 0.0f, 0.0f);
		MoveTowardsTarget(target);
		Debug.Log("Left InputRecieved!",this);
	}
	void OnMovementRight()
	{
		Vector3 target = transform.position + new Vector3(1.0f, 0.0f, 0.0f);
		MoveTowardsTarget(target);
		Debug.Log("Right InputRecieved!",this);
	}

	void OnMovementDirection( Vector3 unitDirection )
	{
		_movingInDirection = true;
		_moveDirection = unitDirection;
	}

	void OnFacingDirection( float angle  )
	{
		FaceDirection( angle );
	}

    void OnKillAction(GameObject e)
    {
		if (isPlayer)
		{
			StartAttacking();
		}
    }

	void OnInfluenceAction(GameObject e)
	{
		if (isPlayer) 
		{
			StartInfluencing();
		}
	}

	void FaceDirection(float angle)
	{
		Debug.Log ("Angle " + angle);
	}

	void MoveTowardsTarget(Vector3 targetPosition)
	{
		Vector3 dir = targetPosition - transform.position;
		if ( Vector3.SqrMagnitude(dir) > _stoppingDistanceSqr )  
		{
			_targetPosition = targetPosition;
			_journeyLength = Vector3.Magnitude(dir);
			_startingPosition = transform.position;
			_journeyTime = 0.0f;
			_movingTowardsTarget = true;
		}
		else
		{
			_movingTowardsTarget = false;
		}
	}

	void UpdateMovement()
	{
		if ( isPlayer )
		{
			if ( _movingInDirection )
			{
				transform.position += (Time.deltaTime * _speed * _moveDirection);
				_movingInDirection = false;
			}
		}
		else
		{
			if( _movingTowardsTarget )
			{
				_journeyTime += Time.deltaTime;
				float distanceCovered = _journeyTime * _speed;
				float fractionCovered = distanceCovered / _journeyLength;
				transform.position = Vector3.Lerp(_startingPosition, _targetPosition, fractionCovered);
                transform.LookAt(_targetPosition);
	            // add jitter if an AI
	            if (!isPlayer)
	            {
	                addJitter();
	            }
				if ( Vector3.SqrMagnitude( transform.position - _targetPosition ) <= _stoppingDistanceSqr )
				{
					_movingTowardsTarget = false;
				}
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
        if (!_movingTowardsTarget)
        {
            _runRouteDirection += 1;
            if (_runRouteDirection == _runRoute.Length)
            {
                _runRouteDirection = 0;
            }
            Vector3 target = transform.position + _runRoute[_runRouteDirection];
            MoveTowardsTarget(target);
        }
    }

    void MoveTowardsPlayer()
    {
        if (!_movingTowardsTarget)
        {
			if (Dude.player != null)
			{
            	MoveTowardsTarget(Dude.player.transform.position);
			}
        }
    }

    Dude findNearestEnemy(float maxRange)
    {
        Dude nearestEnemy = null;
        foreach (var dude in allDudes)
        {
			if (!dude.CompareTag(tag) && !dude.isPlayer)
            {
                var distance = Vector3.Distance(transform.position, dude.transform.position);
                if (distance < maxRange)
                {
                    maxRange = distance;
                    nearestEnemy = dude;
                }
            }
        }
        return nearestEnemy;
    }

    // swarming
    void SwarmToFaction()
    {
        if (!_movingTowardsTarget)
        {
            // basically just swarming to the closes faction
            Vector3 closestAllyPosition = Vector3.zero;
            // minimum distance before swarming
            float closestAllyDist = 3f;
            foreach (var dude in allDudes)
            {
                if (dude.CompareTag(tag) && dude != this)
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
                MoveTowardsTarget(closestAllyPosition);
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

	internal void OnReceivedInfluence()
	{
        var faction = this.GetComponent<Faction>();
        if (!GameManager.gameEnded && faction != null && faction.onInfluence != null)
        {
            Debug.Log("influence recieved!");
            faction.onInfluence(this);
        }
	}

    void OnDestroy()
    {
        allDudes.Remove(this);
        if (this.isPlayer)
        {
            Dude.player = null;
            if ( playerDies != null )
			{
				playerDies();
			}
        }
        if (dudeDies != null) {
            dudeDies(this);
        }
        var faction = this.GetComponent<Faction>();
        if (!GameManager.gameEnded && faction != null && faction.onDeath != null)
        {
            faction.onDeath(this);
        }
    }

	void LateUpdate()
	{
		_receivedInput = false;
	}


    void addJitter()
    {
        // jitter
        Vector3 jitter = new Vector3(Random.Range(-jitterMagnitude, jitterMagnitude), 0,
                                     Random.Range(-jitterMagnitude, jitterMagnitude));
        transform.position += jitter;
    }

    void MouseUpdate()
    {
		if ( isPlayer )
		{
			Plane plane = new Plane(Vector3.up, transform.position);
	        // On PC, the cursor point is the mouse position
	        Vector3 cursorScreenPosition = Input.mousePosition;
	        
	        // Find out where the mouse ray intersects with the movement plane of the player
	        Vector3 cursorWorldPosition = InputHandler.ScreenPointToWorldPointOnPlane (cursorScreenPosition, plane, Camera.main);
			cursorWorldPosition.y = transform.position.y;
	        Vector3 facingDirection = (cursorWorldPosition - transform.position);
			transform.forward = facingDirection;
		}

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        
//        float ent = 100.0f;
//        if (plane.Raycast(ray, out ent))
//        {
//            Vector3 hitPoint = ray.GetPoint(ent);
//            
//            Debug.DrawRay (ray.origin, ray.direction * ent, Color.green);
//        }
//        else
//        {
//            Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);
//        }
//        
    }
}
