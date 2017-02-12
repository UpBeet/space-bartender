using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Enumerator for ingredient types in the game.
	[Flags]
	public enum IngredientType {
		Nothing = 0,
		RedDrank = 1 << 0,
		BlueDrank = 1 << 1,
		GreenDrank = 1 << 2,
		PurpleDrank = RedDrank ^ BlueDrank,
		CyanDrank = BlueDrank ^ GreenDrank,
	}

	// Static class with manifest functionality for ingredients.
	public static class Ingredients {

		// Returns the combination of two ingredients.
		public static IngredientType GetMix (params IngredientType[] ingredients) {
			IngredientType flag = 0;
			foreach (IngredientType i in ingredients) {
				flag ^= i;
			}
			return flag;
		}

		// GetMix override for list object.
		public static IngredientType GetMix (List<IngredientType> ingredients) {
			return GetMix (ingredients.ToArray ());
		}

		// Returns the sprite for the specified ingredient.
		public static Sprite GetIngredientSprite (IngredientType ingredient) {
			switch (ingredient) {
			case IngredientType.RedDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[0];
			case IngredientType.BlueDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[1];
			case IngredientType.GreenDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[2];
			case IngredientType.PurpleDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[8];
			case IngredientType.CyanDrank: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[23];
			default: return Resources.LoadAll<Sprite> ("Sprites/potion_tileset")[20];
			}
		}
	}
}
