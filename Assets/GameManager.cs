using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public AudioSource deadSound;
    public static boolean gameEnded = false;

	// Use this for initialization
	void Start () {
        Dude.playerDies += () =>
        {
            Debug.Log("YOU DIED");
            gameEnded = true;
            if (deadSound != null)
            {
                deadSound.Play();
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameEnded)
        {
            // check if only player remains
            if (Dude.allDudes.Count == 1 && Dude.player != null)
            {
                if (PlayerStats.kills > 0)
                {
                    if (PlayerStats.kills * 1.0f / Dude.totalDudes > 0.5f)
                    {
                        Debug.Log("AGGRESSIVE ENDING");
                        gameEnded = true;
                        // aggressive ending
                    }
                    else
                    {
                        Debug.Log("OPPORTUNIST ENDING");
                        gameEnded = true;
                        // opportunist ending
                    }
                }
                else
                {
                    Debug.Log("PASSIVE ENDING");
                    gameEnded = true;
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
                Debug.Log("ONLY ONE FACTION LIVES ENDING");
                gameEnded = true;
                // one faction lives ending
            }
        }
	}
}
