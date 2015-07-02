using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public GameObject toSpawn;
	public float spawnTimeMin;
	public float spawnTimeMax;

	private float timer;
	private float nextSpawn;


	// Use this for initialization
	void Start ()
	{
		setNextSpawn ();
	}

	private void setNextSpawn ()
	{
		timer = 0;
		nextSpawn = UnityEngine.Random.Range (spawnTimeMin, spawnTimeMax);
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > nextSpawn) {
			spawnObject ();
			setNextSpawn ();
		}
	}

	private void spawnObject ()
	{
		float angle = UnityEngine.Random.value * Mathf.PI * 2;
		Vector2 camPos = Camera.main.transform.position;
		float camSize = Camera.main.orthographicSize;
		Vector2 pos = camPos + camSize * 3 * new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
		GameObject spawned = Instantiate (toSpawn, pos, Quaternion.identity) as GameObject;
		
		// Pick random point in play area
		float x = (float)UnityEngine.Random.Range (300, 700) / 1000;
		float y = (float)UnityEngine.Random.Range (300, 700) / 1000;
		Vector2 aimAt = Camera.main.ViewportToWorldPoint (new Vector2 (x, y));

		spawned.SendMessage ("startAimedAt", aimAt);
	}
}
