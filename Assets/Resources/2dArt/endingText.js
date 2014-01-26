#pragma strict

function Start () {
	var Roman = 100;
	var Viking = 50;
	var Gaul = 10;
	//determine the player's faction ranking 
	
	var loveFaction = "";
	var midFaction = "";
	var hateFaction="";
	
	
	if(Roman < Viking){//assign Romans to a rating
		if(Roman < Gaul){
			hateFaction = 'Romans';
		} else {
			midFaction = 'Romans';
		}
	} else {
		loveFaction = 'Romans';
	}
	
	if(Viking < Roman){//assign Vikings to a rating
		if(Viking < Gaul){
			hateFaction = 'Vikings';
		} else {
			midFaction = 'Vikings';
		}
	} else {
		loveFaction = 'Vikings';
	}
	
	if(Gaul < Viking){ //assign Gauls to a faction
		if(Gaul < Roman){
			hateFaction = 'Gauls';
		} else {
			midFaction = 'Gauls';
		}
	} else {
		loveFaction = 'Gauls';
	}
	
	//fill in the "Mad Libs" text based on who liked you the most
	var endingMessage = "\n\nEnd Results: \nThe "+loveFaction+" loved you and will always remember your name.\nThe "+midFaction+" didn't suffer too much under your rule, but resent foreign control. \nThe "+hateFaction+", however, will always hate you.";
	
	GetComponent(TextMesh).text += endingMessage;
}


function Update () {

}