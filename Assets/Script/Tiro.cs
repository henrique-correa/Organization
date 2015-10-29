using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Tiro : NetworkBehaviour {

	public float vel_tiro = 10.0f;
	public string tiro_id;
	//public int dano = 10;
	//public int pontos_por_dano = 10;


	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * vel_tiro * Time.deltaTime);

	
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.collider.gameObject.tag == "parede") {
			Destroy(gameObject);
		}
		if (col.collider.gameObject.tag == "tiro") {
			Destroy(gameObject);
		}
		/*if (col.collider.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Jogador_controle>().dano(dano);
			GameObject j = GameObject.Find(tiro_id);
			j.GetComponent<Jogador_controle>().pontos += pontos_por_dano;
			Destroy(gameObject);


		}*/
	}
}
