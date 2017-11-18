using UnityEngine;
using System.Collections;

public static class initiate{
	public static void Fade(string Scene, Color col, float damp){
		GameObject init = new GameObject ();
		init.name = "fader";
		init.AddComponent<Fader> ();
		Fader scr = init.GetComponent<Fader>();
		scr.FadeDamp = damp;
		scr.fadeScene = Scene;
		scr.fadeColor = col;
		scr.start = true;
	}
}
