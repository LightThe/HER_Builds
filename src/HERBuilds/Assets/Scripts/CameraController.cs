using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player; //posição do jogador
	private Vector3 offset; //diferença entre player e camera

	//Start é uma função executada no início do programa
	void Start(){
		offset = transform.position - player.transform.position; //calcula a diferença entre a posição do jogador e a camera

	}

	//para cameras e objetos que seguem o jogador, LateUpdate executa depois de Update() e garante que o jogador já vai ter se movido quando atualizar a camera.
	void LateUpdate(){
		transform.position = player.transform.position + offset; //alinha a camera com o player
	}
}
