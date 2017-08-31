using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

	public static string tutorialState;
	string currentState;
	GameObject moveButton;
	GameObject lightButton;
	GameObject proximityButton;
	GameObject soundButton;
	GameObject returnButton;
	GameObject menuButton;

	public GameObject robotPrefab;
	GameObject robotObject;
	RobotController robotController;
	RobotManager robotManager;
	Canvas tutorialCanvas;
	Canvas conceptCanvas;
	Canvas locomotionCanvas;
	GameObject environment;
	public GameObject envLightPrefab;
	GameObject envLightObject;

	public GameObject obstaclesTutorial;
	public GameObject otIRAnatomy;
	public GameObject otIRFunctionality;
	public GameObject otTask;
	// Use this for initialization
	void Start () {
		tutorialState 	 = "";
		currentState     = "";
		moveButton 		 = GameObject.Find("MoveButton");
		lightButton 	 = GameObject.Find("LightButton");
		proximityButton  = GameObject.Find("ProximityButton");
		soundButton      = GameObject.Find("SoundButton");
		returnButton	 = GameObject.Find("ReturnButton");
		menuButton		 = GameObject.Find("MenuButton");
		conceptCanvas	 = GameObject.Find("ConceptCanvas").GetComponent<Canvas>();
		tutorialCanvas   = GameObject.Find("TutorialCanvas").GetComponent<Canvas>();
		locomotionCanvas = GameObject.Find("LocomotionCanvas").GetComponent<Canvas>();
		environment		 = GameObject.Find("Environment");
		robotManager	 = GameObject.Find("RobotManager").GetComponent<RobotManager>();

		environment.SetActive(false);
		returnButton.SetActive(false);
		//robotController.enabled = false;
		tutorialCanvas.enabled   = false;
		locomotionCanvas.enabled = false;

		obstaclesTutorial.SetActive(false);
		otIRAnatomy.SetActive(false);
		otIRFunctionality.SetActive(false);
		otTask.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(tutorialState == "Locomotion"){
			//Debug.Log(tutorialState + " should be Locomotion");

		}else if(tutorialState == "Light"){
			//Debug.Log(tutorialState + " should be Light");

		}else if(tutorialState == "Sounds"){
			Debug.Log(tutorialState + " should be Sounds");

		}else if(tutorialState == "Obstacles"){
			Debug.Log(tutorialState + " should be Obstacles");

		}else if(tutorialState == "Main"){
			Debug.Log(tutorialState + " should be Main");

		}

	}

	public void EnterTutorialLocomotion(){
		tutorialState = "Locomotion";
		currentState = tutorialState;
		ResetRobotObject();
		robotManager.DeactivateSensorsOfType("IR");
		robotManager.DeactivateSensorsOfType("LDR");
		robotManager.DeactivateSensorsOfType("MIC");
		GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = false;
		//RobotController.reset = true;
		//robotController.enabled = true;
		locomotionCanvas.enabled = true;
		SwitchTutorialButtons();
	}
	public void EnterTutorialLight(){
		tutorialState = "Light";
		currentState = tutorialState;
		ResetRobotObject();
		//RobotController.reset = true;
		//robotController.enabled = true;
		//while(RobotController.initialized == false){Debug.Log("RobotController.initialized? " + RobotController.initialized);}
		Debug.Log("In state " + tutorialState + " with active robot, " + robotObject.name + " using controller " + robotController.name);
		Debug.Log("Activating LDRs....");
		robotManager.ActivateSensorsOfType("LDR");
		envLightObject = GameObject.Instantiate(envLightPrefab, environment.transform);
		SwitchTutorialButtons();
	}
	public void EnterTutorialObstacles(){
		tutorialState = "Obstacles";
		currentState = tutorialState;

		SwitchTutorialButtons();
		returnButton.SetActive(false);
		environment.SetActive(false);
		obstaclesTutorial.SetActive(true);
		otIRAnatomy.SetActive(true);
	}

	public void ObstaclesTutorialTransition(string fromSection){
		switch(fromSection){
			case "IR Anatomy":
				otIRAnatomy.SetActive(false);
				otIRFunctionality.SetActive(true);
				break;
			case "IR Functionality":
				otIRFunctionality.SetActive(false);
				otTask.SetActive(true);
				break;
			case "Obstacle Task Canvas":
				otTask.SetActive(false);
				environment.SetActive(true);
				ResetRobotObject();
				//RobotController.reset = true;
				//robotController.enabled = true;
				robotManager.ToggleSensorsOfType("IR");
				returnButton.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void EnterTutorialSounds(){
		tutorialState = "Sounds";
		currentState = tutorialState;
		//RobotController.reset = true;
		//robotController.enabled = true;
		ResetRobotObject();
		robotManager.ToggleSensorsOfType("MIC");
		SwitchTutorialButtons();
	}

	//First: Should Tutorial Manager be handling all of these things, or should some be
	//offloaded to the other "Tutorial" scripts?
	//Second: When going to a Tutorial, the robot jumps to the position it inhabited from the
	//end of the previous Tutorial even though Returning to Main resets the transform.
	//Third: The Environment does not currently reset either.... Obstacles that were seen by IRs in TutorialObstacles,
	//are still visible if there is a switch back to Main, and then to TutorialLight, for example.
	public void Return(){
		GameObject[] envWalls;
		Debug.Log("Returning from " + currentState);
		SwitchTutorialButtons();
		robotManager.DeactivateSensorsOfType("IR");
		robotManager.DeactivateSensorsOfType("LDR");
		robotManager.DeactivateSensorsOfType("MIC");
		tutorialState = "Main";
		currentState = tutorialState;
		ResetRobotObject();
		Destroy(envLightObject);
		envWalls = GameObject.FindGameObjectsWithTag("Wall");
		foreach(GameObject wall in envWalls){
			wall.transform.GetComponent<MeshRenderer>().enabled = false;
		}
		robotController.enabled = false;
		locomotionCanvas.enabled = false;
	}

	public void OnContinueToMainClick(){
		conceptCanvas.enabled  = false;
		tutorialCanvas.enabled = true;
		tutorialState = "Main";
		environment.SetActive(true);
	}

	public void LoadMenu(){
		SceneManager.LoadScene("Intro");
	}

	public void ResetRobotObject(){
		Vector3 robotPosition     = Vector3.zero;
		Quaternion robotRotation  = Quaternion.identity;
		Quaternion cameraRotation = Quaternion.identity;
		cameraRotation.x 		  = 90f;
		if(robotObject != null){
			robotManager.DestroySensors();
			RobotManager.robotCam.transform.SetParent(null);
			RobotManager.cameraAudioListener.enabled = true;
			//RobotController.initialized = false;
			Destroy(robotObject);
		}
		robotObject 	= GameObject.Instantiate(robotPrefab, robotPosition, robotRotation);
		robotController = robotObject.GetComponent<RobotController>();
		robotManager.InitializeRobot();

		RobotManager.robotCam.transform.SetParent(robotObject.transform);
		RobotManager.robotCam.transform.position = robotObject.transform.position + new Vector3(0f, 500f, 0f);
		RobotManager.robotCam.transform.rotation = robotObject.transform.rotation;
		RobotManager.robotCam.transform.Rotate(new Vector3(90f, 0f, 0f));
		Debug.Log(RobotManager.robotCam.name + " has parent " + RobotManager.robotCam.transform.parent.name);
		//Debug.Log("ResetRobotObject(): position - " + robotObject.transform.position.ToString() + " orientation - " + robotObject.transform.rotation.ToString());
		//robotObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
	}

	void SwitchTutorialButtons(){
		moveButton.SetActive(!moveButton.activeSelf);
		lightButton.SetActive(!lightButton.activeSelf);
		proximityButton.SetActive(!proximityButton.activeSelf);
		soundButton.SetActive(!soundButton.activeSelf);
		returnButton.SetActive(!returnButton.activeSelf);
		menuButton.SetActive(!menuButton.activeSelf);
		//robotController.enabled = !robotController.enabled;
		//robotObject.SetActive(false);
		//robotObject.SetActive(true);
		//ResetRobotObject();
		//locomotionCanvas.enabled = !locomotionCanvas.enabled;
	}
}
