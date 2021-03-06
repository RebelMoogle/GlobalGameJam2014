﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public AudioSource deadSound;
    public static bool gameEnded = false;
    public string nextLevel = null;

	// Use this for initialization
	void Start () {
        gameEnded = false;
        LevelOver.nextLevel = nextLevel;
        Dude.playerDies += () =>
        {
            if (!gameEnded)
            {
                Debug.Log("YOU DIED");
                LevelOver.won = false;
                gameEnded = true;
                if (deadSound != null)
                {
                    deadSound.Play();
                }
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
                        LevelOver.VictoryType = LevelOver.victoryType.AGGRESSIVE;
                        LevelOver.won = true;
                        gameEnded = true;
                        // aggressive ending
                    }
                    else
                    {
                        Debug.Log("OPPORTUNIST ENDING");
                        LevelOver.VictoryType = LevelOver.victoryType.OPPORTUNIST;
                        LevelOver.won = true;
                        gameEnded = true;
                        // opportunist ending
                    }
                }
                else
                {
                    Debug.Log("PASSIVE ENDING");
                    LevelOver.VictoryType = LevelOver.victoryType.PASSIVE;
                    LevelOver.won = true;
                    gameEnded = true;
                    // passive ending
                }
            }
            // only one faction remains
            var existingFactions = new HashSet<FactionType>();
            foreach (var dude in Dude.allDudes)
            {
                existingFactions.Add(AILibs.getFactionType(dude));
            }
            if (existingFactions.Count == 2)
            {
                foreach (var faction in existingFactions)
                {
                    if (faction != FactionType.PLAYER && !AILibs.factionDislikesPlayer(faction))
                    {
                        Debug.Log("ONLY ONE FACTION LIVES ENDING");
						if (AILibs.factionLikesPlayer(faction)) {
                        	LevelOver.VictoryType = LevelOver.victoryType.HELPFUL;
						} else {
							LevelOver.VictoryType = LevelOver.victoryType.NICEONE;
						}
                        LevelOver.won = true;
                        gameEnded = true;
                    }
                }
                // one faction lives ending
            }
            else
            {
                bool dislikes = false;
                foreach (var faction in existingFactions)
                {
                    if (AILibs.factionDislikesPlayer(faction))
                    {
                        dislikes = true;
                    }
                }
                if (!dislikes)
                {
                    LevelOver.VictoryType = LevelOver.victoryType.NICE;
                    LevelOver.won = true;
                    gameEnded = true;
                }
            }
        }
        else
        {
            LevelOver.kills = PlayerStats.kills;
            if (nextLevel == null || nextLevel.Equals("") || !LevelOver.won)
            {
				StartCoroutine("LoadEnding");
                //Application.LoadLevel("Ending");
            }
            else
            {
				StartCoroutine("LoadLevelOver");
               
            }
        }
	}

	IEnumerator LoadEnding()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel("Ending");
	}

	IEnumerator LoadLevelOver()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel("LevelOver");
	}
}
