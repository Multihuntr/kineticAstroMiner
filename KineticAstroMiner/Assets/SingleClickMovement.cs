using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour
{
	public float acceleration;
	public float maxspeed;
	public float minspeed;
	public float decelarationDistance;
	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private Vector2 CurrentPosition;
	public Vector2 PlayerVelocity;

	//stuff that isnt variables starts here

	// Use this for initialization
	void Start ()
	{
		rigBody = GetComponent<Rigidbody2D> ();
		TargetPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float playerSize = GetComponent<CircleCollider2D> ().radius;
		CurrentPosition = gameObject.transform.position;
		Vector2 MousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if ((Input.GetMouseButtonDown (0)) && ((MousePos - CurrentPosition).sqrMagnitude > playerSize * playerSize)) {
			TargetPosition = MousePos;
			transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((TargetPosition.y - transform.position.y), (TargetPosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
		}
		if ((TargetPosition - CurrentPosition).sqrMagnitude > decelarationDistance) {
			if (rigBody.velocity.sqrMagnitude < maxspeed) {
				rigBody.AddForce (transform.up * Time.deltaTime * acceleration);
			}
		} else if (rigBody.velocity.sqrMagnitude > minspeed) {
			rigBody.AddForce ((transform.up * Time.deltaTime * acceleration) * -1);
		} else {
			transform.position = TargetPosition;
			rigBody.velocity = Vector2.zero;
		}
	}
}