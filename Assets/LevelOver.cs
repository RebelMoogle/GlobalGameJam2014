using UnityEngine;
using System.Collections;

public class LevelOver : MonoBehaviour {

    public static string nextLevel = null;

    public enum victoryType
    {
        AGGRESSIVE,
        OPPORTUNIST,
        PASSIVE,
        HELPFUL,
		NICEONE,
        NICE
    };

    public static int kills = 0;
    public static string gameOverDetails = null;
    public static bool won = false;
    public static victoryType VictoryType = victoryType.NICEONE;

	// Use this for initialization
	void Start () {
        var text = this.GetComponent<GUIText>();
        text.text = buildLevelOverString();
	}

    void OnGUI() {
        GUILayout.BeginArea(new Rect(15, 30, 100, 500));
        if (nextLevel != null)
        {
            if(GUILayout.Button("Next Level", GUILayout.Height(50))) {
                Application.LoadLevel(nextLevel);
            }
        }
        GUILayout.EndArea();
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
                    detailString = "You annihilated all \nenemies who stand \nin your way.";
                    break;
                case victoryType.OPPORTUNIST:
                    detailString = "You bided your time and \nstriked when the time \nis right.";
                    break;
                case victoryType.PASSIVE:
                    detailString = "You let the fools destroy \nthemselves.";
                    break;
                case victoryType.HELPFUL:
                    detailString = "You sided with the strong.";
                    break;
                case victoryType.NICE:
                    detailString = "You promoted peace \nbetween the remaining \nfactions.";
                    break;
				case victoryType.NICEONE:
					detailString = "You made peace with the \nremaining faction.";
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
