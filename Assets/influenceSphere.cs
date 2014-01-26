using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class influenceSphere : MonoBehaviour {
	
	public Dude owner;
	public delegate void DeathCallback(Dude dude);
	public static event DeathCallback playerKilledEnemy;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		var dude = collision.collider.GetComponent<Dude>();
		if (dude != null)
		{
			if (owner == Dude.player)
			{
				if (playerKilledEnemy != null) { playerKilledEnemy(dude); }
			}
			dude.OnReceivedAttack();
		}
	}
}

