using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d; //variavel responsavel pela ligação do rigidbody2D com o objeto no editor.
	public float speed;//velocidade com que a força é aplicada ao objeto
	public float rotationSpeed;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> (); //acessando o objeto no editor.
	}

	//Ctrl+' em um texto selecionado busca aquele texto dentro do site do Unity API. Bem simples :)
	//fixed update, atualização antes de calculo de fisica. É aqui que o autor colocou a movimentaçao do personagem.
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal"); //recebe o eixo horizontal (teclas A e D?) e armazena em 'moveHorizontal'.
		float moveVertical = Input.GetAxis ("Vertical"); //o mesmo do comentado acima, porém com o eixo vertical (W e S?).
		if (moveVertical != 1)
			moveVertical = 0;
		Vector2 movement = new Vector2(0,moveVertical); //reunindo entradas de movimento em uma variavel tipo Vector2, que guarda posição X e Y.
		Vector3 angles = new Vector3(0, 0, moveHorizontal*(-1));
		rb2d.transform.Rotate(angles*rotationSpeed);
		rb2d.AddRelativeForce(movement*speed, ForceMode2D.Impulse); //Usando variavel Vector2 para aplicar forçãs no componente RigidBody2D descrito acima;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("PickUp")){
			other.gameObject.SetActive(false);
		}
	}
}
