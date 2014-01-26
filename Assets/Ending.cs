using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

    private string romansLike = "You, you are a true leader. As your army, we have traveled far and brought \n" + 
                                "rule and order to these savage lands. We would follow you wherever you would \n" + 
                                "lead and against any foe, even against Rome herself. Hail, Caesar!";

    private string romansNeutral = "We bide our time under your command  dreaming of our lands back home. We followed \n" + 
                                   "you hoping for stories to one day tell our wives and children, but you brought us, \n" + 
                                   "and yourself, neither glory nor shame.";

    private string romansDislike = "You are no true Roman. You pandered to these savages and wasted our lives \n" + 
                                   "fighting against foes who fear nothing because they have nothing! As you should have known, \n" + 
                                   "Caesar, not all who leave Rome on campaign return...";

    private string vikingsLike = "You fight as if wishing to sit at Vahalla among other true warriors! We care little \n" + 
                                 "for your laws, but would join you for the glory alone.";

    private string vikingsNeutral = "You were strangely dressed, and give us neither good trade or good sport. Goodbye, \n" + 
                                    "Roman, we had no use for you here.";

    private string vikingsDislike = "Pah, you offered us only bread and useless coin, but not the chance for a good death! \n" + 
                                    "We would rather celebrate than mourne your death, Roman. If all Rome is as soft as you, we \n" + 
                                    "would take all Rome for our own. In fact, we will it so.";
    
    private string robinLike = "You have given us grain and protected us when we had need. At peace, Roman, and let us call you friend.";

    private string robinNeutral = "Your ways had little to offer us, Roman, but you bore us no ill will, nor did we for you. \n" + 
                                  "Stay or go, it matters not to us.";

    private string robinDislike = "You invade our lands, kill our people, and tell us we should be like you? We would rather \n" + 
                                  "you kill us all than make us care about your death!";

	// Use this for initialization
	void Start () {
        var textMesh = this.GetComponent<TextMesh>();
        textMesh.text += createEndingString();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private string createEndingString() {
        string endingString = "\n\nIn the end, Caesar's conquest ended gloriously: his name was known throughout the land. \n" + 
                              "at his eulogy, representatives from all over the land came to speak.\n\n";
        // romans
        endingString += "\n A Roman representative stepped up to the podium, and he began to speak:\n";
        if (AILibs.factionLikesPlayer(FactionType.ROMAN)) {
            endingString += "\n" + romansLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.ROMAN)) {
            endingString += "\n" + romansDislike + "\n";
        } else {
            endingString += "\n" + romansNeutral + "\n";
        }

        // viking
        endingString += "\n The Viking representative, with his large club slung over his shoulder, said this:\n";
        if (AILibs.factionLikesPlayer(FactionType.VIKING)) {
            endingString += "\n" + vikingsLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.VIKING)) {
            endingString += "\n" + vikingsDislike + "\n";
        } else {
            endingString += "\n" + vikingsNeutral + "\n";
        }

        // robin
        endingString += "\n The Robin representive, speaking almost as quietly as he walked through the crowd:\n";
        if (AILibs.factionLikesPlayer(FactionType.ROBIN)) {
            endingString += "\n" + robinLike + "\n";
        } else if (AILibs.factionDislikesPlayer(FactionType.ROBIN)) {
            endingString += "\n" + robinDislike + "\n";
        } else {
            endingString += "\n" + robinNeutral + "\n";
        }
        endingString += "\nTHE END.";
        return endingString;
    }
}
