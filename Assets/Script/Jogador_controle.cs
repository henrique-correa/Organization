using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using  UnityEngine.UI;

public class Jogador_controle : NetworkBehaviour {

	Vector2 mouse_look;
	Vector3 dir;


	Animator animatorCorpo, animatorPernas; // Animator dos objetos Corpo e Pernas;
	GameObject corpo, pernas;
	int nEstado;

	[SyncVar]public int id;
	public GameObject tiro_spawn;

	public GameObject maleta_Ref;




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
	bool encimaMaleta = false;

	int pontos_por_morte = 50;
	int dano = 5;
	bool morto = false;

	public int pontos_maleta_tempo;
	int pontos_maleta_proximo;

	public Color cor_player;

	//bool spawnMaleta = false;
	//float tempoMaleta = 0.0f;
	//float proximaMaleta = 30.0f;
	//int mCount = 0;

	//Vector3 pos2; 
	float x, y, z;


	// Use this for initialization
	void Start () {
		corpo = gameObject.transform.FindChild("Corpo").gameObject;
		pernas = gameObject.transform.FindChild("Pernas").gameObject;
		animatorCorpo = corpo.GetComponent<Animator>();
		animatorPernas = pernas.GetComponent<Animator>();
		/*corpo.GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		pernas.GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		corpo.GetComponent<NetworkAnimator> ().GetParameterAutoSend (0);
		pernas.GetComponent<NetworkAnimator> ().GetParameterAutoSend (0);*/

	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (pos2);
		/*if (spawnMaleta == true) {
			
			Cmd_respawMaleta();
			spawnMaleta = false;

		}*/
		//Cmd_cor (cor_player);
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
			animatorPernas.SetBool ("andando", true);
			pernas.transform.eulerAngles = new Vector3 (pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 270.0f);
			}
			if (Input.GetKeyUp (KeyCode.W)) {
			animatorPernas.SetBool("andando", false);
			}


			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector2.left * Time.deltaTime, Space.World);
				animatorPernas.SetBool("andando", true);
				pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 180.0f);
			}
			if (Input.GetKeyUp (KeyCode.A)) {
			animatorPernas.SetBool("andando", false);
			}


			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector2.down * Time.deltaTime, Space.World);
				animatorPernas.SetBool("andando", true);
				pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 90.0f);
			}
			if (Input.GetKeyUp (KeyCode.S)) {
			animatorPernas.SetBool("andando", false);
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector2.right * Time.deltaTime, Space.World);
				animatorPernas.SetBool("andando", true);
				pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 0.0f);
			}
			if (Input.GetKeyUp (KeyCode.D)) {
			animatorPernas.SetBool("andando", false);
			}

			if (Input.GetKeyDown (KeyCode.E)) {
				if(encimaMaleta == true){
				maleta = true;
			//GameObject m = GameObject.FindGameObjectWithTag("maleta");
				Cmd_MoveMaletaOut();
				//NetworkServer.Destroy(maleta_Ref);
			}
			if(encimaMaleta == false && maleta == true){
				maleta = false;
				Cmd_MoveMaletaIn(transform.position);


			}
			//transform.Translate (Vector2.right * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.Q)) {
			if(maleta == true){
				//maleta = false;
				//Cmd_respawMaleta();
			}
			//GameObject m = GameObject.FindGameObjectWithTag("maleta");
			//Destroy(m);
			//transform.Translate (Vector2.right * Time.deltaTime, Space.World);
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
				nEstado = 1;
				animatorCorpo.SetInteger("estado", nEstado);
				GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = true;
				Cmd_atirar ();
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			nEstado = 0;
			animatorCorpo.SetInteger("estado", nEstado);
			GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = false;
		}

		if (Input.GetMouseButton (1)) {
			if (Time.time > Jogador_proximo_soco) {
				Jogador_proximo_soco = Time.time + Jogador_cadencia_soco;
				nEstado = 2;
				animatorCorpo.SetInteger("estado", nEstado);
				Cmd_socar ();
			}
		}
		if (Input.GetMouseButtonUp (1)) {
			nEstado = 0;
			animatorCorpo.SetInteger("estado", nEstado);
		}
	}

	[Command]
	void Cmd_atirar(){
		GameObject t = Instantiate (Resources.Load ("Tiro"), tiro_spawn.transform.position, gameObject.transform.rotation) as GameObject;
		//NetworkConnection c = netId;

		t.GetComponent<Tiro> ().tiro_id = gameObject.name; 
		NetworkServer.Spawn (t);

	}


	[Command]
	void Cmd_socar(){
		GameObject s = Instantiate (Resources.Load ("Mao_soco"), tiro_spawn.transform.position, gameObject.transform.rotation) as GameObject;
		//NetworkConnection c = netId;
		
		s.GetComponent<Socar> ().Id_pai = gameObject.name; 
		NetworkServer.Spawn (s);
		
	}


	/*[Client]
	void Cmd_respawMaleta(){
		if (spawnMaleta == true) {
			GameObject m = Instantiate (Resources.Load ("maleta"), new Vector3 (x, y, z), Quaternion.identity) as GameObject;
			NetworkServer.Spawn (m);
			spawnMaleta = false;
		}


	}*/

	[Command]
	void Cmd_MoveMaletaOut(){
		maleta_Ref.transform.position = new Vector3 (-23.0f, 15.0f, 0.0f);
	}

	[Command]
	void Cmd_MoveMaletaIn(Vector3 pos){
		maleta_Ref.transform.position = pos;
	}

	//[Command]
	/*void Cmd_soco(){
		gameObject.transform.FindChild ("Mao").GetComponent<CircleCollider2D> ().enabled = true;//ativar;
	}

	void Cmd_soco2(){
		gameObject.transform.FindChild ("Mao").GetComponent<CircleCollider2D> ().enabled = false;//ativar;
	}*/


	void OnCollisionEnter2D (Collision2D col){
		if (col.collider.gameObject.tag == "tiro") {
			col_TIRO(col);
		}


		if (col.collider.gameObject.tag == "soco") {
			col_SOCO(col);
		}

		//Debug.Log ("AAAAAAAA");
	}

	void OnTriggerEnter2D(Collider2D col){
		encimaMaleta = true;
		maleta_Ref = col.gameObject;
			

	}
	void OnTriggerExit2D(Collider2D col){
		encimaMaleta = false;
		
		
	}


	[Client]
	void col_TIRO(Collision2D c){
		if (morto == false) {

			//col.gameObject.GetComponent<Tiro>().tiro_id;
			GameObject j = GameObject.Find (c.gameObject.GetComponent<Tiro> ().tiro_id);
			j.GetComponent<Jogador_controle> ().Cmd_add_pontos (pontos_por_dano);
			Cmd_Add_dano (dano);
			
			
			if (morto == true) {
				
				j.GetComponent<Jogador_controle> ().Cmd_add_pontos (pontos_por_morte);
				
				Rpc_respawn ();
				//morto = false;

				
			}
			Destroy (c.collider.gameObject);
		}
		

	}

	[Client]
	void col_SOCO(Collision2D cl){
		if (morto == false) {
			
			//col.gameObject.GetComponent<Tiro>().tiro_id;
			GameObject v = GameObject.Find (cl.gameObject.GetComponent<Socar> ().Id_pai);
			v.GetComponent<Jogador_controle> ().Cmd_add_pontos (pontos_por_soco);
			Cmd_Add_dano (dano_soco);

			
			if (morto == true) {
				
				v.GetComponent<Jogador_controle> ().Cmd_add_pontos (pontos_por_morte);
				
				Rpc_respawn ();
				//morto = false;
				
				
			}
			Destroy (cl.collider.gameObject);
		}
	}

	[Command]
	public void Cmd_Add_dano (int d){
		//if (!isServer) {
		//	return;
		//}
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
	/*[Client]
	public void Cmd_cor(Color c){
		gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = c;

	}
	[Client]
	public void Cmd_set_cor(Color e){
		cor_player = e;
		//return cor_player;
	}*/
	[Client]
	public void Cmd_SetColor(){
		if (id == 0) {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.blue;
		}
		if (id == 1) {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.red;
		}
		if (id == 2) {
			gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.green;
		}

		/*if (gameObject.name == "Player_1") {
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
		}*/
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
			if(maleta == true){
				//Debug.Log("POS2 antes " + pos2);
				x = gameObject.transform.position.x;
				y = gameObject.transform.position.y;
				z = gameObject.transform.position.z;
				//Debug.Log("POS2 DEPOIS " + pos2);
				//spawnMaleta = true;
				maleta = false;
				Cmd_MoveMaletaIn(transform.position);

			}
			gameObject.transform.position = new Vector2(0.0f , 0.0f);
			morto = false;





		}

	}
}
