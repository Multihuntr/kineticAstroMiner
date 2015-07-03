using UnityEngine;
using System.Collections;

public class Asteroid : Cargo
{
	public float minPush;
	public float maxPush;
	public float minRot;
	public float maxRot;

	Vector2 aimAt;

	void Start ()
	{
		// Easy reference to the RigidBody
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		// Apply a random force and rotation, and let it free.
		Vector2 twoDPos = gameObject.transform.position;
		float forceFactor = UnityEngine.Random.Range (minPush, maxPush);
		rb.AddForce (forceFactor * (aimAt - twoDPos));
		float rotationFactor = UnityEngine.Random.Range (minRot, maxRot);
		rb.AddTorque (rotationFactor);
	}

	void Update ()
	{
		if (Game.Paused) {
			return;
		}
		// If the Asteroid is too far away from the camera
		Vector2 camDist = Camera.main.transform.position;
		float camSize = Camera.main.orthographicSize;
		Vector2 thisDist = gameObject.transform.position;
		if ((thisDist - camDist).sqrMagnitude > camSize * camSize * 9) {
			Destroy (gameObject);
		}

	}

	void startAimedAt (Vector2 aimAt)
	{
		this.aimAt = aimAt;
	}

	public override void youAreEaten ()
	{
		addToHold ();
	}
}
