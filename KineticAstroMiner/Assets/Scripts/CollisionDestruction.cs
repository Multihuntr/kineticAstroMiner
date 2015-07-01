using UnityEngine;
using System.Collections;

public class CollisionDestruction : MonoBehaviour, ILaserable
{
	public GameObject explosionspawn;
	public int destructionThreshold;
	private Vector2 hitspot;
	//public GameObject resource;

	void OnCollisionEnter2D (Collision2D other)
	{
		hitspot = other.contacts [0].point;
		if (other.relativeVelocity.sqrMagnitude > destructionThreshold && other.gameObject.name == "Asteroid(Clone)") {
			//Instantiate (resource, transform.position, Quaternion.identity);
			Destroy (gameObject);
			Instantiate (explosionspawn, hitspot, Quaternion.identity);
		}
	}

	public void lasered ()
	{
		Debug.Log ("An asteroid got lasered");
	}
}
