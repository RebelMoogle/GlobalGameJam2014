using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

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
            dude.OnReceivedAttack();
        }
    }
}
