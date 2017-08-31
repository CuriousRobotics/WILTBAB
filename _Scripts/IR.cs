using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IR : MonoBehaviour {

	public bool hitWall;
	// Use this for initialization
	void Start () {
		hitWall = false;
	}
	
	// Update is called once per frame
	void Update () {
		//print("hitWall? " + hitWall.ToString());
		
	}
	/*
	void OnCollisionEnter(Collision source){
		print(this.name + " has been Triggered");
		if(!source.transform.name.Contains("L16A")){
			print(this.name + " hit " + source.transform.name);
			if(source.transform.name.Contains("Wall")){
				hitWall = true;
			}

		}
	}*/

	void OnTriggerStay(Collider source){
		//print(this.name + " has been Triggered");
		if(source.name.Contains("Wall")){
			hitWall = true;
		}
	}
}
