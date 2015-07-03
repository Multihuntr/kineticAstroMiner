using UnityEngine;
using System.Collections.Generic;

public abstract class Cargo : MonoBehaviour
{
	private static List<GameObject> hold;

	public Cargo ()
	{
		// Every time an object spawns, it will check if the hold is empty.
		//	This could be improved with some restructuring.
		if (hold == null) {
			hold = new List<GameObject> ();
		}
	}

	public static void empty ()
	{
		hold = new List<GameObject> ();
	}

	public static bool remove (GameObject item)
	{
		bool result = hold.Remove (item);
		rejigPositions ();
		return result;
	}

	public static GameObject loadTheCannon ()
	{
		// Get the last thing to enter the hold
		GameObject shot = hold [hold.Count - 1];
		// Re-enable Physics
		shot.GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		shot.GetComponent<CircleCollider2D> ().enabled = true;
		// Re-Adjust size
		shot.transform.localScale += new Vector3 (0.7f, 0.7f, 0);
		// Remove it from the list
		remove (shot);
		// Plug your ears
		return shot;
	}

	public static bool some ()
	{
		return (hold.Count > 0);
	}
	
	static void rejigPositions ()
	{
		for (int i = 0; i < hold.Count; ++i) {
			Vector2 xy = Camera.main.ViewportToWorldPoint (new Vector2 (1 - (float)i / hold.Count / 3 - 0.02f, 0.05f));
			hold [i].transform.position = new Vector3 (xy.x, xy.y, 5);
		}
	}

	void stripPhysicsInteractions ()
	{
		// Disable interactions
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.collisionDetectionMode = CollisionDetectionMode2D.None;
		rb.angularVelocity = 15;
		// Prepare to be launched, sucka
		rb.drag = 0;
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

	public abstract void youAreEaten ();
}
