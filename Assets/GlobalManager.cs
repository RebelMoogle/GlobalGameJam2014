using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalManager : MonoBehaviour {

    public static Dictionary<string, int> factionKillCounts = new Dictionary<string, int>();
    public static Dictionary<string, int> factionDeathCounts = new Dictionary<string, int>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
}