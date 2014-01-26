using UnityEngine;
using System.Collections;

public class AIRobin {

    // neutral to influence

    public static void OnDefense(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROBIN, 0.1f);
    }

    // doesn't like dying
    public static void OnDeath(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROBIN, -0.1f);
    }
}
