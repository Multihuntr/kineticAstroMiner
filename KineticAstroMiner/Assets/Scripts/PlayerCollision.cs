using UnityEngine;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{
	public GameObject explosionspawn;
	public float OmNomAngle;
	private Vector2 hitspot;

	void OnCollisionEnter2D (Collision2D other)
	{
		Vector2 targetDir = other.transform.position - gameObject.transform.position;
		Vector2 forward = transform.up;
		float angle = Vector2.Angle (targetDir, forward);
		if (angle < OmNomAngle) {
			Cargo cargo = other.gameObject.GetComponent<Cargo> ();
			if (cargo != null) {
				cargo.youAreEaten ();
			}
		} else if (angle > (OmNomAngle + 5)) {
			Destroy (other.gameObject);
			hitspot = other.contacts [0].point;
			Instantiate (explosionspawn, hitspot, Quaternion.identity);
			//hull damage will go here!
		}
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
