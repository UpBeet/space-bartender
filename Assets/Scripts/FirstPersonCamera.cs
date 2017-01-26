using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBartender {

	// Controls the first person camera.
	[RequireComponent(typeof(Camera))]
	public class FirstPersonCamera : MonoBehaviour {

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
				float rotX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * horizontalSensitivity;
				rotY = Mathf.Clamp (rotY + Input.GetAxis ("Mouse Y") * verticalSensitivity, minRotationY, maxRotationY);
				transform.localEulerAngles = new Vector3 (rotY, rotX, 0);
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
