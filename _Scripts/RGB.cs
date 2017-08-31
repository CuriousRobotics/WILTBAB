using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGB : MonoBehaviour {

	GameObject sphereLED;
	GameObject[] lightObjs;
	List<Light> lights = new List<Light>(4);

	void Start(){
		sphereLED = transform.Find("LED").gameObject;
		sphereLED.SetActive(false);

	}

	void OnTriggerStay(Collider source){
		Color sourceColor;

		//print(this.name + " has been Triggered");
		if(source.tag == "HasColor"){
			GetLEDs();
			//Debug.Log("Trigger HasColor, turn on " + lights.Count + " lights!");
			sourceColor 								   = source.transform.Find("WallLight").transform.GetComponent<Light>().color;//transform.GetComponent<MeshRenderer>().material.color;
			transform.GetComponent<SpriteRenderer>().color = sourceColor;
			sphereLED.SetActive(true);
			foreach(Light light in lights){
				//Debug.Log(light.name + " " + light.color);
				light.color = sourceColor;
			}
			//Debug.Log(source.transform.GetComponent<MeshRenderer>().material.color);
		}
	}

	void OnTriggerExit(Collider source){
		transform.GetComponent<SpriteRenderer>().color = Color.white;
		sphereLED.SetActive(false);
		GetLEDs();
		foreach(Light light in lights){
			light.color = Color.white;
		}
	}

	void GetLEDs(){
		lightObjs = GameObject.FindGameObjectsWithTag("Light");
		foreach(GameObject lightObj in lightObjs){
			lights.Add(lightObj.transform.GetComponent<Light>());
		}
		//Debug.Log(this.name + ": There are " + lights.Count + " lights!");
	}
}
