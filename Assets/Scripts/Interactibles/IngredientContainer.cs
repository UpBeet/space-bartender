using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender.Interactibles {

	// A SelectableObject that contains an Ingredient.
	public class IngredientContainer : SelectableObject {

		// The type of ingredient this IngredientContainer contains.
		[SerializeField]
		private IngredientType ingredient;
		public IngredientType Ingredient {
			get { return ingredient; }
		}

		// Interact with this object.
		public override SelectableObject Interact (PlayerController player) {
			player.AddIngredient (ingredient);
			return base.Interact (player);
		}
	}
}