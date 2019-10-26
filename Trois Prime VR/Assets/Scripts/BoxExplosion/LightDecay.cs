using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                               LightDecay.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - Reduce the intensity of a light over time
//		- Check to see how bright the light is.  If it's still visible keep reducing the intensity, otherwise remove it from the scene

public class LightDecay : MonoBehaviour {

	public float lightTime;					//How long the light will be visible for
	float lightDecay;						//The amount to reduce the intensity of the light


	void Update () {

		if (GetComponent<Light>().intensity > 0.05) {
			lightDecay = (GetComponent<Light>().intensity * (Time.deltaTime/lightTime));
			GetComponent<Light>().intensity -= lightDecay;
		}
		
		if (GetComponent<Light>().intensity < 0.05) {
			Destroy (gameObject);
		}
	}
}