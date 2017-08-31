using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

	GameObject[] irSensors;
	GameObject[] ldrSensors;
	GameObject[] micSensors;
	GameObject[] rgbSensors;

	public static GameObject robotCam;
	public static AudioListener cameraAudioListener;

	public void InitializeRobot(){
		robotCam 					= GameObject.Find("Camera POV");
		cameraAudioListener 		= robotCam.GetComponent<AudioListener>();
		cameraAudioListener.enabled = true;
		Debug.Log("InitializeRobot() - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
		irSensors 		 			= GameObject.FindGameObjectsWithTag("IR");
		ldrSensors 		 			= GameObject.FindGameObjectsWithTag("LDR");
		micSensors 		 			= GameObject.FindGameObjectsWithTag("MIC");
		rgbSensors 		 			= GameObject.FindGameObjectsWithTag("RGB");
		DeactivateSensorsOfType("IR");
		DeactivateSensorsOfType("LDR");
		DeactivateSensorsOfType("MIC");
		DeactivateSensorsOfType("RGB");
    }

    public void DestroySensors(){
    	foreach(GameObject sensor in irSensors){
    		Destroy(sensor);
    	}
		foreach(GameObject sensor in ldrSensors){
    		Destroy(sensor);
    	}
		foreach(GameObject sensor in micSensors){
    		Destroy(sensor);
    	}
		foreach(GameObject sensor in rgbSensors){
    		Destroy(sensor);
    	}
    }

	public void ToggleSensorsOfType(string sensorType){
		if(sensorType == "IR"){
			foreach(GameObject sensor in irSensors){
				sensor.SetActive(!sensor.activeSelf);
				cameraAudioListener.enabled = true;
				Debug.Log("ToggleSensorsOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "LDR"){
			Debug.Log("ToggleSensorsOfType(" + sensorType + ")");
			foreach(GameObject sensor in ldrSensors){
				sensor.SetActive(!sensor.activeSelf);
				Debug.Log(sensor.activeSelf.ToString());
				cameraAudioListener.enabled = true;
				Debug.Log("ToggleSensorsOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "MIC"){
			foreach(GameObject sensor in micSensors){
				sensor.SetActive(!sensor.activeSelf);
				cameraAudioListener.enabled = !cameraAudioListener.enabled;
				Debug.Log("ToggleSensorsOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "RGB"){
			foreach(GameObject sensor in rgbSensors){
				sensor.SetActive(!sensor.activeSelf);
				cameraAudioListener.enabled = true;
				Debug.Log("ToggleSensorsOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}
	}

	public void DeactivateSensorsOfType(string sensorType){
		if(sensorType == "IR"){
			foreach(GameObject sensor in irSensors){
				sensor.SetActive(false);
				cameraAudioListener.enabled = true;
				//Debug.Log("DeactivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "LDR"){
			foreach(GameObject sensor in ldrSensors){
				sensor.SetActive(false);
				cameraAudioListener.enabled = true;
				//Debug.Log("DeactivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "MIC"){
			foreach(GameObject sensor in micSensors){
				sensor.SetActive(false);
				cameraAudioListener.enabled = true;
				//Debug.Log("DeactivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
				GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = false;
			}
		}else if(sensorType == "RGB"){
			foreach(GameObject sensor in rgbSensors){
				sensor.SetActive(false);
				cameraAudioListener.enabled = true;
				//Debug.Log("DeactivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}
	}

	public void ActivateSensorsOfType(string sensorType){
		if(sensorType == "IR"){
			foreach(GameObject sensor in irSensors){
				sensor.SetActive(true);
				cameraAudioListener.enabled = true;
				//Debug.Log("ActivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "LDR"){
			Debug.Log("ActivateSensorsOfType(" + sensorType + ")");
			foreach(GameObject sensor in ldrSensors){
				sensor.SetActive(true);
				//Debug.Log(sensor.activeSelf.ToString());
				cameraAudioListener.enabled = true;
				//Debug.Log("ActivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}else if(sensorType == "MIC"){
			foreach(GameObject sensor in micSensors){
				sensor.SetActive(true);
				cameraAudioListener.enabled = false;
				//Debug.Log("ActivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
				GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = true;
			}
		}else if(sensorType == "RGB"){
			foreach(GameObject sensor in rgbSensors){
				sensor.SetActive(true);
				cameraAudioListener.enabled = true;
				//Debug.Log("ActivateSensorOfType(" + sensorType + ") - cameraAudioListener.enabled? " + cameraAudioListener.enabled);
			}
		}
	}
}
