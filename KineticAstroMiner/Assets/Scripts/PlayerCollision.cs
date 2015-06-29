using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
	public float minOmNomAngle;
	public float maxOmNomAngle;
	public float asteroidCargo;
	public float enemyFighterCargo;

	void OnCollisionEnter2D (Collision2D other)
	{
		Vector3 targetDir = other.transform.position - gameObject.transform.position;
		Vector3 forward = transform.up;
		float angle = Vector3.Angle (targetDir, forward);
		if (angle > minOmNomAngle && angle < maxOmNomAngle) {
			Destroy (other.gameObject);
			if (other.gameObject.name == "Asteroid(Clone)") {
				asteroidCargo += 1;
			} else if (other.gameObject.name == "enemyFightClone") {
				enemyFighterCargo += 1;
			}

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
