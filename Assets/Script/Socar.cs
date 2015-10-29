using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Socar : NetworkBehaviour {

	public string Id_pai;
	// Use this for initialization

	void Start () {
		Destroy (gameObject, 2.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			Destroy(gameObject);
		}

	}

	/*[Server]
	void Cmd_aplicaDanoSoco(Collision2D c){

		c.gameObject.GetComponent<Jogador_controle>().Add_dano2(15);
		//GameObject.Find(Id_pai).GetComponent<Jogador_controle>().Cmd_add_pontos(20); 
	}*/


}
