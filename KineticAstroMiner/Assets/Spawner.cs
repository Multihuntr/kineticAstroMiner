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
		Vector2 pos = 18 * new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
		GameObject spawned = Instantiate (toSpawn, pos, Quaternion.identity) as GameObject;
		spawned.SendMessage ("startRandom");
	}
}
