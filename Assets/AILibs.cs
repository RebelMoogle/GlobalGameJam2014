using UnityEngine;
using System.Collections;

// A class for AI logic
public static class AILibs {

    public static FactionType getFactionType(Dude dude)
    {
        switch(dude.tag) {
            case "ROMAN":
                return FactionType.ROMAN;
            case "VITRING":
                return FactionType.VIKING;
            case "ROBIN":
                return FactionType.ROBIN;
        }
        return FactionType.PLAYER;
    }

    public static bool factionLikesPlayer(FactionType faction)
    {
        return GlobalManager.factionOpinion[faction] > 0.3;
    }

    public static bool factionDislikesPlayer(FactionType faction)
    {
        return GlobalManager.factionOpinion[faction] < -0.3;
    }
}
