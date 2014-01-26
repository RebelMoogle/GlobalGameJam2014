﻿using UnityEngine;
using System.Collections;

public class AIHood : AIBase {

    // neutral to influence

    static void OnDefense(Dude dude)
    {
        GlobalManager.betterOpinion(FactionType.ROBINHOODS, 0.1f);
    }

    // doesn't like dying
    static void OnDeath(Dude dude)
    {
        GlobalManager.worseOpinion(FactionType.ROBINHOODS, 0.1f);
    }
}