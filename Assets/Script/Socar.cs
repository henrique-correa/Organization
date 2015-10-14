using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Socar : NetworkBehaviour {

	public string Id_pai;
	// Use this for initialization
	void Start () {
		Id_pai = transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {

			col.gameObject.GetComponent<Jogador_controle>().Cmd_Add_dano(15);
			GameObject.Find(Id_pai).GetComponent<Jogador_controle>().Cmd_add_pontos(20); 
		}

	}


}
