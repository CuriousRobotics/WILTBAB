using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
 
public class RobotController : MonoBehaviour {
	//public static bool initialized = false;

    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;

	GameObject wheelColliders;
    List<WheelCollider> wheels = new List<WheelCollider>();
    public float timeNext;
    public float timeCurrent;
    public int steerMode;
    public Light morningstar;

    //public List<GameObject> ir_sensors_go;
    //private IR[] ir_sensors;
    //private LDR[] ldr_sensors;

    /***********/
    float linearVel;
    float steeringVel;
	Vector3 translation;
	Vector3 rotation;
	float rotationSpeed;
	/*
	Vector3 translation;
	Vector3 rotation;
	float rotationSpeed;
	GameObject environment;
	// Use this for initialization
	void Start () {
		translation   = Vector3.zero;
		rotation      = Vector3.zero;
		rotationSpeed = 10f;
		environment = GameObject.Find("Environment");
	}
	
	// Update is called once per frame
	void Update () {
		//Change to Dynamic, but keep centered?  Then it can hit walls instead of passing through them.
		//Alternatively, the walls need to be dynamic, but not Move when hitting the robot?
		rotation    = Vector3.zero;
		if(Input.GetKey(KeyCode.UpArrow)){
			translation = environment.transform.position + (-environment.transform.up * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			translation = environment.transform.position + (environment.transform.up * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			rotation.z = -rotationSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			rotation.z = rotationSpeed * Time.deltaTime;
		}
		environment.transform.position = translation;
		environment.transform.Rotate(rotation);
	}*/

    /**********/
    public void Start(){
		//initialized = false;
		//Rigidbody ir_sensor	   = new Rigidbody();
    	int i = 0;
    	Quaternion ir_rotation = new Quaternion();
    	Vector3 ir_position    = new Vector3();
		Quaternion ldr_rotation = new Quaternion();
    	Vector3 ldr_position    = new Vector3();
//    	wheelColliders = GameObject.Find("WheelColliders");
//    	//morningstar = GameObject.Find("Directional Light").GetComponent<Light>();
//    	wheels.Add(wheelColliders.transform.Find("frontRight").GetComponent<WheelCollider>());
//		wheels.Add(wheelColliders.transform.Find("frontLeft").GetComponent<WheelCollider>());
//		//wheels.Add(wheelColliders.transform.Find("backRight").GetComponent<WheelCollider>());
//		//wheels.Add(wheelColliders.transform.Find("backLeft").GetComponent<WheelCollider>());
//		//Wheels = wheelColliders.transform.Find("frontRight").GetComponent<WheelCollider>();
//		foreach(WheelCollider wheel in wheels){
//			
//			if(wheel.name.Contains("front")){
//				wheel.motorTorque = 50f;
//				//wheel.steerAngle = 45f;
//			}
//		}
		timeNext  = 0;
		timeCurrent = 0;
		steerMode   = 0;

		translation   = Vector3.zero;
		rotation      = Vector3.zero;
		rotationSpeed = 20f;
		Debug.Log("RobotController.Start()");
		Debug.Log("position: " + transform.position.ToString() + " rotation: " + transform.rotation.ToString());

    }
    /*
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
 
        Transform visualWheel = collider.transform.GetChild(0);
 
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
 
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
        print(visualWheel.name + ": position " + visualWheel.transform.position.ToString() + " : rotation " + visualWheel.transform.rotation.ToString());
    }
 	*/
    public void FixedUpdate()
    {
		//initialized = true;
    	//Debug.Log("reset? " + reset.ToString());
    	//if(reset){Reset();}
		//Debug.Log("SimpleCarController.FixedUpdate()");
		//Debug.Log("position: " + transform.position.ToString() + " rotation: " + transform.rotation.ToString());
    	//print("Bringer of light: " + morningstar.intensity.ToString());
    	timeCurrent = Time.timeSinceLevelLoad;
		//Relative angle of sensor on the sensor ring
		//Not handling this correctly still...
		//linearVel   = 0.0f;
		//steeringVel = 0.0f;
		rotation = Vector3.zero;
		//translation = Vector3.zero;
		if(Input.GetKey(KeyCode.UpArrow)){
			//linearVel = 500.0f;// * Time.deltaTime;
			translation = transform.position + (transform.forward * Time.deltaTime * 100.0f);
			Move();
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			//linearVel = -500.0f;// * Time.deltaTime;
			translation = transform.position + (-transform.forward * Time.deltaTime * 100.0f);
			Move();
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			//steeringVel = 500.0f;// * Time.deltaTime;
			rotation.y = -rotationSpeed * Time.deltaTime;
			Move();
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			//steeringVel = -500.0f;// * Time.deltaTime;
			rotation.y = rotationSpeed * Time.deltaTime;
			Move();
		}

	
    }

    void Move(){
    	transform.position = translation;
    	transform.Rotate(rotation);
    	/*Debug.Log("linearVel: " + linearVel.ToString() + " steeringVel: " + steeringVel.ToString());
		foreach(WheelCollider wheel in wheels){
			if(wheel.name.Contains("front")){
				wheel.motorTorque = linearVel;
				wheel.steerAngle = steeringVel;
			}
		}*/
    }
    /*
    void Reset(){
    	transform.position = Vector3.zero;
    	transform.rotation = Quaternion.identity;
    	reset = false;
    }*/
}