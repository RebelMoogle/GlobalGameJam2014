using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public AudioSource deadSound;

	// Use this for initialization
	void Start () {
        Dude.playerDies += () =>
        {
            if (deadSound != null)
            {
                deadSound.Play();
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
        // check if only player remains
        if (Dude.allDudes.Count == 1 && Dude.player != null)
        {
            if (PlayerStats.kills > 0 )
            {
                if (PlayerStats.kills * 1.0f / Dude.totalDudes > 0.5f)
                {
                    // aggressive ending
                }
                else
                {
                    // opportunist ending
                }
            }
            else
            {
                // passive ending
            }
        }
        // only one faction remains
        var existingFactions = new HashSet<Dude.faction>();
        foreach (var dude in Dude.allDudes)
        {
            existingFactions.Add(dude.Faction);
        }
        if (existingFactions.Count > 2)
        {
            // one faction lives ending
        }
	}
}
