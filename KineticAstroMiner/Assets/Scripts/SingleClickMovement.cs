using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour
{
	public float maxSpd;
	public float snapdistance;
	public float driftSpd;

	private bool launching;
	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private Vector2 OriginalDirection;

	//stuff that isnt variables starts here
	void movement (Vector2 CurrentPosition)
	{
		Vector2 dist = (TargetPosition - CurrentPosition);
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((TargetPosition.y - transform.position.y), (TargetPosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);

		// If the distance to the object is within range to snap to it, then do so, else modify the speed vector
		if (dist.sqrMagnitude < snapdistance * snapdistance) {
			launching = false;
			transform.position = TargetPosition; //SNAP!
			rigBody.velocity = OriginalDirection.normalized * driftSpd; //Drift
			rigBody.angularVelocity = 0;
		} else {
			// http://www.wolframalpha.com/input/?i=20-1%2F%28x%2B1%2F%2820-1%29+-+0.1%29+%2B+1%2F%28x-1%2F%2820-1%29+-+5%29
			float to = dist.magnitude; 					// x
			// maxSpd									// '20'
			float orig = OriginalDirection.magnitude;	// '5'
			// snapdistance 							// '0.1'

			// We wanted roots at snapdistance and orig, but the small value of the other terms were throwing that off
			//	So I went with an approximation.
			float newVel = maxSpd - 1 / (to + 1 / (maxSpd - 1) - snapdistance) + 1 / (to - 1 / (maxSpd - 1) - orig);
			rigBody.velocity = transform.up * newVel;
		}
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
			OriginalDirection = (TargetPosition - CurrentPosition);
		}
		if (launching) { 
			movement (CurrentPosition);
		}
	}
}