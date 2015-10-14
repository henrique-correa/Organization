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

	public float Jogador_cadencia_soco;
	float Jogador_proximo_soco = 0.0f;

	int pontos_por_dano = 10;
	int pontos_por_soco = 20;

	int dano_soco = 15;

	int pontos_por_morte = 50;
	int dano = 5;
	bool morto = false;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		Cmd_SetColor ();
		if (!isLocalPlayer) {
			return;
		}

		GameObject.Find ("debug_vida").GetComponent<Text> ().text = "vida: " + vida.ToString ();
		GameObject.Find ("debug_pontos").GetComponent<Text> ().text = "pontos: " + pontos.ToString ();
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

		Vector3 pos = transform.position;
		pos.z = -10;
		Camera.main.transform.position = pos;

		//Debug.Log ("player_controller " + base.playerControllerId);

		if (Input.GetMouseButton (0)) {
			if (Time.time > Jogador_proximo_tiro) {
				Jogador_proximo_tiro = Time.time + Jogador_cadencia_tiro;
				//Debug.Log ("TIRO");

					Cmd_atirar ();


			}
		}

		if (Input.GetMouseButton (1)) {
			if (Time.time > Jogador_proximo_soco) {
				Jogador_proximo_soco = Time.time + Jogador_cadencia_soco;
				//Debug.Log ("TIRO");
				
				Cmd_soco ();
				
				
			}
		}
		if (Input.GetMouseButtonUp (1)) {
			//if (Time.time > Jogador_proximo_soco) {
				//Jogador_proximo_soco = Time.time + Jogador_cadencia_soco;
				//Debug.Log ("TIRO");
				
				Cmd_soco2 ();
				
				
			//}
		}
	}	
	[Command]
	void Cmd_atirar(){
		GameObject t = Instantiate (Resources.Load ("Tiro"), tiro_spawn.transform.position, gameObject.transform.rotation) as GameObject;
		//NetworkConnection c = netId;

		t.GetComponent<Tiro> ().tiro_id = gameObject.name; 
		NetworkServer.Spawn (t);

	}

	//[Command]
	void Cmd_soco(){
		gameObject.transform.FindChild ("Mao").GetComponent<CircleCollider2D> ().enabled = true;//ativar;
	}

	void Cmd_soco2(){
		gameObject.transform.FindChild ("Mao").GetComponent<CircleCollider2D> ().enabled = false;//ativar;
	}


	void OnCollisionEnter2D (Collision2D col){
		if (col.collider.gameObject.tag == "tiro") {
			col_TIRO(col);
		}

		//if (col.collider.gameObject.tag == "soco") {
		//	col_SOCO(col);
		//}

		//Debug.Log ("AAAAAAAA");
	}

	[Client]
	void col_TIRO(Collision2D c){

			//col.gameObject.GetComponent<Tiro>().tiro_id;
			GameObject j = GameObject.Find (c.gameObject.GetComponent<Tiro>().tiro_id);
			j.GetComponent<Jogador_controle> ().Cmd_add_pontos(pontos_por_dano);
			Cmd_Add_dano (dano);
			if(morto == true){
				j.GetComponent<Jogador_controle> ().Cmd_add_pontos(pontos_por_morte);
				Rpc_respawn();
				morto = false;
			}
			Destroy(c.collider.gameObject);
		

	}


	void col_SOCO(Collision2D cl){
		GameObject k = GameObject.Find(cl.gameObject.transform.Find("Mao").GetComponent<Socar>().Id_pai);
		Debug.Log ("colisao do soco " + k.transform.name);
		k.GetComponent<Jogador_controle> ().Cmd_add_pontos(pontos_por_soco);
		Add_dano2 (dano_soco);
		if(morto == true){
			k.GetComponent<Jogador_controle> ().Cmd_add_pontos(pontos_por_morte);
			Rpc_respawn();
			morto = false;
		}
	}
	[Command]
	public void Cmd_Add_dano (int d){
		if (!isServer) {
			return;
		}
		vida -= d;
		if (vida <= 0) {
			vida = 50;
			morto = true;

			//Debug.Log ("morreu");

		}
	}

	public void Add_dano2 (int d){
		if (!isServer) {
			return;
		}
		vida -= d;
		if (vida <= 0) {
			vida = 50;
			morto = true;
			
			//Debug.Log ("morreu");
			
		}
	}

	[Client]
	public void Cmd_SetColor(){
		if (gameObject.name == "Player_1") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.blue;
		}
		if (gameObject.name == "Player_2") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.red;
		}
		if (gameObject.name == "Player_3") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.green;
		}
		if (gameObject.name == "Player_4") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.yellow;
		}
		if (gameObject.name == "Player_5") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.magenta;
		}
		if (gameObject.name == "Player_6") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.cyan;
		}
		if (gameObject.name == "Player_7") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.white;
		}
		if (gameObject.name == "Player_8") {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.black;
		}
	}


	[Command]
	public void Cmd_add_pontos(int p){
		/*if (!isServer) {
			return;
		}*/
			pontos += p;
	}

	[ClientRpc]
	public void Rpc_respawn(){
		if (isLocalPlayer) {
			gameObject.transform.position = new Vector2(0.0f , 0.0f);
		}

	}
}
