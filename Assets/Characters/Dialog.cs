using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

	public bool isComplete = false;
	public int targetMessage = 0;
	public Transform messageParent;
	public Transform[] messages;
	public AnimationCurve zoomAnim;
	public float animSpeed = 0.5f;

	[HideInInspector]
	public bool isVisible = false;

	private int currentMessage = 0;
	private int lastTarget = 0;
	private bool moving = false;
	private bool lastComplete;
	private Vector3 startScale;
	private float time;

	private void Start () {
		lastTarget = targetMessage = currentMessage;
		lastComplete = isComplete;
		time = 0f;

		startScale = messageParent.localScale;
		messageParent.localScale = Vector3.zero;
		for (int i = 0; i < messages.Length; i++)
			messages[i].gameObject.SetActive(i == currentMessage);
	}

	private void Update () {
		if (lastTarget != targetMessage)
			ChangeMessage(targetMessage);

		if (isComplete && !lastComplete)
			Complete();

		if (targetMessage != messages.Length - 1)
			isComplete = false;

		if (!moving) {
			if ((time == 1f) && (currentMessage == messages.Length - 1))
				Complete(true);
			return;
		}

		if (time == 0)
			currentMessage = targetMessage;

		if (isVisible && (currentMessage == targetMessage))
			time += 1 / animSpeed * Time.deltaTime;
		else
			time -= 1 / animSpeed * Time.deltaTime;

		time = Mathf.Clamp01(time);

		messageParent.localScale = startScale * zoomAnim.Evaluate(time);
		for (int i = 0; i < messages.Length; i++)
			messages[i].gameObject.SetActive(i == currentMessage);

		if ((time == 0f || time == 1f) && (currentMessage == targetMessage))
			moving = false;

		lastTarget = targetMessage;
		lastComplete = isComplete;
	}

	private void OnTriggerEnter (Collider other) {
		isVisible = true;
		moving = true;
	}

	private void OnTriggerExit (Collider other) {
		isVisible = false;
		moving = true;
	}

	public void ChangeMessage (int to) {
		lastTarget = targetMessage = to;
		moving = true;
	}

	// force means don't wait for player to arrive
	public void Complete (bool force = false) {
		ChangeMessage(messages.Length - 1);
		if (force)
			lastComplete = isComplete = true;
	}

	private void OnGUI () {
		if (GUI.Button(new Rect(10, 10, 150, 20), "Set to first message"))
			ChangeMessage(0);
		if (GUI.Button(new Rect(10, 40, 150, 20), "Set to second message"))
			ChangeMessage(1);
		if (GUI.Button(new Rect(10, 70, 150, 20), "Set to final message"))
			ChangeMessage(2);

		if (GUI.Button(new Rect(10, 110, 150, 20), "Mark complete (soft)"))
			Complete();
		if (GUI.Button(new Rect(10, 140, 150, 20), "Mark complete (hard)"))
			Complete(true);
	}
}
