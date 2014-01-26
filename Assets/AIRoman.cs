using UnityEngine;
using System.Collections;

public static class AIRoman {

    public static void OnInfluence(Dude dude)
    {
    }

    public static void OnDeath(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.ROMAN, -0.1f);
    }
}
