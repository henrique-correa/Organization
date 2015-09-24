using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using  UnityEngine.UI;

public class Jogador_controle : NetworkBehaviour {

	Vector2 mouse_look;
	Vector3 dir;

	public int id;
	public GameObject tiro_spawn;

	[SyncVar]public int vida;

	[SyncVar]public int pontos;

	[SyncVar]public bool maleta;

	public int cor;
	public float Jogador_cadencia_tiro;
	float Jogador_proximo_tiro = 0.0f;




	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		GameObject.Find ("debug_vida").GetComponent<Text> ().text = "vida: " + vida.ToString ();
		//GameObject.Find ("debug_pontos").GetComponent<Text> ().text = "pontos: " + gerente.singleton.GetComponent<gerente> ().placar [id].ToString ();
		GameObject.Find ("debug_maleta").GetComponent<Text> ().text = "maleta: " + maleta.ToString ();
					
			//WASD move o jogador de acordo com as coordenadas do MUNDO
			
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector2.up * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector2.left * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector2.down * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector2.right * Time.deltaTime, Space.World);
			}
			
			
			// rotaciona o jogador em direçao ao mouse
			
			mouse_look = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dir = new Vector3 (mouse_look.x, mouse_look.y, 0.0f) - transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		if (Input.GetMouseButton (0)) {
			if (Time.time > Jogador_proximo_tiro) {
				Jogador_proximo_tiro = Time.time + Jogador_cadencia_tiro;
				Debug.Log ("TIRO");

					Cmd_atirar ();


			}
		}
	}	
	[Command]
	void Cmd_atirar(){
		GameObject t = Instantiate (Resources.Load ("Tiro"), tiro_spawn.transform.position, gameObject.transform.rotation) as GameObject;
		//NetworkConnection c = netId;

		t.GetComponent<Tiro> ().tiro_id = id; 
		NetworkServer.Spawn (t);

	}


	void OnCollisionEnter2D (Collision2D col){

		Debug.Log ("AAAAAAAA");
	}

	public void dano (int d){
		if (!isServer) {
			return;
		}
		vida -= d;
		if (vida <= 0) {
			Debug.Log ("morreu");
		}
	}

	[Command]
	public void Cmd_add_pontos(int p){

		pontos += p;

	}
}
