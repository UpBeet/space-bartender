﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Enumerator for ingredient types in the game.
	public enum IngredientType {
		RedDrank,
		BlueDrank,
		GreenDrank,
	}

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
			print("Grabbed ingredient " + ingredient.ToString());
			return base.Interact (player);
		}
	}
}