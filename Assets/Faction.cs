using UnityEngine;
using System.Collections;

public enum FactionType
{
	ROMAN,
	VIKING,
    ROBIN,
    PLAYER
}

public class Faction : MonoBehaviour {

	[SerializeField]
	FactionType currentType = FactionType.ROMAN;

	public FactionType CurrentType {
		get {
			return currentType;
		}
	}

	private string currentTag;

	public GameObject romanFab;
	public GameObject vitringFab;
    public GameObject robinFab;

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
        var dude = GetComponent<Dude>();
		currentType = newFaction;
        if(newFaction == FactionType.ROMAN)
		{
            dude.detectEnemyRange = 2f;
            dude.detectPlayerRange = 3f;
			if(currentClone != null)
				Destroy(currentClone);

			if(romanFab != null)
			{
				currentClone = (GameObject)Instantiate(romanFab, this.transform.position, this.transform.rotation);
				currentClone.transform.parent = transform;
			}

			this.tag = "ROMAN";
			   
		}
		else if(newFaction == FactionType.VIKING)
		{
            dude.detectEnemyRange = 10f;
            dude.detectPlayerRange = 2f;
			if(currentClone != null)
				Destroy(currentClone);

			if(vitringFab != null)
			{
				currentClone = (GameObject)Instantiate(vitringFab, this.transform.position, this.transform.rotation);
				currentClone.transform.parent = transform;
			}
				
			this.tag = "VITRING";
        }
        else if (newFaction == FactionType.ROBIN)
        {
            dude.detectEnemyRange = 0.5f;
            dude.detectPlayerRange = 0.5f;
			if(currentClone != null)
				Destroy(currentClone);

			if(romanFab != null)
			{
				currentClone = (GameObject)Instantiate(robinFab, this.transform.position, this.transform.rotation);
				currentClone.transform.parent = transform;
			}
            this.tag = "ROBIN";
        }
        else
        {
            this.tag = "PLAYER";
        }
	}
}
