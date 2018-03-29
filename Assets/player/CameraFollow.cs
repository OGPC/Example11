using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform follow;
	public Vector3 positionOffset;
	public float lerpRate = 5f;

	void Start () {
		if (follow == null) {
			Debug.LogWarning("CameraFollow on " + gameObject.name + " has no follow assigned.");
			return;
		}
		transform.position = follow.position + positionOffset;
	}
	
	void Update () {
		if (follow == null)
			return;
		transform.position = Vector3.Lerp(
			transform.position,
			follow.position + positionOffset,
			lerpRate * Time.deltaTime
		);
	}
}
