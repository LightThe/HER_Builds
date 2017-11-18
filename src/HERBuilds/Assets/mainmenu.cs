using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour {
	public Color corFadeMenu = Color.black;
	void Start () {
		Time.timeScale = 1.0f;
		if (SceneManager.GetActiveScene ().name == "HER")
			initiate.Fade ("Menu", corFadeMenu, 0.1f);
	}
}
