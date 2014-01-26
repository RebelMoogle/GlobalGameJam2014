using UnityEngine;
using System.Collections;

public class AIVitring {

    public static void OnInfluence(Dude dude)
    {
        GlobalManager.worseOpinion(FactionType.VIKING, 0.1f);
    }
}
