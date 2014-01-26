using UnityEngine;
using System.Collections;

public class FixedFollowCam : MonoBehaviour 
{
	private Vector3 initalOffset;
	// Use this for initialization
	void Start () 
	{
		// Find the dude and stick to him like glue!
		if ( Dude.player != null )
		{
			initalOffset =  transform.position - Dude.player.transform.position;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Dude.player != null )
		{
			transform.position = Dude.player.transform.position + initalOffset;
		}

	}
}
