using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour {

	public NPCController interactingNPC;
	public Pickup foundItem;

	[HideInInspector]
	public Inventory inventory;

	private void Start () {
		inventory = GetComponent<Inventory>();
	}
	
	private void Update () {
		if (Input.GetButtonDown("Use")) {

			// is there an item to pick up?
			if (foundItem != null) {
				if (foundItem.Take())
					inventory.AddItem(foundItem.item);
			}

			// is there an NPC to interact with?
			if (interactingNPC != null) {
				// do we have what they want?
				string toGive = interactingNPC.wantsItem;
				if (inventory.HasItem(toGive) && interactingNPC.Trade(toGive)) {
					inventory.RemoveItem(toGive);
					inventory.AddItem(interactingNPC.hasItem);
					// successfully traded
				}
				// tried and failed to trade
				interactingNPC.Talk();
			}
		}
		// didn't try to trade
	}

	private void OnTriggerEnter (Collider other) {
		if (other.tag == "NPC") {
			interactingNPC = other.GetComponent<NPCController>();
			if (interactingNPC == null) {
				Debug.LogWarning("No NPCController found on " + other.name + " (despite \"NPC\" tag)");
				return;
			}
		} else if (other.tag == "Item") {
			foundItem = other.GetComponent<Pickup>();
			if (foundItem == null) {
				Debug.LogWarning("No Pickup found on " + other.name + " (despite \"Item\" tag)");
				return;
			}
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.tag == "NPC") {
			interactingNPC = null;
		} else if (other.tag == "Item") {
			foundItem = null;
		}
	}
}
