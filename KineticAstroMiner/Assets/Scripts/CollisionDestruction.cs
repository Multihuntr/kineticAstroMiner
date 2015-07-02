using UnityEngine;
using System.Collections;

public class CollisionDestruction : MonoBehaviour, ILaserable
{
	public GameObject explosionspawn;
	public GameObject resource;
	public int destructionThreshold;
	private Vector2 hitspot;
	//public GameObject resource;

	void OnCollisionEnter2D (Collision2D other)
	{
		Rigidbody2D rigbody = GetComponent<Rigidbody2D> ();
		hitspot = other.contacts [0].point;
		if (other.relativeVelocity.sqrMagnitude > destructionThreshold && other.gameObject.name == "Asteroid(Clone)") {
			Destroy (gameObject);
			if (other.rigidbody.velocity.sqrMagnitude > rigbody.velocity.sqrMagnitude && other.gameObject.name == "Asteroid(Clone)") {
				Instantiate (explosionspawn, hitspot, Quaternion.identity);
				Instantiate (resource, hitspot, Quaternion.identity);
			}
		}
	}

	public void lasered ()
	{
		Debug.Log ("An asteroid got lasered");
	}
}
