using UnityEngine;
using System.Collections;

public class GoodLuck : MonoBehaviour
{	
	public GUISkin guiSkin;

	private float fade = 4;

	void Start ()
	{
	}

	void OnGUI ()
	{
		if (Time.deltaTime == 0 || Game.Paused) {
			return;
		}
		fade = fade / 1.02f;
		GUI.skin = guiSkin;
		GUI.backgroundColor = new Color (0, 0, 0, fade);
		GUI.color = new Color (1, 1, 1, fade);
		GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Good Luck.");
		if (fade < 0.01) {
			Destroy (gameObject);
		}
	}
}
