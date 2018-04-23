using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public string item;
	public bool pickedUp = false;
	public float shrinkTime = 0.5f;

	private float time;
	private Vector3 startScale;

	private void Start () {
		startScale = transform.localScale;
	}

	private void Update () {
		if (!pickedUp)
			return;

		transform.localScale = Vector3.Lerp(startScale, Vector3.zero, time/shrinkTime);
		time += Time.deltaTime;
		if (time >= shrinkTime)
			gameObject.SetActive(false);
	}

	public bool Take () {
		if (pickedUp)
			return false;

		pickedUp = true;
		time = 0f;
		return true;
	}
}
