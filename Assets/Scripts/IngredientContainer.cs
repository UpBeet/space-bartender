using System.Collections;
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
			player.AddIngredient (ingredient);
			return base.Interact (player);
		}

		public static Sprite GetIngredientSprite (IngredientType ingredient) {
			switch(ingredient) {
				case IngredientType.RedDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[0];
				case IngredientType.BlueDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[1];
				case IngredientType.GreenDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[2];
				default: return null;
			}
		}
	}
}