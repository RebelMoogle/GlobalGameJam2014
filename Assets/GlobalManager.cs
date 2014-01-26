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

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // initialize opinions
        factionOpinion[FactionType.ROMAN] = 1;
        factionOpinion[FactionType.VIKING] = -1;
        factionOpinion[FactionType.ROBINHOODS] = 0;
        Dude.dudeDies += (dude) =>
        {
            Debug.Log("faction death: " + dude.tag);
            increment(factionDeathCounts, dude.tag);
        };
        Weapon.playerKilledEnemy += (dude) =>
        {
            Debug.Log("faction kill: " + dude.tag);
            increment(factionKillCounts, dude.tag);
        };
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

    public static void worseOpinion(FactionType faction, float magnitude)
    {
    }

    public static void betterOpinion(FactionType faction, float magnitude)
    {
    }
}