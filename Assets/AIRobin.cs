using UnityEngine;
using System.Collections;

public class AIRobin {

    // doesn't like dying
    public static void OnDeath(Dude dude)
    {
		if (!AILibs.factionLikesPlayer(FactionType.ROBIN)) {
			//GlobalManager.modifyOpinion (FactionType.ROBIN, -0.05f);
		}
    }

    public static void OnInfluence(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROBIN, 0.2f);
    }
}
