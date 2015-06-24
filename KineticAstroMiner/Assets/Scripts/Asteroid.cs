using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	public float minPush;
	public float maxPush;
	public float minRot;
	public float maxRot;

	Rigidbody2D rb;
	Vector2 aimAt;

	void Start ()
	{
		// Easy reference to the RigidBody
		rb = GetComponent<Rigidbody2D> ();
		// Turn interpolation on so that slow-down effects will be smooth
		rb.interpolation = RigidbodyInterpolation2D.Interpolate;

		// Apply a random force and rotation, and let it free.
		Vector2 twoDPos = gameObject.transform.position;
		float forceFactor = UnityEngine.Random.Range (minPush, maxPush);
		rb.AddForce (forceFactor * (aimAt - twoDPos));
		float rotationFactor = UnityEngine.Random.Range (minRot, maxRot);
		rb.AddTorque (rotationFactor);
	}

	void Update ()
	{
		// If the Asteroid is too far away from the camera
		Vector2 camDist = Camera.main.transform.position;
		Vector2 thisDist = gameObject.transform.position;
		if ((thisDist - camDist).sqrMagnitude > 400) {
			Destroy (gameObject);
		}
	}

	void startRandom ()
	{
		// Pick random point in play area
		float x = UnityEngine.Random.value;
		float y = UnityEngine.Random.value;
		aimAt = Camera.main.ViewportToWorldPoint (new Vector2 (x, y));
	}
}
