using UnityEngine;
using System.Collections;

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
            if (PlayerStats.kills > 0)
            {
                // aggressive ending
            }
            else
            {
                // passive ending
            }
        }
        // only one faction remains
        // Dude.faction remainingFaction = null;
	}
}
