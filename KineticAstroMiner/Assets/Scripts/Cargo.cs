using UnityEngine;
using System.Collections.Generic;

public abstract class Cargo : MonoBehaviour
{
	private static List<GameObject> hold;

	public Cargo ()
	{
		if (hold == null) {
			hold = new List<GameObject> ();
		}
	}

	void stripPhysicsInteractions ()
	{
		// Disable interactions
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.collisionDetectionMode = CollisionDetectionMode2D.None;
		rb.angularVelocity = 15;
		rb.angularDrag = 0;
		rb.velocity = Vector2.zero;
		GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void addToHold ()
	{
		stripPhysicsInteractions ();
		gameObject.transform.localScale -= new Vector3 (0.7f, 0.7f, 0);
		hold.Add (gameObject);
		rejigPositions ();
	}

	void rejigPositions ()
	{
		for (int i = 0; i < hold.Count; ++i) {
			Vector2 xy = Camera.main.ViewportToWorldPoint (new Vector2 (1 - (float)i / hold.Count / 3 - 0.02f, 0.05f));
			hold [i].transform.position = new Vector3 (xy.x, xy.y, 5);
		}
	}

	public abstract void youAreEaten ();
}
