#pragma strict

//global vars

var myHealth = 100;
var myFollowers = 5;
var textHealth : GUIText;
var followerStatus = ['A','B','C'];
var status = 0;

function Start () {

}

function Update () {
	if(Input.GetKeyDown(KeyCode.Q)){
		if(myHealth >0){
			myHealth -= 25;
		}else{
			myHealth = 0;
		}
		
		if(myFollowers < 100){
			myFollowers +=5;
		}else{
			myFollowers = 100;
		}
		
		if(status < 2){
			status +=1;
		}else{
			status = 0;
		}
	} //this is how you decrement the counter. Pass value from Yusuke's health tracking
	textHealth.text = 'Health: '+myHealth +'\nFollowers: '+myFollowers +'\n  Follower Status: '+followerStatus[status];
}