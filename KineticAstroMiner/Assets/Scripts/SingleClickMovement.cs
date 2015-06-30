using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour
{
	public float LinSpd;
	public float DecelerationProximityThresholdFactor;

	public float RotSpd;
	public float RotSlow;


	public static bool launching;



	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private float ProximityThreshold;

	//stuff that isnt variables starts here
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
		// Check if the player has clicked
		Vector2 CurrentPosition = transform.position;
		Vector2 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0) && !launching && !ShootingMode.Active) {
			launching = true;
			TargetPosition = MousePos;
			// We assign a distance before the player should slow down
			ProximityThreshold = (TargetPosition - CurrentPosition).sqrMagnitude / DecelerationProximityThresholdFactor;
		}
	}

	void FixedUpdate ()
	{
		// Handle movement
		if (launching && !ShootingMode.Active) {
			//First we check if we should be rotating to face the correct position.
			Vector2 CurrentPosition = transform.position;
			bool rotating = rotation (CurrentPosition);
			if (!rotating) {
				// If we are facing the right way, then go forwards!
				rigBody.AddForce (transform.up * LinSpd);
				// Then we are considered to be finished launching if the distance is less than the proximity threshold
				launching = ((TargetPosition - CurrentPosition).sqrMagnitude > ProximityThreshold);
			}
		}
	}
}