using UnityEngine;
using System.Collections;

public class KillParticleSystem : MonoBehaviour {

	float lifeTime;
		

	// Use this for initialization
	void Start () {
		lifeTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{

		lifeTime += Time.deltaTime;
		if(lifeTime >= this.GetComponent<ParticleSystem>().duration);
		{
			Destroy(gameObject);
		}
	}
}
