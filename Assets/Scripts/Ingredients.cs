using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Enumerator for ingredient types in the game.
	[Flags]
	public enum IngredientType {
		RedDrank = 1 << 0,
		BlueDrank = 1 << 1,
		GreenDrank = 1 << 2,
	}

	// Static class with manifest functionality for ingredients.
	public static class Ingredients {

		// Returns the sprite for the specified ingredient.
		public static Sprite GetIngredientSprite (IngredientType ingredient) {
			switch (ingredient) {
			case IngredientType.RedDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[0];
			case IngredientType.BlueDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[1];
			case IngredientType.GreenDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[2];
			default: return null;
			}
		}
	}
}
