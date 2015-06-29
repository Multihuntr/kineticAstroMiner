using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour
{
	public float LinSpd;
	public float DecelerationProximityThresholdFactor;

	public float RotSpd;
	public float RotSlow;


	private bool launching;
	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private float ProximityThreshold;

	//stuff that isnt variables starts here
	void movement (Vector2 CurrentPosition)
	{
		Vector2 dist = (TargetPosition - CurrentPosition);
		if (dist.sqrMagnitude < ProximityThreshold) {
			launching = false;
		}
		rigBody.AddForce (transform.up * LinSpd);
	}

	bool rotation (Vector2 CurrentPosition)
	{
		// Get the angles you need
		float currAng = normaliseAngle (transform.eulerAngles.z);
		float targetAng = normaliseAngle (Mathf.Atan2 (TargetPosition.y - CurrentPosition.y, TargetPosition.x - CurrentPosition.x) * Mathf.Rad2Deg - 90);
		// Compare them
		float dif = targetAng - currAng;
		float abDif = Mathf.Abs (dif);

		// Is it close enough to what you want?
		bool closeEnough = abDif < RotSlow;
		if (!closeEnough) {
			// Using dif/abDif gives you a +1 or -1 which you multiply by the acceleration variable
			rigBody.AddTorque (dif / abDif * RotSpd);
		} else {
			// Snap it to the rotation you want. Hopefully no one will notice the snap!
			rigBody.angularVelocity = 0;
			rigBody.rotation = targetAng;
			transform.eulerAngles = new Vector3 (0, 0, targetAng);
		}

		return !closeEnough;
	}

	float normaliseAngle (float a)
	{
		a += 360;
		return a % 360;
	}

	// Use this for initialization
	void Start ()
	{
		launching = false;
		rigBody = GetComponent<Rigidbody2D> ();
		TargetPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float playerSize = GetComponent<CircleCollider2D> ().radius;
		Vector2 CurrentPosition = transform.position;
		Vector2 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if ((Input.GetMouseButtonDown (0)) && ((MousePos - CurrentPosition).sqrMagnitude > playerSize * playerSize) && !launching) {
			launching = true;
			TargetPosition = MousePos;
			ProximityThreshold = (TargetPosition - CurrentPosition).sqrMagnitude / DecelerationProximityThresholdFactor;
		}
		if (launching) {
			bool rotating = rotation (CurrentPosition);
			if (!rotating) {
				movement (CurrentPosition);
			}
		}
	}
}