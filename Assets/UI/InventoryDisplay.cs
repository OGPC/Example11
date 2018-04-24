using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public Text itemName;
	public Transform displayParent;

	public GameObject [] displayItemPrefabs;

	public float spinSpeed = 90f;

	public Inventory inventory;

	private string lastItem = "";



	private void Start () {
		lastItem = "";
		UpdateDisplay();
	}

	private void LateUpdate () {
		if (lastItem != inventory.heldItem)
			UpdateDisplay();

		// spin item
		displayParent.Rotate(0, spinSpeed * Time.deltaTime, 0, Space.Self);

		lastItem = inventory.heldItem;
	}

	private void UpdateDisplay () {
		itemName.text = inventory.heldItem;
		displayParent.transform.rotation = Quaternion.identity;
		if (displayParent.childCount != 0)
			Destroy(displayParent.GetChild(0).gameObject);
		for (int i = 0; i < displayItemPrefabs.Length; i++) {
			if (displayItemPrefabs[i].name == inventory.heldItem) {
				Instantiate(displayItemPrefabs[i], displayParent);
				break;
			}
		}
	}
}
