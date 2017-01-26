using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Controls the first person camera.
	[RequireComponent(typeof(Camera))]
	public class FirstPersonCamera : MonoBehaviour {

		// Treshold for making sure the mouse doesn't snap around.
		private const float SNAP_THRESHOLD = 0.5f;

		// If set to true, inverts Y camera rotation.
		[SerializeField]
		private bool invertY = false;

		// Horizontal rotation sensitivity.
		[SerializeField]
		private float horizontalSensitivity = 15f;

		// Vertical rotation sensitivity.
		[SerializeField]
		private float verticalSensitivity = 15f;

		// Minimum Y rotation
		[SerializeField]
		private float minRotationY = -60f;

		// Maximum Y rotation.
		[SerializeField]
		private float maxRotationY = 60f;

		// Cached Y rotation.
		float rotY = 0;

		// Initialize this component.
		void Start () {
			Cursor.lockState = CursorLockMode.Locked;
		}

		// Update this component between frames.
		void Update () {

			// Update rotation via mouse motion.
			if(Cursor.lockState == CursorLockMode.Locked) {

				// Get mouse axes.
				float mouseX = Mathf.Clamp(Input.GetAxis ("Mouse X"), -SNAP_THRESHOLD, SNAP_THRESHOLD);
				float mouseY = Mathf.Clamp(Input.GetAxis ("Mouse Y"), -SNAP_THRESHOLD, SNAP_THRESHOLD);

				// Calculate rotation.
				float rotX = transform.localEulerAngles.y + mouseX * horizontalSensitivity;
				rotY = Mathf.Clamp (rotY + mouseY * verticalSensitivity, minRotationY, maxRotationY);

				// Apply rotation.
				transform.localEulerAngles = new Vector3 (rotY * (invertY ? 1 : -1), rotX, 0);
			}

			#if UNITY_EDITOR || DEBUG

			// In editor/debug builds, toggle mouse lock with tilde key.
			if(Input.GetKeyUp(KeyCode.BackQuote)) {
				Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
			}

			#endif
		}
	}
}
