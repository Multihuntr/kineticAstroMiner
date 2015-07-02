using UnityEngine;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour,ILaserable
{
	public GameObject explosionspawn;
	public float OmNomAngle;
	public static LinkedList <GameObject> cargo;
	private Vector2 hitspot;

	void OnCollisionEnter2D (Collision2D other)
	{
		Vector2 targetDir = other.transform.position - gameObject.transform.position;
		Vector2 forward = transform.up;
		float angle = Vector2.Angle (targetDir, forward);
		if (angle < OmNomAngle) {
			Destroy (other.gameObject);
			if (other.gameObject.name == "Asteroid(Clone)" || other.gameObject.name == "enemyFightClone") {
				cargo.AddFirst (other.gameObject);
			} else {
				GetComponent<Hull> ().repair (150);
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
	// Use this for initialization
	void Start ()
	{
		cargo = new LinkedList<GameObject> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
