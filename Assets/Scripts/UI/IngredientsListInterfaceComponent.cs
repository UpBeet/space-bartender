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
			player.OnAddIngredient += AddIngredient;
		}

		// Called when this component is destroyed.
		void OnDestroy() {

			// Stop listening to events emitted from player.
			player.OnAddIngredient -= AddIngredient;
		}

		// Adds an ingredient to the list.
		private void AddIngredient(IngredientType ingredient) {

			// Add a new image to the list.
			Image img = new GameObject (ingredient.ToString ()).AddComponent<Image> ();
			img.transform.SetAndClampParent (transform);
			img.sprite = IngredientContainer.GetIngredientSprite (ingredient);
		}
	}
}
