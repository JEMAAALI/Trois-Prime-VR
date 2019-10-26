using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                             CameraShaker.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - This script handles the shaking effect of the camera. It could however, be used to shake any object.

public class CameraShaker : MonoBehaviour {

	Vector3 initialPosition; 						//Original position of the camera
	public static float shake = 0f;					//How long and how shaky the camera will get
	
	public bool keepInitialPosition = false;		//If true, the object will return to its original position once it stops shaking
	public int shakeDecay = 2;						//How quickly the shaking stops
	
	bool isShaking = false;  						//Is the camera currently shaking?

	
	void Start () {

		initialPosition = transform.position;		//Sets the original position of the object so it can return to it once the shaking stops
	}
	

	void Update () {

		//If the shake value is greater than 0, shake the camera!
		if (shake > 0) {
			
			shake -= shakeDecay;  					//Decrease the shake value based on shakeDecay
			
			//If the initial position of the camera is not needed, update the value of initialPosition based on the current position of the object
			if (keepInitialPosition == false) {
				initialPosition = transform.position; 
			}
			
			//Move the object in a random direction from the initialPosition...
			//...multiplying it by the value of Shake so that at the start the shake is stronger and it gets weaker towards the end, until it stops
			Vector3 randomPosition = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
			transform.position = initialPosition + randomPosition * shake * 0.002f;
			
			if (isShaking == false) {  				//If the camera is not shaking, start shaking it
				isShaking = true;  					//Used to make this shake check happen just once
			}
		}

		//If the shake value reaches 0, stop shaking
		else  {
			
			if (isShaking == true) {  				//If the camera is still shaking, stop shaking it
				isShaking = false;  				//Used to make this shake check happen just once
			}
		}
	}
}