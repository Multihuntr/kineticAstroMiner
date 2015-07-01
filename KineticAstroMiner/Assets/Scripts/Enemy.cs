using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	enum State
	{
		Launching,
		Targeting,
		Charging,
		Firing,
		Recharging}
	;

	public float LinSpd;
	public float ProximityThresholdFactor;
	public float lockOnTime;
	public float chargeTime;
	public float firingTime;
	public float rechargeTime;

	private Vector2 aimAt;
	private State state;
	private float ProximityThreshold;
	private float lockOn = 0;
	private float charged = 0;
	private float firing = 0;
	private float recharge = 0;

	private LineRenderer line;


	void Start ()
	{
		state = State.Launching;
		line = GetComponent<LineRenderer> ();
	}

	void FixedUpdate ()
	{
		RaycastHit2D[] hits = null;
		if (state != State.Launching && state != State.Recharging) {
			// Aim a raycast from the current position at the latest recorded relative position of the player
			// If it's in Charging or Firing, then that won't be the actual location of the player.
			hits = Physics2D.RaycastAll (transform.position, aimAt - (Vector2)transform.position);
			// If it hits anything on the way then set the LineRenderer's second point to the second 
			//	thing the raycast hit (the first thing will always be the enemy that 
			//	is targeting, for some stupid reason), else, just make it really long.
			if (hits.Length > 1 && hits [1].transform != null) {
				line.SetPosition (1, (hits [1].point - (Vector2)transform.position));
			} else {
				line.SetPosition (1, ((aimAt - (Vector2)transform.position).normalized * Camera.main.orthographicSize * 10));
			}
		}


		switch (state) {
		case State.Launching:
			instantlyFacePoint (aimAt);
			// Go forwards!
			GetComponent<Rigidbody2D> ().AddForce ((aimAt - (Vector2)transform.position).normalized * LinSpd);
			// We are considered to be finished launching if the distance is less than the proximity threshold
			if ((aimAt - (Vector2)transform.position).sqrMagnitude < ProximityThreshold) {
				// Close enough? Then move to Targeting mode, and remove the jet fire
				state = State.Targeting;
				foreach (Transform child in transform.GetChild(0)) {
					Destroy (child.gameObject);
				}
			}
			break;
		case State.Targeting:
			aimAt = GameObject.Find ("Player").transform.position;
			instantlyFacePoint (aimAt);
			if (hits != null && hits.Length > 1 && hits [1].transform != null) {
				// Try to lock on to the player				
				if (hits [1].transform.name == "Enemy(Clone)") {
					lockOn = 0;
				} else {
					lockOn += Time.fixedDeltaTime;
					if (lockOn >= chargeTime) {
						charged = 0;
						state = State.Charging;
					}
				}
			}
			break;
		case State.Charging:
			// Imma chargin' ma lazor
			charged += Time.fixedDeltaTime;
			// Power down the tracer
			float scale = Mathf.Lerp (0.05f, 0.005f, charged / chargeTime);
			line.SetWidth (scale, scale);


			if (charged >= chargeTime) {
				// I am fully charged!
				firing = 0;
				state = State.Firing;
				line.SetWidth (0.2f, 0.2f);
			}
			break;
		case State.Firing:
			// Imma firin' ma lazor
			firing += Time.fixedDeltaTime;
			if (firing >= firingTime) {
				recharge = 0;
				state = State.Recharging;
				line.SetPosition (1, Vector3.zero);
			}
			break;
		case State.Recharging:
			recharge += Time.fixedDeltaTime;
			if (recharge >= rechargeTime) {
				lockOn = 0;
				state = State.Targeting;
				line.SetWidth (0.05f, 0.05f);
			}
			break;
		default:
			break;
		}

	}

	void instantlyFacePoint (Vector2 point)
	{
		float angle = Mathf.Atan2 (point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg - 90;
		transform.GetChild (0).transform.eulerAngles = new Vector3 (0, 0, angle);
	}

	void startAimedAt (Vector2 aimAt)
	{
		this.aimAt = aimAt;
		
		ProximityThreshold = (aimAt - (Vector2)transform.position).sqrMagnitude / ProximityThresholdFactor;
		instantlyFacePoint (aimAt);
	}

	void toLockOn (Transform t)
	{
	}
}
