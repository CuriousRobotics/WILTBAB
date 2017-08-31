using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicSensor : MonoBehaviour {

	private static float MAX_DISTANCE = 2267f;//15.5f;
	GameObject audioSource;
	float audioX;
	float audioY;
	float micX;
	float micY;

	// Use this for initialization
	void Start () {
		GameObject.Find("Camera POV").GetComponent<AudioListener>().enabled = false;
		audioSource = GameObject.Find("Audio Source");
		audioSource.GetComponent<AudioSource>().enabled = true;
		audioX = audioSource.transform.position.x;
		audioY = audioSource.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 heading = transform.position - audioSource.transform.position;
		float distance;
		float direction;
		distance = heading.magnitude;
		direction = heading.x;
		//Debug.Log("heading: " + heading.ToString() + " distance " + distance.ToString() + " direction " + direction.ToString());
		audioSource.transform.GetComponent<AudioSource>().volume = 1f-(distance/MAX_DISTANCE);
		audioSource.transform.GetComponent<AudioSource>().panStereo = -Mathf.Clamp(direction, -1, 1);
	}
}
