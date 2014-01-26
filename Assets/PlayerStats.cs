using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public static int kills = 0;

    void Awake()
    {
        kills = 0;
    }

	// Use this for initialization
	void Start () {
        Dude.playerKilledEnemy += (dude) => { 
            kills++; 
        };
	}
	
	// Update is called once per frame
	void Update () {
	}
}
