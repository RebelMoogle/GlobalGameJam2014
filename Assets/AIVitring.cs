using UnityEngine;
using System.Collections;

public static class AIVitring {

    public static void OnInfluence(Dude dude)
    {
        GlobalManager.modifyOpinion(FactionType.VIKING, -0.1f);
    }

    public static void OnDeath(Dude dude) {
    }
}
