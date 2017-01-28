using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Player controller script.
	public class PlayerController : MonoBehaviour {

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

				LayerMask mask = 1 << LayerMask.NameToLayer("SelectableObjects");
				// Shoot a ray out of the camera from the object's current position
				if (Physics.Raycast(cam.transform.position, cam.transform.forward, 10, mask.value)) {
					print("There is something in front of the object!");
				}
				else {
					print("There is nothing");
				}
			}

			#if UNITY_EDITOR || DEBUG

			// In editor/debug builds, toggle mouse lock with tilde key.
			if (Input.GetKeyUp (KeyCode.BackQuote)) {
				Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
			}

			#endif
		}
	}
}
