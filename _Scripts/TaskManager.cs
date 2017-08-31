using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TaskManager : MonoBehaviour {

	Canvas taskCanvas;
	public GameObject robotPrefab;
	GameObject robotObject;
	RobotController robotController;
	RobotManager robotManager;
	GameObject environment;
	public GameObject envLightPrefab;
	GameObject envLightObject;
	public GameObject envWallPrefab;
	GameObject[] envWalls;

	// Use this for initialization
	void Start () {
		taskCanvas	 = GameObject.Find("Canvas").GetComponent<Canvas>();
		
		environment  = GameObject.Find("Environment");
		//robotObject  = GameObject.Find("Robot");
		robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();
		GameObject.Find("Camera POV").transform.GetComponent<AudioListener>().enabled = true;
		//Destroy(robotObject);
		robotManager.InitializeRobot();
		//robotObject.SetActive(false);
		//EnterTask();
		//ResetRobotObject();
		//robotObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterTask(){
		Vector3 envLightPos;
		Debug.Log("EnterTask()");

		if(SceneManager.GetActiveScene().name == "Task1"){//Or do this by tags?
			ResetRobotObject();
			robotManager.DeactivateSensorsOfType("IR");
			robotManager.DeactivateSensorsOfType("LDR");
			robotManager.DeactivateSensorsOfType("MIC");
			robotManager.DeactivateSensorsOfType("RGB");
			Debug.Log("Deactivated all Sensor Types");

			Debug.Log("Activating LDR, and LDR only...!");
			robotManager.ActivateSensorsOfType("LDR");
			envLightObject 	 				  = GameObject.Instantiate(envLightPrefab, environment.transform);
			envLightPos 	  				  = envLightObject.transform.position;
			envLightPos.x      				  = 0.0f;
			envLightObject.transform.position = envLightPos;
		}else if(SceneManager.GetActiveScene().name == "Task2"){
			ResetRobotObject();
			robotManager.DeactivateSensorsOfType("IR");
			robotManager.DeactivateSensorsOfType("LDR");
			robotManager.DeactivateSensorsOfType("MIC");
			robotManager.DeactivateSensorsOfType("RGB");
			Debug.Log("Deactivated all Sensor Types");

			robotManager.ActivateSensorsOfType("MIC");
			robotManager.ActivateSensorsOfType("IR");
		}else if(SceneManager.GetActiveScene().name == "Task3"){
			ResetRobotObject();
			robotManager.DeactivateSensorsOfType("IR");
			robotManager.DeactivateSensorsOfType("LDR");
			robotManager.DeactivateSensorsOfType("MIC");
			robotManager.DeactivateSensorsOfType("RGB");
			Debug.Log("Deactivated all Sensor Types");

			//robotManager.ActivateSensorsOfType("IR");
			TaskThreeWalls();
			robotManager.ActivateSensorsOfType("RGB");
		}else if(SceneManager.GetActiveScene().name == "Task4"){
			robotManager.ActivateSensorsOfType("LDR");
			robotManager.ActivateSensorsOfType("IR");
			robotManager.ActivateSensorsOfType("MIC");
			robotManager.ActivateSensorsOfType("RGB");
		}
		taskCanvas.enabled 				  = false;
	}

	public void ResetRobotObject(){
		Vector3 robotPosition     = Vector3.zero;
		Quaternion robotRotation  = Quaternion.identity;
		Quaternion cameraRotation = Quaternion.identity;
		cameraRotation.x 		  = 90f;
		if(robotObject != null){
			robotManager.DestroySensors();
			Destroy(robotObject);
			RobotManager.robotCam.transform.SetParent(null);
			RobotManager.cameraAudioListener.enabled = true;
			Debug.Log("ResetRobotObject() - cameraAudioListener.enabled? " + RobotManager.cameraAudioListener.enabled);
			//RobotController.initialized = false;
		}
		robotObject 	= GameObject.Instantiate(robotPrefab, robotPosition, robotRotation);
		robotController = robotObject.GetComponent<RobotController>();
		robotManager.InitializeRobot();

		RobotManager.robotCam.transform.SetParent(robotObject.transform);
		RobotManager.robotCam.transform.position = robotObject.transform.position + new Vector3(0f, 750f, 0f);
		RobotManager.robotCam.transform.rotation = robotObject.transform.rotation;
		RobotManager.robotCam.transform.Rotate(new Vector3(90f, 0f, 0f));
		Debug.Log(RobotManager.robotCam.name + " has parent " + RobotManager.robotCam.transform.parent.name);
		//Debug.Log("ResetRobotObject(): position - " + robotObject.transform.position.ToString() + " orientation - " + robotObject.transform.rotation.ToString());
		//robotObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
	}

	void TaskThreeWalls(){
		int i = new int();
		Vector3 wallPosition = new Vector3();
		Quaternion wallRotation = new Quaternion();
		float wallAngle = new float();
		float radius    = (50f * 32f)/(2f * Mathf.PI);
		envWalls = new GameObject[32];

		Renderer wallRenderer = new Renderer();
		Light wallLight = new Light();
		Color wallColor = Color.red;

		for(i = 0; i < 32; i++){
			wallAngle = (i * 360/32);// * Mathf.Deg2Rad;
			wallRotation = Quaternion.identity;
			wallPosition = Vector3.zero;
			envWalls[i] = Instantiate(envWallPrefab, wallPosition, wallRotation);
			envWalls[i].transform.SetParent(environment.transform);
			//wallRotation.y = wallAngle;
			//wallPosition.x = Mathf.Cos(wallAngle) * radius;
			//wallPosition.z = Mathf.Sin(wallAngle) * radius;
			envWalls[i].transform.RotateAround(wallPosition, Vector3.up, wallAngle);
			envWalls[i].transform.Translate(Vector3.forward * radius);
			envWalls[i].transform.Translate(Vector3.up * 70.5f);

			wallLight = envWalls[i].transform.Find("WallLight").transform.GetComponent<Light>();
			wallLight.enabled = false;
			//Figure out how to change color programmatically using the MeshRenderer, or some other means.
			//start pure red,
			//0-5
			if(i >= 0 && i <= 5){
				wallColor.b += 255f * (i/5f);
				wallLight.color = wallColor;
			}
			//add blue, 
			//6-11
			else if(i >= 6 && i <= 11){
				wallColor.r -= 255f * ((i-6f)/5f);
				wallLight.color = wallColor;
			}
			//then subtract red, 
			//12-17
			else if(i >= 12 && i <= 17){
				wallColor.g += 255f * ((i-12)/5f);
				wallLight.color = wallColor;
			}
			//then add green, 
			//18-23
			else if(i >= 18 && i <= 23){
				wallColor.b -= 255f * ((i-18f)/5f);
				wallLight.color = wallColor;
			}
			//then subtract blue, 
			//24-29
			else if(i >= 24 && i <= 29){
				wallColor.r += 255f * ((i-24f)/5f);
				wallLight.color = wallColor;
			}
			//then add red, 
			//30-32
			else if(i >= 30 && i <= 32){
				wallColor.g -= 255f * ((i-30)/2f);
				wallLight.color = wallColor;
			}
			//then subtract green
			//wallLight.color = Color.white * new Vector4(32f-(i/32f), i/32f, i/32f, 1f);
			//Therefore, there is no "color" component to be altered.  Could shine lights on the walls?
		}
	}
}
