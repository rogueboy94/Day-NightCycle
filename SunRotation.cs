using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {

	public float speed = 1f;

	// Use this for initialization
	void Update () {
		transform.Rotate (Time.deltaTime * speed, 0, 0);
	}
}
