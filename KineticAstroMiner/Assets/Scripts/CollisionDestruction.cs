using UnityEngine;
using System.Collections;

public class CollisionDestruction : MonoBehaviour, ILaserable
{
	public GameObject explosionspawn;
	public GameObject resource;
	public int destructionThreshold;
	public int laserHitMax;

	private Vector2 hitspot;
	private float lasercount = 0;

	void OnDestroy ()
	{
		Cargo.remove (gameObject);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		Rigidbody2D rigbody = GetComponent<Rigidbody2D> ();
		if (other.relativeVelocity.sqrMagnitude > destructionThreshold && other.gameObject.name != "Player") {
			hitspot = other.contacts [0].point;
			death (hitspot);
			CameraShake.go (10);

			if (other.gameObject.name == "Asteroid(Clone)" && gameObject.name == "Asteroid(Clone)" 
				&& other.rigidbody.velocity.sqrMagnitude > rigbody.velocity.sqrMagnitude) {

				Instantiate (resource, hitspot, Quaternion.identity);
			}
		}
	}

	void death (Vector3 loc)
	{
		Destroy (gameObject);
		Instantiate (explosionspawn, loc, Quaternion.identity);
	}

	public void lasered ()
	{
		lasercount += 1;
		if (lasercount > laserHitMax) {
			Destroy (gameObject);
			death (transform.position);
		}
	}
}
