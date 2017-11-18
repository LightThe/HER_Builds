using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;

public class ControleLateral : MonoBehaviour
{
    #region variaveis
    public float vel, vRot;
    private Rigidbody2D rb2d;
    private Animator anim;
	Legendas leg;

    //HASHES DO ANIMATOR
    int vHash = Animator.StringToHash("Velocidade");
    int rHash = Animator.StringToHash("Inclinacao");
    #endregion
	IEnumerator TravaL03(){
		leg.f1 = 1;
		yield return new WaitForSeconds (10);
		leg.f1 = 0;

	}
    #region Start e Updates
    void Start()
    {
		leg = GetComponent<Legendas> ();
        anim = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
		if (SceneManager.GetActiveScene ().name == "L03") {
			StartCoroutine (TravaL03 ());
		}
    }

    void FixedUpdate()
    {
		if (leg.f1 == 0) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector2 force = new Vector2 (moveHorizontal * vRot, moveVertical*vel);
			if (moveVertical != 1) {
				moveVertical = 0;
				anim.SetFloat (vHash, moveVertical);
			} else
				anim.SetFloat (vHash, moveVertical);
			anim.SetInteger (rHash, (int)moveHorizontal);
			rb2d.AddForce (force, ForceMode2D.Impulse);
		}
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
		other.gameObject.SetActive(false);
    }
}
