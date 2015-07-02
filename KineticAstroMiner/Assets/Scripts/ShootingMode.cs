using UnityEngine;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using System.Linq;
#endif

public class ShootingMode : MonoBehaviour
{
	public static bool Active = false;

	public Light ambient;

	private float TimeScaleTarget = 1;
	private Rigidbody2D rb;

#if UNITY_IOS || UNITY_ANDROID
	// Selects the 8th layer (player)
	private int playerMask = 1 << 8;
#endif

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{

#if UNITY_IOS || UNITY_ANDROID
		if (!Active) {
			if (checkTouchOnPlayer (TouchPhase.Began)) {
				activate ();
			}
		} else {
			Touch? touch = Input.touches.First (t => t.phase == TouchPhase.Moved);
			Vector2 aimAt = touch.Value.position;

			instantlyFacePoint (Camera.main.ScreenToWorldPoint (aimAt));
			
				// Should leave if there is a touch end on the player object
			if (checkTouchOnPlayer (TouchPhase.Ended)) {
				deactivate ();
			}
		}
#else
		if (Input.GetKeyDown (KeyCode.Space)) {
			toggleActivate ();
		}
		if (Active) {
			instantlyFacePoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			if (Input.GetMouseButtonDown (0)) {
				fire ();
			}
		}

#endif


		Time.timeScale = towards (Time.timeScale, TimeScaleTarget);
		ambient.intensity = towards (ambient.intensity, 1 + 7 * (1 - TimeScaleTarget));
	}

	
	
#if UNITY_IOS || UNITY_ANDROID
	bool checkTouchOnPlayer (TouchPhase phase)
	{
		// Return true if any t matches the lambda
		//	The lambda checks that it's in the right phase and is directed onto the player.
		return Input.touches.Any (t => t.phase == phase 
		                          && Physics.Raycast (Camera.main.ScreenPointToRay (t.position), Mathf.Infinity, playerMask));
	}
#endif


	void toggleActivate ()
	{
		if (!SingleClickMovement.launching) {
			if (Active) {
				deactivate ();
			} else {
				activate ();
			}
		}
	}

	void deactivate ()
	{
		Active = false;
		TimeScaleTarget = 1;
	}
	
	void activate ()
	{
		Active = true;
		TimeScaleTarget = 0.5f;
		rb.angularVelocity = 0;
	}

	void fire ()
	{
		if (Cargo.some ()) {
			// Get the shot and load the cannon
			GameObject shot = Cargo.loadTheCannon ();

			// Position it with the player
			shot.transform.position = transform.position;

			// Move it to the 'shotsFired' layer so that it doesn't collide with the player
			shot.layer = 9;

			// Find the launch direction in world space by translating the player objects 'up' vector
			Vector2 launchDir = transform.up;

			// FIRE!
			shot.GetComponent<Rigidbody2D> ().AddForce (launchDir * 1000);
			rb.AddForce (-launchDir * 100);
		}
	}

	void instantlyFacePoint (Vector2 TargetPos)
	{		
		// Trig. stuff! Uses rise/run to calculate the inverse tan in 2 dimensions.
		float yDif = TargetPos.y - transform.position.y;
		float xDif = TargetPos.x - transform.position.x;
		float angle = Mathf.Atan2 (yDif, xDif) * Mathf.Rad2Deg - 90;
		// Then we apply that rotation to the graphics AND the physics.
		transform.eulerAngles = new Vector3 (0, 0, angle);
		// We have to apply it to the physics because the physics engine is being slowed to a stop
		rb.rotation = angle;
	}
	
	private float towards (float orig, float target, float rate = 0.1f, float snap = 0.05f)
	{
		float dif = orig - target;
		if (Mathf.Abs (dif) < snap) {
			return target;
		} else {
			return orig - dif * rate;
		}
	}


}
