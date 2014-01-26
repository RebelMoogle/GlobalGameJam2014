using UnityEngine;
using System.Collections;

public enum FactionType
{
	ROMAN,
	VIKING
}

public class Faction : MonoBehaviour {

	[SerializeField]
	FactionType currentType = FactionType.ROMAN;

	public FactionType CurrentType {
		get {
			return currentType;
		}
	}

	string currentTag;

	public GameObject romanFab;
	public GameObject vitringFab;

	GameObject currentClone = null;

	// Use this for initialization
	void Start () 
	{
		//if no faction, set random tag
		//if(currentType = null)

		// check if tag is ROMAN or VITRING

		// TODO: add ourselves to changefaction event in commander!

		SetFaction(currentType);

	}
	
	// Update is called once per frame
	void Update () 
	{


	}



	// set tag
	void SetFaction(FactionType newFaction)
	{
//		if(newFaction == currentType)
//			return; // we are already the correct faction, we shall do nothing

		// set tag


		currentType = newFaction;
        if(newFaction == FactionType.ROMAN)
		{
			if(currentClone != null)
				Destroy(currentClone);

			if(romanFab != null)
			{
				currentClone = (GameObject)Instantiate(romanFab, this.transform.position, this.transform.rotation);
				currentClone.transform.parent = transform;
			}

			this.tag = "ROMAN";
			   
		}
		else
		{
			if(currentClone != null)
				Destroy(currentClone);

			if(vitringFab != null)
			{
				currentClone = (GameObject)Instantiate(vitringFab, this.transform.position, this.transform.rotation);
				currentClone.transform.parent = transform;
			}
				
			this.tag = "VITRING";
        }
	}
}
