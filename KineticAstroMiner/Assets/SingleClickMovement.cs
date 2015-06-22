using UnityEngine;
using System.Collections;

public class SingleClickMovement : MonoBehaviour {
	public float speed = 15f;
	private Rigidbody2D rigBody;
	private Vector2 TargetPosition;
	private Vector2 CurrentPosition;

	// Use this for initialization
	void Start () {
		rigBody = GetComponent<Rigidbody2D> ();
		TargetPosition = gameObject.transform.position;
		CurrentPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButtonDown(0)){
			TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CurrentPosition = gameObject.transform.position;
		}
	if (TargetPosition != CurrentPosition) {
			transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((TargetPosition.y - transform.position.y), (TargetPosition.x - transform.position.x))*Mathf.Rad2Deg - 90);
			rigBody.AddForce(transform.up * Time.deltaTime * speed);
		}
	}
}