//handleMenu.js

/*handleMenu is the logic for the project Caesar menu scene. It handles creating quick and dirty buttons to:
a) start a new game, b) display game instructions, c) display game credits, d) exit the game*/

//-----the global vars-----//

var buttonWidth = 200;
var buttonHeight= 50;
var buttonSpacing = 10;

function OnGUI(){
//called every frame

	//GUI.Button(Rect(0,0,200,100), "Hello!");
	/*GUILayout.BeginArea(Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - spacing, buttonWidth, buttonHeight), "New Game 1");
		if(GUILayout.Button("New Game", GUILayout.Height(50))){
			Application.LoadLevel("FirstTestLevel");
			}
			GUILayout.Space(25);
		if(GUILayout.Button("Instructions", GUILayout.Height(50))){
			Application.LoadLevel("Instructions");
			}
			GUILayout.Space(25);
		if(GUILayout.Button("Credits", GUILayout.Height(50))){
			Application.LoadLevel("Credits");
			}
			GUILayout.Space(25);
		if(GUILayout.Button("Exit", GUILayout.Height(50))){
			Debug.Log("Clicking this button will exit the game in the compiled build");
			Application.Quit();
			}
			GUILayout.Space(25);*/
			
	GUILayout.BeginArea(Rect(15,30, 100,500));//Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 -200, buttonWidth, 400));
		if(GUILayout.Button("New Game", GUILayout.Height(buttonHeight))){
			//Application.LoadLevel("FirstTestLevel"); //for now, go to Russell's test  level
			Application.LoadLevel("levelOne");
			}
		GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Instructions", GUILayout.Height(buttonHeight))){
			Application.LoadLevel("Instructions");
			}
		GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Credits", GUILayout.Height(buttonHeight))){
			Application.LoadLevel("Credits");
			}
			GUILayout.Space(buttonSpacing);
		if(GUILayout.Button("Exit", GUILayout.Height(buttonHeight))){
			Debug.Log("Clicking this button will exit the game in the compiled build");
			Application.Quit();
			}
	GUILayout.EndArea();
			
}