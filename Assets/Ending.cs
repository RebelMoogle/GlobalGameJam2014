﻿using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

    private string romansLike = "ROMANS LIKE";
    private string romansNeutral = "ROMANS NEUTRAL";
    private string romansDislike = "ROMANS DISLIKE";

    private string vikingsLike = "VIKINGS LIKE";
    private string vikingsNeutral = "VIKINGS NEUTRAL";
    private string vikingsDislike = "VIKINGS DISLIKE";
    
    private string robinLike = "ROBIN LIKE";
    private string robinNeutral = "ROBIN NEUTRAL";
    private string robinDislike = "ROBIN DISLIKE";

	// Use this for initialization
	void Start () {
        var textMesh = this.GetComponent<TextMesh>();
        textMesh.text += createEndingString();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private string createEndingString() {
        string endingString = "\n\nEnd Results: ";
        // romans
        if (AILibs.factionLikesPlayer(FactionType.ROMAN)) {
            endingString += "\n" + romansLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.ROMAN)) {
            endingString += "\n" + romansDislike + "\n";
        } else {
            endingString += "\n" + romansNeutral + "\n";
        }

        // viking
        if (AILibs.factionLikesPlayer(FactionType.VIKING)) {
            endingString += "\n" + vikingsLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.VIKING)) {
            endingString += "\n" + vikingsDislike + "\n";
        } else {
            endingString += "\n" + vikingsNeutral + "\n";
        }

        // robin
        if (AILibs.factionLikesPlayer(FactionType.ROBIN)) {
            endingString += "\n" + robinLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.ROBIN)) {
            endingString += "\n" + robinDislike + "\n";
        } else {
            endingString += "\n" + robinNeutral + "\n";
        }
        return endingString;
    }
}
