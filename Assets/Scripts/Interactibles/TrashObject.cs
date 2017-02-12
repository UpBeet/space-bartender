using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender.Interactibles {

	// A SelectableObject that mixes your drinks when you select it.
	public class TrashObject : SelectableObject {

		// Interact with this object.
		public override SelectableObject Interact (PlayerController player) {
			player.DumpIngredients ();
			return base.Interact (player);
		}
	}
}
