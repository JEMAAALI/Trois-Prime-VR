using UnityEngine;
using System.Collections;

// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
//                            BoomBox 1.5, Copyright © 2014, RipCord Development
//                                              ClearScene.cs
//                                           info@ripcorddev.com
//
// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\

//ABOUT - When triggered, removes all objects with a certain tag from the scene

public class ClearScene : MonoBehaviour {

	void OnMouseUp () {

		RemoveOldObjects();
	}
	

	void RemoveOldObjects () {

		//Find all existing boomBox objects in the scene then delete them.
		GameObject[] oldBoxes = GameObject.FindGameObjectsWithTag("box");
		
		foreach (Object oldBox in oldBoxes) {
			Destroy(oldBox);
		}
		
		GameObject[] oldShrapnel = GameObject.FindGameObjectsWithTag("shrapnel");
		
		foreach (Object piece in oldShrapnel) {
			Destroy(piece);
		}
	}
}