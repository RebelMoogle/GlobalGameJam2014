using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalManager : MonoBehaviour {

    public static Dictionary<string, int> factionKillCounts = new Dictionary<string, int>();
    public static Dictionary<string, int> factionDeathCounts = new Dictionary<string, int>();
    // stores the opinions of each faction of you.
    // positive > 0, negative < 0
    public static Dictionary<FactionType, float> factionOpinion = new Dictionary<FactionType, float>();
    // TODO: # of times influenced a faction
    // TODO: # of times aggressed? a faction
    // # of times 
    public static int aggressiveness;
    public static bool _isInstatiated = false;

    void Awake()
    {
        instantiate();
    }

    private void instantiate()
    {
        if (!_isInstatiated)
        {
            Dude.playerKilledEnemy += (dude) =>
            {
				var factionType = AILibs.getFactionType(dude);
				if (factionType != FactionType.VIKING) {
					factionOpinion[FactionType.VIKING] += 0.2f;
				}
                if (AILibs.getFactionType(dude) == FactionType.ROMAN)
                {
                    factionOpinion[FactionType.ROMAN] -= 0.5f;
                }
            };
            // initialize opinions
            factionOpinion[FactionType.ROMAN] = 1; 
            factionOpinion[FactionType.VIKING] = -1;
            factionOpinion[FactionType.ROBIN] = 0;
            factionOpinion[FactionType.PLAYER] = 1;
            Dude.dudeDies += (dude) =>
            {
                Debug.Log("faction death: " + dude.tag);
                increment(factionDeathCounts, dude.tag);
            };
            Dude.playerKilledEnemy += (dude) =>
            {
                Debug.Log("faction kill: " + dude.tag);
                increment(factionKillCounts, dude.tag);
            };
            _isInstatiated = true;
        }
    }

	// Use this for initializationm
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void increment(Dictionary<string, int> dict, string key)
    {
        if (!dict.ContainsKey(key))
        {
            dict[key] = 0;
        }
        dict[key] += 1;
    }

    public static void modifyOpinion(FactionType faction, float change)
    {
        factionOpinion[faction] += change;
    }
}