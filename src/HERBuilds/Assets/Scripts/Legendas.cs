using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;

public class Legendas : MonoBehaviour
{
    #region variaveis
    public Text txtOther, txtLgnd;
	public string[] strOther = new string[5], legenda = new string[5], LegSeq = new string[4];
    private GameObject barreira, menuobj;
    private Animator txtAnim;
	private int rndComp = 0, i = 0, oth = 0, b = 0;
	public int otherCount = 0;
	public byte f1 = 0;
	public Color fadeColor = Color.black;
	public GameObject audiom, MenuInGameCanvas;
	private bool pause;

	//HASHES
	int TextHash = Animator.StringToHash("transit");
    #endregion
    #region classes adicionais
    #endregion
    #region co-routines
    IEnumerator textFade()						//Declaração da função com o tipo IEnumerator 
    {											//necessário para executar em segundo plano
        txtAnim.SetBool(TextHash, true);		//define a variável TextHash dentro do animador
												//que inicia a animação de fade do texto
        f1 = 1;									//define f1 = 1, travando o controle do jogo
        yield return new WaitForSeconds(2);		//atrasa o retorno, esperando por dois segundos
												//após os dois segundos, a função continua sua execução
        txtAnim.SetBool(TextHash, false);		//impossibilita a animação de executar novamente
        f1 = 0;									//define f1 = 0, retornando o controle ao jogador
    } 
	IEnumerator subtitleTiming(int typeOf, string type2txt = "")
    {
		
		
		if (typeOf == 1) {
			if (SceneManager.GetActiveScene ().name == "tutorial" ||
				SceneManager.GetActiveScene ().name == "L04" ||
				SceneManager.GetActiveScene().name == "L05")
			f1 = 1;
			while (i < legenda.Length) {
				txtLgnd.text = legenda [i];
				yield return new WaitForSeconds (4);
				i++;
			}
			if (GameObject.Find ("Barreira") != null) {
				barreira.SetActive (false);
			}
			txtLgnd.enabled = false;
			switch (SceneManager.GetActiveScene ().name) {
				case "tutorial":
					txtOther.text = "Use as setas para se mover";
					txtAnim.SetBool (TextHash, true);
					f1 = 0;
					yield return new WaitForSeconds (2);
					txtAnim.SetBool (TextHash, false);
					break;
				case "L02":
					txtOther.text = "Corra, sem se importar com nada";
					txtAnim.SetBool (TextHash, true);
					f1 = 0;
					yield return new WaitForSeconds (2);
					txtAnim.SetBool (TextHash, false);
					break;
				case "L04":
					txtOther.text = "Voe, encontre seus pensamentos, em seu tempo.";
					txtAnim.SetBool (TextHash, true);
					f1 = 0;
					yield return new WaitForSeconds (2);
					txtAnim.SetBool (TextHash, false);
					break;
			case "L05":
				txtOther.text = "Voe, encontre uma saída ▲▼";
				txtAnim.SetBool (TextHash, true);
				f1 = 0;
				yield return new WaitForSeconds (2);
				txtAnim.SetBool (TextHash, false);
				break;
			}
			i = 0;
		} else if (typeOf == 2) {
			txtLgnd.enabled = true;
			txtLgnd.text = type2txt;
			yield return new WaitForSeconds (3);
			txtLgnd.enabled = false;
			f1 = 0;
		} else if (typeOf == 3) {
			txtLgnd.enabled = true;
			yield return new WaitForSeconds (2);
			txtLgnd.enabled = false;
		} else if (typeOf == 4) {
			string[] a = new string[3];
			a [0] = "E de repente eu passo a ver menos";
			a [1] = "Como se velocidade não impotasse mais";
			a [2] = "Como se eu estivesse perdendo o controle...";
			int i = 0;
			txtLgnd.enabled = true;
			while (i < a.Length) {
				txtLgnd.text = a [i];
				yield return new WaitForSeconds (3);
				i++;
			}
		}
    }
    #endregion
    #region start e updates
    void Start () {
		txtAnim = txtOther.gameObject.GetComponent<Animator> ();
		if (GameObject.Find ("Barreira") != null) {
			barreira = GameObject.Find ("Barreira");
		}
		StartCoroutine (subtitleTiming (1));
	}
	
	// Update is called once per frame
	void Update () {
		if (otherCount == 5) {
			StartCoroutine (subtitleTiming (2, "Eles sempre parecem Iguais..."));
			otherCount = 0;
		}
		//menu is here
		menuobj = GameObject.Find ("MenuInGameCanvas(Clone)");  //variavel pra encontrar o objeto facil
		if (Input.GetKeyDown (KeyCode.Escape)) {				//verifica se o usuario apertou a tecla esc
			Time.timeScale = 0.0f; 								//pausa o tempo
			if (menuobj == null) 								//verifica se o objeto do menu não existe
				Instantiate (MenuInGameCanvas);					//cria o menu na tela a partir do Prefab
			else {												//caso o menu exista quando esc foi pressionado
				DestroyObject (menuobj);						//destroi o objeto do menu
				Time.timeScale = 1.0f;							//volta o tempo ao normal
			}
		}														//O menu possui um botão que muda o nome do objeto para "MenuDestroy"
		if (GameObject.Find ("MenuDestroy")) {					//Se, na atualização de um frame, o jogo encontrar um objeto com esse nome
			DestroyObject (GameObject.Find ("MenuDestroy"));	//Destrói esse objeto (pq o usuario que retornar para o jogo)
			Time.timeScale = 1.0f;								//volta o tempo ao normal
		}
    }
    #endregion
	#region ONTRIGGERENTER ESTA AQUI AMIGUINHO
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Random")) {
			int a = Random.Range (0, 4);
			while (a == rndComp)
				a = Random.Range (0, 4);
			rndComp = a;
			switch (SceneManager.GetActiveScene ().name) {
			case "L01":
				other.enabled = false;
				txtOther.text = strOther [a];
				txtOther.transform.position = other.transform.position;
				StartCoroutine (textFade ());
				break;
			case "L02":
				txtLgnd.text = strOther [a];
				StartCoroutine (subtitleTiming (3));
				break;
			case "L03":
				other.gameObject.SetActive (false);
				txtLgnd.text = strOther [a];
				StartCoroutine (subtitleTiming (3));
				break;
			case "L04":
				other.gameObject.SetActive (false);
				if (!txtLgnd.enabled)
					txtLgnd.enabled = true;
				txtLgnd.text = LegSeq [b];
				b++;
				oth ++;
				if(oth == LegSeq.Length){
					initiate.Fade ("L05", fadeColor, 0.5f);
				}
				break;
			case "L05":
				other.gameObject.SetActive (false);
				break;
			}

		}

		else if (other.gameObject.CompareTag ("Important")) {
			switch (SceneManager.GetActiveScene ().name) {
			case "tutorial":
				other.enabled = false;
				StartCoroutine (subtitleTiming (2, LegSeq [i]));
				i++;
				if (i == LegSeq.Length)
					initiate.Fade ("L01", Color.gray, 0.5f);
				break;
			case "L02":
				if (other.gameObject.name == "trgLeg") {
					other.gameObject.SetActive (false);
					StartCoroutine(subtitleTiming(4));
				}
				break;
			}

		}

		//WARP ZONE
		else if (other.gameObject.CompareTag ("warp")) {
			switch (SceneManager.GetActiveScene ().name) {
			case "L01":
				initiate.Fade ("L02", fadeColor, 0.5f);
				DontDestroyOnLoad (audiom);
                f1 = 1;
				break;
			case "L02":
				initiate.Fade ("L03", fadeColor, 0.5f);
				DontDestroyOnLoad (audiom);
				break;
			case "L03":
				initiate.Fade ("L04", fadeColor, 0.5f);
				DontDestroyOnLoad (audiom);
				break;
			case "L04":
				DontDestroyOnLoad (audiom);
				break;
			case "L05":
				if(other.gameObject.name == "Perder01")
					initiate.Fade ("L05-01", fadeColor, 0.5f);
				else if (other.gameObject.name == "Perder02")
					initiate.Fade ("L05-02", fadeColor, 0.5f);
				break;
			case "L05-01":
				initiate.Fade ("HER", fadeColor, 1.5f);
				break;
			case "L05-02":
				initiate.Fade ("HER", fadeColor, 1.5f);
				break;

			}
		}
	}
	#endregion
}
