using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public static int kills = 0;

	// Use this for initialization
	void Start () {
        Weapon.playerKilledEnemy += () => { 
            kills++; 
        };
	}
	
	// Update is called once per frame
	void Update () {
	}
}
