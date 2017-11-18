using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {

	public bool start = false;
	public float FadeDamp = 0.0f;
	public string fadeScene;
	public float alpha = 0.0f;
	public Color fadeColor;
	public bool isFadein = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (!start)
			return;
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		Texture2D mytex = new Texture2D(1, 1);
		mytex.SetPixel (0, 0, fadeColor);
		mytex.Apply ();

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), mytex);
		if (isFadein)
			alpha = Mathf.Lerp (alpha, -0.1f, FadeDamp * Time.deltaTime);
		else
			alpha = Mathf.Lerp (alpha, 1.1f, FadeDamp * Time.deltaTime);

		if (alpha >= 1 && !isFadein) {
			SceneManager.LoadScene (fadeScene);
			DontDestroyOnLoad (gameObject);
		}
		else if (alpha <= 0 && isFadein) {
			Destroy (gameObject);
		}
	}
	void OnLevelWasLoaded(int level){
		isFadein = true;
	}
}
