using UnityEngine;
using System.Collections;

public static class AIRoman {

    public static void OnInfluence(Dude dude)
    {
        GlobalManager.betterOpinion(FactionType.ROMAN, 0.1f);
    }

    public static void OnDeath(Dude dude)
    {
    }
}
