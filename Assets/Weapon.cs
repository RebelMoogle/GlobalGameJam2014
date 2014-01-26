using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Dude owner;
    public delegate void DeathCallback(Dude dude);
    public static event DeathCallback playerKilledEnemy;

	public ParticleSystem swooshParticles;
	public float swingSpeed = 1.0f;
	public float swingAngle = 45.0f;
	public float forwardMultiplier = 0.3f;
	public float rightMultiplier = 0.2f;

	Quaternion startRotation; 
	Vector3 startPosition;


	// Use this for initialization
	void Start () 
	{

		transform.localRotation = Quaternion.AngleAxis(180, transform.right);

		if(transform.parent != null) //if parent present
		{
			//transform.forward = transform.parent.forward;
			transform.position = transform.parent.position + transform.parent.right * forwardMultiplier + transform.parent.forward * rightMultiplier;
		}
    }
    
    // Update is called once per frame
	void Update () 
	{
		//transform.RotateAround(transform.parent.position, transform.right, swingAngle * Time.deltaTime);
	}
	void FixedUpdate()
	{
		// rotate sword towards target, if active.
		//rigidbody.AddTorque(transform.up, ForceMode.Impulse);
	}

	void OnEnable()
	{
		// init particles
		if(swooshParticles != null)
		{
			ParticleSystem particleClone;
			particleClone = (ParticleSystem)Instantiate(swooshParticles, transform.position, transform.rotation);
			particleClone.transform.parent = transform;
		}


		// start rotation
		startRotation = transform.localRotation;
		if(transform.parent != null) // we only work with supervision. :P
			transform.position = transform.parent.position + transform.parent.right * forwardMultiplier + transform.parent.forward * rightMultiplier;
		// target rotation

		//targetRotation = transform.rotation + Quaternion.AngleAxis(swingAngle, transform.right);



	}

	void OnDisable()
	{
		//reset
		transform.localRotation = startRotation;
		//transform.localPosition = startPosition;

	}

    void OnCollisionEnter(Collision collision)
    {
        var dude = collision.collider.GetComponent<Dude>();
        if (dude != null && !dude.CompareTag(owner.tag))
        {
            if (owner == Dude.player || !dude.CompareTag(owner.tag))
            {
                if (playerKilledEnemy != null) { playerKilledEnemy(dude); }
            }
            dude.OnReceivedAttack();
        }
    }
}
