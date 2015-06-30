using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	enum State
	{
		Launching,
		Targeting,
		Firing}
	;

	public float LinSpd;
	public float ProximityThresholdFactor;

	private Vector2 aimAt;
	private State state;
	private float ProximityThreshold = 2;


	void Start ()
	{
		state = State.Launching;
	}

	void Update ()
	{
		switch (state) {
		case State.Launching:
			instantlyFacePoint (aimAt);
			// Go forwards!
			GetComponent<Rigidbody2D> ().AddForce (transform.up * LinSpd);
			// We are considered to be finished launching if the distance is less than the proximity threshold
			if ((aimAt - (Vector2)transform.position).sqrMagnitude < ProximityThreshold) {
				state = State.Targeting;
				foreach (Transform child in transform) {
					Destroy (child.gameObject);
				}
			}
			break;
		case State.Targeting:
			instantlyFacePoint (GameObject.Find ("Player").transform.position);
			break;
		case State.Firing:
			break;
		default:
			break;
		}
	}

	void instantlyFacePoint (Vector2 point)
	{
		float angle = Mathf.Atan2 (point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg - 90;
		// Then we apply that rotation to the graphics AND the physics.
		transform.eulerAngles = new Vector3 (0, 0, angle);
		GetComponent<Rigidbody2D> ().rotation = angle;
	}

	void startAimedAt (Vector2 aimAt)
	{
		this.aimAt = aimAt;
		instantlyFacePoint (aimAt);
	}
}
