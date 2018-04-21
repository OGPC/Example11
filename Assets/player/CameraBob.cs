using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour {

	public Vector3 amplitude = Vector3.one * 0.1f;
	public Vector3 offset = new Vector3(0f, 2f*Mathf.PI/3f, 4f*Mathf.PI/3f);
	public Vector3 speed = new Vector3(0.9f, 0.8f, 1f);

	private Quaternion startRot;
	private Vector3 curr;
	private float x, y, z;

	private void Start () {
		startRot = transform.rotation;
		curr = Vector3.zero;
	}

	private void LateUpdate () {
		x = Mathf.Sin(offset.x + (Time.timeSinceLevelLoad * speed.x));
		y = Mathf.Sin(offset.y + (Time.timeSinceLevelLoad * speed.y));
		z = Mathf.Sin(offset.z + (Time.timeSinceLevelLoad * speed.z));
		
		curr = Vector3.Scale(new Vector3(x, y, z), amplitude);

		transform.rotation = startRot * Quaternion.Euler(curr);
	}
}
