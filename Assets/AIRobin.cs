using UnityEngine;
using System.Collections;

public class AIRobin {

    // doesn't like dying
    public static void OnDeath(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROBIN, -0.2f);
    }

    public static void OnInfluence(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROBIN, 0.1f);
    }
}
