using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

	public float initialVelocity;
	public float acceleration;
	float currentVelocity;

	// Use this for initialization
	void Start () {
		currentVelocity = initialVelocity;
	}
	
	void FixedUpdate() {
		if (Time.fixedTime < Timer.predictedTime) {
			currentVelocity += acceleration * Time.fixedDeltaTime;
			transform.Translate (Vector3.right * currentVelocity * Time.fixedDeltaTime);
		}
	}
}
