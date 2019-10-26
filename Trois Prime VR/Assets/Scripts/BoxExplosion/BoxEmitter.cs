using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                               BoxEmitter.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - When triggered, generates a random object from the boxes list

public class BoxEmitter : MonoBehaviour {

	public GameObject[] boxes;
	public Transform emitter;


	void OnMouseDown () {

		GameObject selectedBox = boxes[Random.Range(0, boxes.Length)];
		Vector3 randomPosition = emitter.transform.position + new Vector3(Random.Range (-4, 4), 0, Random.Range (-4, 4));
		Vector3 randomRotation = new Vector3(Random.Range (0, 360), Random.Range(0, 360), Random.Range(0, 360));

		Instantiate (selectedBox, randomPosition, Quaternion.Euler(randomRotation) );
	}
}