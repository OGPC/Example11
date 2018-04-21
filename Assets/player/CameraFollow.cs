using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform follow;
	public Vector3 positionOffset;
	public float lerpRate = 5f;

	private void Start () {
		if (follow == null)
			Debug.LogWarning("CameraFollow on " + gameObject.name + " has no follow assigned.");
		else
			transform.position = follow.position + positionOffset;
	}
	
	private void Update () {
		if (follow == null)
			return;
		transform.position = Vector3.Lerp(
			transform.position,
			follow.position + positionOffset,
			lerpRate * Time.deltaTime
		);
	}
}
