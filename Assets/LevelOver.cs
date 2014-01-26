using UnityEngine;
using System.Collections;

public class LevelOver : MonoBehaviour {

    public enum victoryType
    {
        AGGRESSIVE,
        OPPORTUNIST,
        PASSIVE,
        HELPFUL
    };

    public static int kills = 0;
    public static string gameOverDetails = null;
    public static bool won = false;
    public static victoryType VictoryType = victoryType.PASSIVE;

	// Use this for initialization
	void Start () {
        var text = this.GetComponent<GUIText>();
        text.text = buildLevelOverString();
	}

    public string buildLevelOverString()
    {
        string levelOverString = "";
        levelOverString += (won ? "You won!\n" : "You lost!\n");
        if (won)
        {
            string detailString = "";
            switch (VictoryType)
            {
                case victoryType.AGGRESSIVE:
                    detailString = "You annihilated all enemies who stand in your way.";
                    break;
                case victoryType.OPPORTUNIST:
                    detailString = "You bided your time and striked when the time is right.";
                    break;
                case victoryType.PASSIVE:
                    detailString = "You let the fools destroy themselves.";
                    break;
                case victoryType.HELPFUL:
                    detailString = "You sided with the strong.";
                    break;
            }
            levelOverString += detailString + "\n";
        }
        levelOverString += kills + " kills.";
        return levelOverString;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
