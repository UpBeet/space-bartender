using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Recipes with names.
	[Flags]
	public enum NamedRecipe {
		Unnamed = 0,
		PurpleDrank = IngredientType.RedDrank ^ IngredientType.BlueDrank,
	}

	// Static class with manifest functionality for recipes of ingredients.
	public static class Recipes {

		// Returns the NamedRecipe enum for the specified list of ingredients.
		public static NamedRecipe GetRecipeName(params IngredientType[] ingredients) {

			// Build recipe flag.
			int flag = 0;
			foreach (IngredientType i in ingredients) {
				flag ^= (int)i;
			}

			// Check if recipe exists, return unnamed otherwise.
			if(Enum.IsDefined(typeof(NamedRecipe), flag)) {
				return (NamedRecipe)flag;
			}
			else {
				return NamedRecipe.Unnamed;
			}
		}

		// Override to query recipe name via a list object.
		public static NamedRecipe GetRecipeName (List<IngredientType> ingredients) {
			return GetRecipeName (ingredients.ToArray ());
		}
	}
}
