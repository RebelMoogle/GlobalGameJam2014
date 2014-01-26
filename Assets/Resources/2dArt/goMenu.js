#pragma strict

var buttonHeight = 70;
var buttonWidth = 150;

function Start () {
	
}

function Update () {

}

function OnGUI (){
	if(GUI.Button(Rect(0,0,buttonWidth,buttonHeight), "Back to Menu")){
			Application.LoadLevel("Menu");
			}
}