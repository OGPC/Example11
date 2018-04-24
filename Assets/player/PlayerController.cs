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
				if (inventory.Empty() && foundItem.Take())
					inventory.Give(foundItem.item);
			}

			// is there an NPC to interact with?
			if (interactingNPC != null) {
				// first, talk until out of things to say.
				if (!interactingNPC.Talk()) {
					// do we have what they want?
					string toGive = interactingNPC.wantsItem;
					if (inventory.HasItem(toGive) && interactingNPC.Trade(toGive)) {
						inventory.Take(toGive);
						inventory.Give(interactingNPC.hasItem);
						// successfully traded
					}
					// tried and failed to trade
				}
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
