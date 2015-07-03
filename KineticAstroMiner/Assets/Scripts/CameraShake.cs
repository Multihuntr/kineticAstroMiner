using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public static void go (float intensity)
	{
		Camera.main.GetComponent<CameraShake> ().shake (intensity);
	}

	private static float remaining;

	void shake (float intensity)
	{
		if (remaining <= 0) {
			StartCoroutine (shaking (intensity));
		}
	}

	IEnumerator shaking (float intensity)
	{
		remaining += intensity;
		int i = 0;
		while (remaining > 0.1) {
			++i;
			float reduction = Mathf.Pow (remaining, (float)1 / 4);
			remaining -= reduction;

			// Get angle to move camera
			float currAng = 180 - Mathf.Atan2 (transform.position.x, transform.position.y) * Mathf.Rad2Deg;
			float ShakeAngle = Random.Range (currAng - 45, currAng + 45);

			// Get amount to move camera
			float ShakeAmount = reduction / 10 * Mathf.PerlinNoise (i * 0.01f, i * 0.05f);
			Vector2 dir = ShakeAmount * (new Vector2 (Mathf.Cos (ShakeAngle), Mathf.Sin (ShakeAngle)));

			// Move camera
			transform.Translate (new Vector2 (dir.x, dir.y));
			yield return null;
		}
		transform.position = new Vector3 (0, 0, -10);
		remaining = 0;
	}
}
