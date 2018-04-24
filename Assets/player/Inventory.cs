using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	
	public string heldItem;


	public bool Empty () {
		return heldItem == "";
	}

	public bool HasItem (string item) {
		return heldItem == item;
	}

	public void Give (string item) {
		heldItem = item;
		
	}

	public void Take (string item) {
		heldItem = "";
	}
}
