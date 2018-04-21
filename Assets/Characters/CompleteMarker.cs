using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMarker : MonoBehaviour {

	private Dialog dialog;

	private float time;
	private Vector3 startPos;
	private Vector3 startScale;

	private void Start () {
		dialog = GetComponentInParent<Dialog>();
		if (dialog == null) {
			Debug.Log("Dialog missing in parent of " + gameObject.name);
			gameObject.SetActive(false);
			return;
		}
		startPos = transform.localPosition;
		startScale = transform.localScale;
	}
	
	private void Update () {
		if (dialog.isComplete)
			time += 1 / dialog.animSpeed * Time.deltaTime;
		else
			time -= 1 / dialog.animSpeed * Time.deltaTime;

		time = Mathf.Clamp01(time);

		transform.localPosition = Vector3.LerpUnclamped(
			Vector3.zero,
			startPos,
			dialog.zoomAnim.Evaluate(time)
		);

		transform.localScale = Vector3.LerpUnclamped(
			Vector3.zero,
			startScale,
			dialog.zoomAnim.Evaluate(time)
		);
	}
}
