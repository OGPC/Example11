using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	[SerializeField]
	private List<string> items = new List<string>();
	
	public void AddItem (string item) {
		items.Add(item);
	}

	public bool HasItem (string item) {
		return items.Contains(item);
	}

	public string RemoveItem (string item) {
		if (items.Contains(item)) {
			items.Remove(item);
			return item;
		}
		return "";
	}
}
