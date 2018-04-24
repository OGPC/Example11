using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dialog))]
public class NPCController : MonoBehaviour {

	public string hasItem;
	public string wantsItem;
	public Collider door;

	public bool tradeCompleted = false;

	[HideInInspector]
	public Dialog dialog;

	private void Start () {
		dialog = GetComponentInParent<Dialog>();
	}

	private void Update () {
		if (door != null)
			door.enabled = !tradeCompleted;
	}

	public bool Talk () {
		return dialog.Talk();
	}

	public bool Trade (string givenItem) {
		if (tradeCompleted)
			return false;

		if (givenItem != wantsItem)
			return false;

		tradeCompleted = true;
		dialog.Complete();
		return true;
	}
}
