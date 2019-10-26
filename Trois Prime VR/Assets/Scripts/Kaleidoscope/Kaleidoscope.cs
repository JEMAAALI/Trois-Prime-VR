using UnityEngine;
using System.Collections;

public class Kaleidoscope : MonoBehaviour {
	public Transform baseObject;
	public int circleValue = 16;
	public bool xMirror = true;
	public Gradient color;

	ParticleSystem pe;
	ParticleSystem.Particle[] particles;
	int mirror_mult = 1;
	// Use this for initialization
	void Start () {
		if (baseObject == null) {
			Debug.LogError ("No baseObject.Please assign GameObject to baseObject!");
		}
		if (xMirror) {
			mirror_mult = 2;
		}
		pe = GetComponent<ParticleSystem> ();
		// create
		pe.Emit (circleValue * mirror_mult);
		particles = new ParticleSystem.Particle[circleValue * mirror_mult];
		pe.GetParticles(particles);

		if (baseObject) {
			ParticleUpdate ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (baseObject) {
			ParticleUpdate ();
		}
	}

	void ParticleUpdate(){
		// circle
		float angle_dist = 360f / (float)circleValue;
		GameObject sub = new GameObject ();
		sub.transform.parent = transform;
		for (int i = 0; i < circleValue; i++) {
			float myangle = angle_dist * (float)i;
			particles [i].position = MoveCircle (baseObject.localPosition, myangle);
			particles [i].size = baseObject.localScale.magnitude;
			sub.transform.localPosition = baseObject.localPosition;
			sub.transform.localRotation = baseObject.localRotation;
			sub.transform.RotateAround (transform.position, transform.forward, myangle);
			float angle;
			Vector3 axis;
			sub.transform.localRotation.ToAngleAxis (out angle, out axis);
			axis = axis.normalized;
			particles [i].rotation = angle;
			particles [i].axisOfRotation = axis;
			// color
			particles [i].color = color.Evaluate (pe.time / pe.duration);
		}

		// mirror
		if (xMirror) {
			Vector3 pos = new Vector3 (-particles[0].position.x, particles[0].position.y, particles[0].position.z);
			for (int i = 0; i < circleValue; i++) {
				int n = circleValue + i;
				float myangle = angle_dist * (float)i;
				particles [n].size = particles [0].size;
				sub.transform.localPosition = pos;
				sub.transform.localRotation = baseObject.localRotation;
				sub.transform.localEulerAngles = new Vector3 (-sub.transform.localEulerAngles.x,-sub.transform.localEulerAngles.y+180,sub.transform.localEulerAngles.z);
				sub.transform.RotateAround (transform.position, transform.forward, myangle);
				float angle;
				Vector3 axis;
				sub.transform.localRotation.ToAngleAxis (out angle, out axis);
				particles [n].position = sub.transform.localPosition;
				particles [n].rotation = angle;
				particles [n].axisOfRotation = axis;
				particles [n].color = particles [0].color;
			}
		}
		Destroy (sub);

		pe.SetParticles(particles,particles.Length);
	}

	Vector3 MoveCircle(Vector3 before, float angle){
		Vector3 after = before;
		after.x = before.x * Mathf.Cos (Mathf.PI / 180f * angle) - before.y * Mathf.Sin (Mathf.PI / 180f * angle);
		after.y = before.x * Mathf.Sin (Mathf.PI / 180f * angle) + before.y * Mathf.Cos (Mathf.PI / 180f * angle);
		return after;
	}

}
