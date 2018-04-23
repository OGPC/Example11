using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dialog))]
public class NPCController : MonoBehaviour {

	public string hasItem;
	public string wantsItem;

	public bool tradeCompleted = false;

	[HideInInspector]
	public Dialog dialog;

	private void Start () {
		dialog = GetComponentInParent<Dialog>();
	}

	public void Talk () {
		dialog.Talk();
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
