using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                            	  BoomBox.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - Add this script to any object to turn it into a weapon of mass destruction.  
//		- When triggered the object will explode, generating particle effects, lights, sounds and whatever else is needed to make the best looking object destruction effects.

public class BoomBox : MonoBehaviour {

	public bool destroyObject;								//If true, the object will be destroyed when the explosion is triggered
	
	// -------------------------------------

	public ExplosiveForce explosiveForce;

	[System.Serializable]
	public class ExplosiveForce {

		public bool addExplosiveForce;						//If true, an explosive force will be added when the explosion is triggered
		public float explosionRadius = 5.0f;
		public float explosionPower = 10.0f;
	
		public bool shakeTheCamera;
		public float shakePower;							//Determines how much the camera will shake when the explosion is triggered
	}

	// -------------------------------------
	
	public AudioComponents explosionAudio;

	[System.Serializable]
	public class AudioComponents {
		
		public bool playExplosionAudio;						//If true, the explosion will play an audio clip
		public AudioClip[] explosionSound;					//The audio clip that will play when the object explodes.  One is randomly selected from the array.
	}
	
	// -------------------------------------
	
	public ExplosionParticles explosionParticles;

	[System.Serializable]
	public class ExplosionParticles {
		
		public bool generateParticles;						//If true, an effect for the actual explosion will be generated
		public GameObject explosionEffect;					//The particle system that will emit when the object explodes
	}

	GameObject effectsContainer;							//An empty object used to store all the explosion effects.  This keeps the project hierarchy clean

	// -------------------------------------
	
	public EmitLight lights;

	[System.Serializable]
	public class EmitLight {
		
		public bool emitLight;								//If true, the explosion will generate a light that will quickly fade out over time
		public GameObject lightPrefab;						//The prefab for the light that will be generated whe the object explodes
	}

	// -------------------------------------

	public Shrapnel shrapnel;

	[System.Serializable]
	public class Shrapnel {

		public bool generateShrapnel;						//If true, the explosion will generate shrapnel fragments
		
		public int shrapnelMin;		  						//Minimum number of shrapnel to be emitted
		public int shrapnelMax;  							//Maximum number of shrapnel to be emitted
		
		public bool randomizeScale;							//Randomize the scale of the generated shrapnel
		public float scaleVariation = 0.25f;				//The scaleVariation will be added and subtracted from the original scale which will give the MAX and MIN values.  The scale of each generated shrapnel object will be a random number between those two values.

		public float lifeSpan;								//The amount of time (in seconds) the shrapnel will be visible for.  If 0 or less, the shrapnel will not disappear
		public GameObject[] shrapnelFragments;				//A list of the shrapnel fragment meshes that can potentially be generated
		
		public Transform shrapnelOrigin;					//The point where the shrapnel and particles will be emitted from
	}

	GameObject shrapnelContainer;

	// -------------------------------------

	public ForceDirection forceDirection;

	[System.Serializable]
	public class ForceDirection {
		
		public bool useForceDirection;
		public Transform directionTransform;
		public float directionStrength;
	}

	// -------------------------------------
	
	public Force shrapnelForce;

	[System.Serializable]
	public class Force {

		public Vector3 minForce = new Vector3(-200.0f, 200.0f, -200.0f);
		public Vector3 maxForce = new Vector3(200.0f, 400.0f, 200.0f);
	}
	
	float randomForceX;
	float randomForceY;
	float randomForceZ;
	
	// -------------------------------------

	public Wreckage wreckage;

	[System.Serializable]
	public class Wreckage {

		public bool generateWreckage;
		public GameObject wreckedObject;
	}

	// -------------------------------------
	
	//Generate smoke particles that emit from the object that exploded.
	public SmokeParticles smokeParticles;

	[System.Serializable]
	public class SmokeParticles {
		
		public bool generateSmoke;
		public GameObject smokeSystem;						//The particle system that will emit when the object explodes.  Smoke is generated at the pivot point of the exploding object.
	}


	void Start () {

		//If there is no effectsContainer in the scene, create one
		if (!GameObject.Find("_EffectsContainer")) {
			effectsContainer = new GameObject("_EffectsContainer");
		}
		
		effectsContainer = GameObject.Find("_EffectsContainer");				//Find the effectsContainer object

		//If there is no shrapnel container in the scene, create one
		if (!GameObject.Find("_ShrapnelContainer")) {
			shrapnelContainer = new GameObject("_ShrapnelContainer");
		}

		shrapnelContainer = GameObject.Find("_ShrapnelContainer");				//Find the shrapnelContainer object

		//If an origin point for the shrapnel wasn't specified, use the pivot point of the gameObject instead
		if (!shrapnel.shrapnelOrigin) {
			shrapnel.shrapnelOrigin = gameObject.transform;
		}
	}


	void OnMouseDown () {

		Explode();
	}
	

	void Explode () {
		
		// DESTROY THE ORIGINAL OBJECT - - - - - - - - - - - - - - - - - - - -
		if (destroyObject) {
			GameObject.Find("Logo_Plane_H1").GetComponent<Scene_Manager>().CameraBlood();
			Destroy(gameObject);
		}
		
		//EXPLOSIVE FORCE AFFECTS OTHER OBJECTS - - - - - - - - - - - - - - - - - - - -
		if (explosiveForce.addExplosiveForce) {
			
			//Applies an explosive force to all nearby rigidbodies
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, explosiveForce.explosionRadius);
			
			foreach (Collider hit in colliders) {
				
				if (!hit) {
					continue;
				}
				if (hit.GetComponent<Rigidbody>()) {
					hit.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce.explosionPower, explosionPos, explosiveForce.explosionRadius, 3.0f);
				}
			}
		}
		
		// CAMERA SHAKE - - - - - - - - - - - - - - - - - - - -
		if (explosiveForce.shakeTheCamera) {
			CameraShaker.shake = explosiveForce.shakePower;
		}
		
		// GENERATE EXPLOSIVE EFFECT PARTICLES - - - - - - - - - - - - - - - - - - - -
		if (explosionParticles.generateParticles) {
			GameObject newExplosionEffect = (GameObject)Instantiate (explosionParticles.explosionEffect, shrapnel.shrapnelOrigin.position, gameObject.transform.rotation);
			newExplosionEffect.transform.parent = effectsContainer.transform;
		}
		
		// CREATE A LIGHT AT THE SOURCE OF THE EXPLOSION - - - - - - - - - - - - - - - - - - - -
		if (lights.emitLight) {
			GameObject newExplosionLight = (GameObject)Instantiate (lights.lightPrefab, shrapnel.shrapnelOrigin.position, gameObject.transform.rotation);
			newExplosionLight.transform.parent = effectsContainer.transform;
		}
		
		// EXPLOSION AUDIO - - - - - - - - - - - - - - - - - - - -
		if (explosionAudio.playExplosionAudio) {
			AudioClip selectedAudio = explosionAudio.explosionSound[Random.Range(0, explosionAudio.explosionSound.Length)];
			AudioSource.PlayClipAtPoint(selectedAudio, shrapnel.shrapnelOrigin.position);
		}
		
		// GENERATE A WRCKED VERSION OF THE OBJECT THAT JUST EXPLODED - - - - - - - - - - - - - - - - - - - -	
		if (wreckage.generateWreckage) {

			GameObject newWreckage = (GameObject)Instantiate (wreckage.wreckedObject, gameObject.transform.position, gameObject.transform.rotation);
			
			//Checks to see if there is a rigidbody on the generated wreckage.  If there is, it will apply a random force causing it to spin				
			if(newWreckage.GetComponent<Rigidbody>()){
				newWreckage.GetComponent<Rigidbody>().AddForce(new Vector3(randomForceX, randomForceY, randomForceZ));

				if(forceDirection.useForceDirection) {
					newWreckage.GetComponent<Rigidbody>().AddForce(forceDirection.directionTransform.up * forceDirection.directionStrength);
				}
			}
		}
		
		// GENERATE SHRAPNEL FRAGMENTS - - - - - - - - - - - - - - - - - - - -
		if (shrapnel.generateShrapnel) {

			int numberOfFragments = Random.Range(shrapnel.shrapnelMin, shrapnel.shrapnelMax);
			
			for (int x = 0; x < numberOfFragments; x++){
				GameObject selectedShrapnel = shrapnel.shrapnelFragments[Random.Range(0, shrapnel.shrapnelFragments.Length)];

				GameObject newShrapnelObject = (GameObject)Instantiate (selectedShrapnel, shrapnel.shrapnelOrigin.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
				newShrapnelObject.transform.parent = shrapnelContainer.transform;

				randomForceX = Random.Range(shrapnelForce.minForce.x,shrapnelForce.maxForce.x);
				randomForceY = Random.Range(shrapnelForce.minForce.y,shrapnelForce.maxForce.y);
				randomForceZ = Random.Range(shrapnelForce.minForce.z,shrapnelForce.maxForce.z);
				
				//Randomize the scale of each shrapnel object that is generated
				if (shrapnel.randomizeScale) {
					var objectScale = Random.Range( (1 - shrapnel.scaleVariation), (1 + shrapnel.scaleVariation) );

					if (objectScale > 0) {
						newShrapnelObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
						newShrapnelObject.GetComponent<Rigidbody>().mass = objectScale;
					}
				}
				
				//Destroy shrapnel object after the specified life span.  Setting the lifespan to zero will leave the shrapnel on screen indefinitely.
				if (shrapnel.lifeSpan > 0) {
					Destroy(newShrapnelObject, shrapnel.lifeSpan);
				}
				
				//Checks to see if there is a rigidbody on the generated shrapnel.  If there is, it will apply a random force causing it to spin	
				if(newShrapnelObject.GetComponent<Rigidbody>()){
					newShrapnelObject.GetComponent<Rigidbody>().AddForce(new Vector3(randomForceX, randomForceY, randomForceZ));
					if(forceDirection.useForceDirection) {
						newShrapnelObject.GetComponent<Rigidbody>().AddForce(forceDirection.directionTransform.up * forceDirection.directionStrength);
					}
				}
			}
		}
		
		// GENERATE SMOKE PARTICLES - - - - - - - - - - - - - - - - - - - -	
		if (smokeParticles.generateSmoke) {

			Instantiate (smokeParticles.smokeSystem, gameObject.transform.position, gameObject.transform.rotation);
		}
	}
}