using UnityEngine;
using System.Collections;

// MouseLook rotates the transform based on the mouse delta.
// To make an FPS style character:
// - Create a capsule.
// - Add the MouseLook script to the capsule.
//   -> Set the mouse look to use MouseX. (You want to only turn character but not tilt it)
// - Add FPSInput script to the capsule
//   -> A CharacterController component will be automatically added.
//
// - Create a camera. Make the camera a child of the capsule. Position in the head and reset the rotation.
// - Add a MouseLook script to the camera.
//   -> Set the mouse look to use MouseY. (You want the camera to tilt up and down like a head. The character already turns.)

[AddComponentMenu("Control Script/Mouse Look")]
public class MouseLook : MonoBehaviour {
	public enum RotationAxes {
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}
	public RotationAxes axes = RotationAxes.MouseXAndY;

	public float sensitivityHor = 9.0f;
	public float sensitivityVert = 9.0f;
	
	public float minimumVert = -45.0f;
	public float maximumVert = 45.0f;

	private float _rotationX = 0;
    public Camera cam;
	
	void Start() {
		// Make the rigid body not change rotation
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
			body.freezeRotation = true;
	}

	void Update() {
		if (axes == RotationAxes.MouseX) {
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
		}
		else if (axes == RotationAxes.MouseY) {
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
			
            cam.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
		else {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            cam.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
		}
	}
}