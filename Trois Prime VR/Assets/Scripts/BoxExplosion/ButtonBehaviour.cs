using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                            buttonBehaviour.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - This script controls the colour for different states of a button. The object must havea collider on it for the mouse events to register.

public class ButtonBehaviour : MonoBehaviour {

	Color offState;								//Used to store the initial material colour
	public Color overState;  					//Defines the colour the button changes to when the cursor hovers over it
	public Color clickState;					//Defines the colour the button changes to when it is clicked on
	
	public AudioClip overAudio;					//If used, this sound clip will play when the cursor hovers over the object
	public AudioClip clickAudio;				//If used, this sound clip will play when the cursor clicks on the object
	
	string buttonState;  						//Stores the current state of the button
	public float fadeTime = 0.25f;				//The amount of time it takes for the button colour to fade from one state to the next
	

	void Start () {
		offState = GetComponent<Renderer>().material.color;		//Defines the offState colour of the button as the colour it is when the scene starts
	}
	
	//Highlight button
	void OnMouseEnter () {
		ButtonState("Over");
		if (overAudio) {
			GetComponent<AudioSource>().clip = overAudio;
			GetComponent<AudioSource>().Play();
		}
	}
	
	//Remove highlight from button
	void OnMouseExit () {
		ButtonState("Off");
	}

	//Change button to click colour
	void OnMouseDown () {
		GetComponent<Renderer>().material.color = clickState;
		if (clickAudio) {
			GetComponent<AudioSource>().clip = clickAudio;
			GetComponent<AudioSource>().Play();
		}
	}

	//This function controls the colour change for the various button states
	void ButtonState(string buttonState)	{

		StartCoroutine( ColourFade(buttonState) );
	}


	IEnumerator ColourFade (string buttonState) {

		Color currentColor = GetComponent<Renderer>().material.color;
		float timeLeft = fadeTime;

		while (timeLeft > 0) {
			
			if (buttonState == "Over") {
				GetComponent<Renderer>().material.color = Color.Lerp(currentColor, overState, (fadeTime - timeLeft) / fadeTime);
			}
			
			if (buttonState == "Off") {
				GetComponent<Renderer>().material.color = Color.Lerp(currentColor, offState, (fadeTime - timeLeft) / fadeTime);
			}
			
			yield return null;
			timeLeft -= Time.deltaTime;
		}
	}

	// BUTTON ACTION --------------------
	
	void OnMouseUp() {
		GetComponent<Renderer>().material.color = overState;
	}
}







