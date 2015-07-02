using UnityEngine;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour,ILaserable
{
	public GameObject explosionspawn;
	public float OmNomAngle;
	private Vector2 hitspot;

	void OnCollisionEnter2D (Collision2D other)
	{
		Vector2 targetDir = other.transform.position - gameObject.transform.position;
		Vector2 forward = transform.up;
		float angle = Vector2.Angle (targetDir, forward);
		if (other.gameObject.name == "Resource(Clone)") {
			GetComponent<Hull> ().repair (150);
			Score.addScore (2500);
			Destroy (other.gameObject);
		} else if (angle < OmNomAngle) {
			Cargo cargo = other.gameObject.GetComponent<Cargo> ();
			if (cargo != null) {
				cargo.youAreEaten ();
			}
		} else if (angle > (OmNomAngle + 5)) {
			Destroy (other.gameObject);
			hitspot = other.contacts [0].point;
			Instantiate (explosionspawn, hitspot, Quaternion.identity);
			GetComponent<Hull> ().damage (50);
		}
	}
	
	public void lasered ()
	{
		GetComponent<Hull> ().damage (3);
	}
}
