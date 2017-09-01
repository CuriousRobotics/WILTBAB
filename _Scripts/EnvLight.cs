using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvLight : MonoBehaviour {

	public List<Light> robotLDRs;
	public float distanceFromMe;

	// Use this for initialization
	void Start () {
		InitializeLDRs();
		
		//Debug.Log(robotLDR.name);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Light robotLDR in robotLDRs){
			float intensity = 0;
			//Debug.Log("robotLDR: " + robotLDR.name);
			if(robotLDR != null){
				distanceFromMe = (transform.position - robotLDR.transform.parent.position).magnitude;
				intensity = (1.0f / (distanceFromMe + 0.1f)) * 1000;
				//Debug.Log("Light distance: " + distanceFromMe.ToString() + " Intensity: " + intensity.ToString());
				robotLDR.intensity = intensity;
			}
		}
	}

	public void InitializeLDRs(){
		GameObject[] ldrs;
		ldrs = GameObject.FindGameObjectsWithTag("LDR");
		Debug.Log("ldrs has " + ldrs.Length.ToString() + " sensors in it.");
		foreach(GameObject ldr in ldrs){
			robotLDRs.Add(ldr.transform.Find("Light").GetComponent<Light>());
			Debug.Log("Adding " + ldr.transform.name);
		}
	}
}
