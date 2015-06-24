using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour
{
	public float acceleration;
	public float maxspeed;
	public float minspeed;
	public float decelarationDistance;
	public float snapdistance;
	private bool launching;
	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private Vector2 CurrentPosition;
	public Vector2 PlayerVelocity;

	//stuff that isnt variables starts here
	private void movement ()
	{
		Vector2 distancecheck = (TargetPosition - CurrentPosition);
		if (distancecheck.sqrMagnitude > decelarationDistance) {
			if (rigBody.velocity.sqrMagnitude < maxspeed) {
				rigBody.AddForce (transform.up * Time.deltaTime * acceleration);
			}
		} else if (rigBody.velocity.sqrMagnitude > minspeed) {
			rigBody.AddForce ((transform.up) * -0.5f * maxspeed * maxspeed / decelarationDistance);
		} else if (distancecheck.sqrMagnitude < snapdistance) {
			launching = false;
			transform.position = TargetPosition;
			rigBody.velocity = Vector2.zero;
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
		CurrentPosition = gameObject.transform.position;
		Vector2 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.Log (rigBody.velocity);
		if ((Input.GetMouseButtonDown (0)) && ((MousePos - CurrentPosition).sqrMagnitude > playerSize * playerSize) && !launching) {
			launching = true;
			TargetPosition = MousePos;
			transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((TargetPosition.y - transform.position.y), (TargetPosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
		}
		if (launching) { 
			movement ();
		}
	}
}