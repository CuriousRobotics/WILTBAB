using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<MeshRenderer>().enabled = false;
		//transform.GetComponent<SpriteRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider source){
		//Debug.Log("Hit... " + source.name);
		if(source.name.Contains("IR")){
			//transform.GetComponent<SpriteRenderer>().enabled = true;
			transform.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	void OnTriggerExit(Collider source){
		//Debug.Log("Exited... " + source.name);
		if(source.name.Contains("IR")){
			//transform.GetComponent<SpriteRenderer>().enabled = false;
			transform.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
