using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YesAndEngine.Utilities;

namespace SpaceBartender.UI {

	// Controls the ingredients list on the UI.
	public class IngredientsListInterfaceComponent : MonoBehaviour {

		// Cache reference to player so we can remove events later.
		private PlayerController player;

		// Initialize this component.
		void Start() {

			// Listen to events emitted from player object.
			player = FindObjectOfType<PlayerController> ();
			if (player != null) {
				player.OnAddIngredient += AddIngredient;
				player.OnMixIngredients += MixIngredients;
				player.OnDumpIngredients += ClearIngredients;
			}
		}

		// Called when this component is destroyed.
		void OnDestroy() {

			// Stop listening to events emitted from player.
			if (player != null) {
				player.OnAddIngredient -= AddIngredient;
				player.OnMixIngredients -= MixIngredients;
				player.OnDumpIngredients -= ClearIngredients;
			}
		}

		// Adds an ingredient to the list.
		private void AddIngredient(IngredientType ingredient) {

			// Add a new image to the list.
			Image img = new GameObject (ingredient.ToString ()).AddComponent<Image> ();
			img.transform.SetAndClampParent (transform);
			img.sprite = Ingredients.GetIngredientSprite (ingredient);
		}

		// Clears the current ingredients.
		private void ClearIngredients () {

			// Clear all children.
			foreach (Transform child in transform) {
				Destroy (child.gameObject);
			}
		}

		// Animate mixing ingredients into newIngredient.
		private void MixIngredients (IngredientType newIngredient) {
			ClearIngredients ();
			AddIngredient (newIngredient);
		}
	}
}
