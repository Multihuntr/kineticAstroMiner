using UnityEngine;
using System.Collections;

public class CollisionDestruction : MonoBehaviour, ILaserable
{
	public GameObject explosionspawn;
	public GameObject resource;
	public int destructionThreshold;
	private Vector2 hitspot;
	private float lasercount;
	//public GameObject resource;

	void OnCollisionEnter2D (Collision2D other)
	{
		Rigidbody2D rigbody = GetComponent<Rigidbody2D> ();
		hitspot = other.contacts [0].point;
		if (other.relativeVelocity.sqrMagnitude > destructionThreshold && other.gameObject.name == "Asteroid(Clone)") {
			death ();
			if (other.rigidbody.velocity.sqrMagnitude > rigbody.velocity.sqrMagnitude) {
				Instantiate (resource, hitspot, Quaternion.identity);
			}
		}
	}
	void death ()
	{
		Destroy (gameObject);
		Instantiate (explosionspawn, hitspot, Quaternion.identity);
	}
	public void lasered ()
	{
		lasercount += 1;
		if (lasercount > 20) {
			Destroy (gameObject);
			death ();
		}
	}
	void Start ()
	{
		lasercount = 0;	
	}
}
