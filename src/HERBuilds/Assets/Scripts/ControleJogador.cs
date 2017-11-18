using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    #region variaveis
    public float vel, vRot;
	private Rigidbody2D rb2d;
    private Animator anim;
	Legendas leg;

	//HASHES DO ANIMATOR
	int vHash = Animator.StringToHash("Velocidade");
	int rHash = Animator.StringToHash("Rotacao");
    #endregion
	#region Co-routines
	IEnumerator fadeMovimento(){
		float velBak = vel;
		vel = 1.2f;
		//vel = Mathf.Lerp (vel, -0.1f, 0.5f*Time.deltaTime);
		yield return new WaitForSeconds(3);
		vel = velBak;
		//vel = Mathf.Lerp (-0.1f, vel, 0.5f*Time.deltaTime);
	}
	#endregion
    #region Start e Updates
    void Start () {
		anim = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
		leg = GetComponent<Legendas> ();
		if (SceneManager.GetActiveScene ().name == "L04")
			anim.SetTrigger ("entry");
		
	}

	void FixedUpdate () {
		if(leg.f1 == 0){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        Vector2 force = new Vector2(0, moveVertical);
        Vector3 angles = new Vector3(0, 0, moveHorizontal * (-1));
        if (moveVertical != 1) {
			moveVertical = 0;
			anim.SetFloat (vHash, moveVertical);
		} else
			anim.SetFloat (vHash, moveVertical);
		anim.SetInteger (rHash, (int)moveHorizontal);
        rb2d.transform.Rotate(angles*vRot);
        rb2d.AddRelativeForce(force*vel, ForceMode2D.Impulse);
	}
	}
    #endregion
	void OnTriggerEnter2D(Collider2D other){
		if(SceneManager.GetActiveScene().name == "L01")
		leg.otherCount += 1;
		if (other.gameObject.CompareTag ("Important") && SceneManager.GetActiveScene ().name == "tutorial")
		StartCoroutine (fadeMovimento ());
			
	}
}