using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Player controller script.
	public class PlayerController : MonoBehaviour {

		#region Controls

		// Treshold for making sure the mouse doesn't snap around.
		private const float LOOK_SNAP_THRESHOLD = 0.5f;

		// Movement speed.
		[SerializeField]
		private float movementSpeed = 3;

		// If set to true, inverts Y camera rotation.
		[SerializeField]
		private bool invertLookY = false;

		// Horizontal rotation look sensitivity.
		[SerializeField]
		private float horizontalLookSensitivity = 15f;

		// Vertical rotation look sensitivity.
		[SerializeField]
		private float verticalLookSensitivity = 15f;

		// Minimum Y look rotation
		[SerializeField]
		private float minLookRotationY = -60f;

		// Maximum Y look rotation.
		[SerializeField]
		private float maxLookRotationY = 60f;

		// Cached Y look rotation.
		private float lookRotationY = 0;

		// Reference to attached Camera component.
		private Camera cam;

		// The currently targeted selectable.
		private SelectableObject target;

		#endregion

		// Maximum number of ingredients in a recipe.
		private const int MAX_INGREDIENTS = 3;

		// Event that emits when the player adds an ingredient to the pending recipe.
		public Action<IngredientType> OnAddIngredient;

		// Event that emits when the player mixes their ingredients. Includes the new ingredient type.
		public Action<IngredientType> OnMixIngredients;

		// Event that emits when the player dumps their ingredients.
		public Action OnDumpIngredients;

		// The current pending list of recipes.
		private List<IngredientType> pendingRecipe = new List<IngredientType> ();

		// Initialize this component.
		void Start () {
			Cursor.lockState = CursorLockMode.Locked;
			cam = GetComponentInChildren<Camera> ();
		}

		// Update this component between frames.
		void Update () {

			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");

			GetComponent<Rigidbody> ().velocity = transform.TransformDirection (h, 0, v) * movementSpeed;

			// Update rotation via mouse motion.
			if (Cursor.lockState == CursorLockMode.Locked) {

				// Get mouse axes.
				float mouseX = Mathf.Clamp (Input.GetAxis ("Mouse X"), -LOOK_SNAP_THRESHOLD, LOOK_SNAP_THRESHOLD);
				float mouseY = Mathf.Clamp (Input.GetAxis ("Mouse Y"), -LOOK_SNAP_THRESHOLD, LOOK_SNAP_THRESHOLD);

				// Calculate rotation.
				float rotX = transform.localEulerAngles.y + mouseX * horizontalLookSensitivity;
				lookRotationY = Mathf.Clamp (lookRotationY + mouseY * verticalLookSensitivity, minLookRotationY, maxLookRotationY);

				// Apply rotation.
				transform.localEulerAngles = new Vector3 (0, rotX, 0);
				cam.transform.localEulerAngles = new Vector3 (lookRotationY * (invertLookY ? 1 : -1), 0, 0);

				// Raycast seek selectables.
				RaycastHit hit;
				Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10);
				target = hit.transform != null ? hit.transform.GetComponent<SelectableObject> () : null;

				// On click, interact.
				if(target != null && Input.GetMouseButtonUp(0)) {
					target.Interact (this);
				}
			}

			#if UNITY_EDITOR || DEBUG

			// In editor/debug builds, toggle mouse lock with tilde key.
			if (Input.GetKeyUp (KeyCode.BackQuote)) {
				Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
			}

			#endif
		}

		// Add an ingredient to the list the player is holding.
		public void AddIngredient (IngredientType ingredient) {

			// Add ingredient to pending recipe.
			if (pendingRecipe.Count < 3) {
				pendingRecipe.Add (ingredient);

				// Emit add event.
				if (OnAddIngredient != null) {
					OnAddIngredient (ingredient);
				}
			}
		}

		// Mix the player's ingredients.
		public void MixIngredients () {

			// Ignore if no ingredients held.
			if(pendingRecipe.Count > 0) {

				// Mix recipe.
				IngredientType recipe = Ingredients.GetMix (pendingRecipe);
				pendingRecipe.Clear ();
				pendingRecipe.Add (recipe);

				// Fire event.
				if(OnMixIngredients != null) {
					OnMixIngredients (recipe);
				}
			}
		}

		// Dump out current ingredients.
		public void DumpIngredients () {

			// Clear recipe.
			pendingRecipe.Clear ();

			// Fire event.
			if(OnDumpIngredients != null) {
				OnDumpIngredients ();
			}
		}
	}
}
