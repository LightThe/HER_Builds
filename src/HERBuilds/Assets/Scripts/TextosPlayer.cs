using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextosPlayer : MonoBehaviour {
	
	public Text txtLegenda;
	public string[] legenda = new string[5];
	void Start () {
		
	}

	void Update () {
		for(int a = 0; a<5; a++){
			while (!Input.GetKeyDown (KeyCode.Space)) {
				txtLegenda.text = legenda [a];
				//txtLegenda.gameObject.SetActive (true);

			}
		}
	}
}
