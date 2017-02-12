using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// An object in the game scene that can be selected.
	public class SelectableObject : MonoBehaviour {

		// Called when the specified player interacts with this object.
		public virtual SelectableObject Interact(PlayerController player) {
			return this;
		}
	}
}
