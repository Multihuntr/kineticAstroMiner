using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour
{
	public GUISkin hullskin;
	public float maxhealth;
	private float currenthealth;
	private float healthpercentage;
	// Use this for initialization
	public void damage (int attackvalue)
	{
		CameraShake.go (attackvalue);
		currenthealth -= attackvalue;
		if (currenthealth < 1) {
			death ();
		}
	}
	public void repair (int repairvalue)
	{
		currenthealth = Mathf.Min (currenthealth + repairvalue, maxhealth);
	}
	void death ()
	{
		Cargo.empty ();
		Application.LoadLevel (Application.loadedLevel);
	}
	void Start ()
	{
		currenthealth = maxhealth;
	}
	// Update is called once per frame
	void Update ()
	{
		healthpercentage = currenthealth / maxhealth;
	}
	void OnGUI ()
	{
		GUI.skin = hullskin;
		GUI.Label (new Rect (Screen.width - 230, 10, 106, 30), healthpercentage.ToString ("HULL 000%"));
	}
}
