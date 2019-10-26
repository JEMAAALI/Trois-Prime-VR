using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                           GenerateParticles.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - This script generates a particle system on start.  
//		- There is an option to have the particle system spawn only some of the time, rather than every time

public class GenerateParticles : MonoBehaviour {

	public GameObject particles;			//The particle system that will be generated
	public int particleChance;				//The chance a particle system will be generated (Between 1 and 100)

	void Start () {

		//Check to make sure the particleChance is valid
		if (particleChance < 0) {
			Debug.LogWarning("ParticleChance is an invalid number. Clamping the value to make it valid");
			particleChance = Mathf.Clamp(particleChance, 1, 100);
		}
		else if (particleChance == 0) {
			Debug.LogWarning("ParticleChance is zero. Consider disabling the script since no particles will be generated");
			return;
		}

		int randomNumber = Random.Range (1, 100);

		if (randomNumber < particleChance) {
			GameObject newParticles = (GameObject)Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
			newParticles.transform.parent = gameObject.transform;
		}
	}
}
