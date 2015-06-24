using UnityEngine;
using System.Collections;

public class CollisionDestruction : MonoBehaviour
{

	public int destructionThreshold;
	//public GameObject destructionAnimation;
	//public GameObject resource;

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.relativeVelocity.sqrMagnitude > destructionThreshold) {
			//Instantiate (destructionAnimation, transform.position, Quaternion.identity);
			//Instantiate (resource, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
