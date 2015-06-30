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

	void startAimedAt (Vector2 aimAt)
	{
		this.aimAt = aimAt;
	}
}
